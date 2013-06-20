using MCM.Data;
using MCM.MinecraftFramework;
using MCM.News;
using MCM.Pages;
using MCM.Settings;
using MCM.User;
using MCM.Utils;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MCM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static MainWindow mainWindow;
        public static MinecraftStatus mcStatus = new MinecraftStatus();

        App()
        {
            InitializeComponent();
        }

        [STAThread]
        static void Main()
        {
            PathData.InitDirectories();
            NewsStorage.InitDirectories();
            SettingsManager.Load();
            MinecraftUserData.loadUsers();

            SettingsManager.AddDefault("javapath", "java", "java.exe");

            App app = new App();
            mainWindow = new MainWindow();

            SettingsManager.LoadList();

            ScheduleMinecraftVersionJsonDownload();

            MinecraftAssetManager.LoadAssets();

            DownloadManager.DownloadAll();

            App.Log("Java version: " + GetJavaVersionInformation());

            app.Run(mainWindow);

            MinecraftUserData.saveUsers();
            SettingsManager.Save();
        }

        private static void ScheduleMinecraftVersionJsonDownload()
        {
            Download dl = DownloadManager.ScheduleDownload("Minecraft Version Json", MinecraftData.VersionsUrl,true);
            dl.Downloaded += (d) =>
            {
                string json = Encoding.ASCII.GetString(d.Data);
                VersionManager.LoadJson(json);
                App.InvokeAction(delegate
                {
                    foreach (TinyMinecraftVersion item in VersionManager.versions)
                    {
                        Label lbl = new Label();
                        lbl.Content = item.Key;
                        lbl.Tag = item;
                        lbl.MouseDoubleClick += (s, e) =>
                        {
                            MinecraftVersion mcversion = ((s as Label).Tag as TinyMinecraftVersion).FullVersion;
                            App.InvokeAction(delegate
                            {
                                TabItem tp = new TabItem();
                                tp.Header = mcversion.Key;
                                MCVersionPage versionPage = new MCVersionPage();
                                versionPage.VersionNameText.Text = mcversion.Key;
                                versionPage.VersionInfoText.Text = mcversion.ToString();
                                versionPage.Version = mcversion;
                                versionPage.ChooseVersion += ChooseVersion;
                                tp.Content = versionPage;
                                mainWindow.Tabs.Items.Insert(1, tp);
                            });
                        };
                        App.mainWindow.lstBackup.Items.Add(lbl);
                    }
                });
            };
        }

        public static void ChooseVersion(MinecraftVersion version, MCVersionPage page)
        {
            MinecraftData.selectedVersion = version;
        }

        public static void Log(string Line)
        {
            if (mainWindow != null)
            {
                mainWindow.Dispatcher.Invoke((Action)(() => {
                    mainWindow.txtLog.Text += Line + "\n";
                }));
            }
        }

        public static void LogMinecraft(string Line)
        {
            if (mainWindow != null)
            {
                mainWindow.Dispatcher.Invoke((Action)(() =>
                {
                    mainWindow.mcLog.Text += Line + "\n";
                }));
            }
        }

        public static void InvokeAction(Action a)
        {
            if(mainWindow != null) {
                mainWindow.Dispatcher.Invoke(a);
            }
        }

        public static bool IsAdministrator()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        public static string GetJavaInstallationPath()
        {
            string javaKey = "SOFTWARE\\JavaSoft\\Java Runtime Environment";
            using (var baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey(javaKey))
            {
                String currentVersion = baseKey.GetValue("CurrentVersion").ToString();
                using (var homeKey = baseKey.OpenSubKey(currentVersion))
                    return homeKey.GetValue("JavaHome").ToString();
            }
        }

        public static string GetJavaVersionInformation()
        {
            try
            {
                System.Diagnostics.ProcessStartInfo procStartInfo =
                    new System.Diagnostics.ProcessStartInfo("java", "-version ");

                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.RedirectStandardError = true;
                procStartInfo.UseShellExecute = false;
                procStartInfo.CreateNoWindow = true;
                System.Diagnostics.Process proc = new Process();
                proc.StartInfo = procStartInfo;
                proc.Start();
                return proc.StandardError.ReadToEnd();

            }
            catch (Exception)
            {
                return null;
            }
        }

        internal static void StartMinecraft(MinecraftVersion version)
        {
            try
            {
                //This causes freeze: App.InvokeAction(delegate { App.mainWindow.IsEnabled = false; });
                App.Log("Waiting for downloads to finish...");
                DownloadManager.WaitForAll();
                App.Log("Downloads should be finished!");
                Process p = new Process();
                MinecraftData.AppdataPath = MinecraftData.VersionsPath + "\\" + version.Key + "\\minecraft";
                string java = "C:\\Program Files\\Java\\jre7\\bin\\java.exe";
                if (IsAdministrator())
                {
                    java = GetJavaInstallationPath() + "\\bin\\java.exe";
                }
                p.StartInfo.FileName = java;
                MinecraftUser user = null;
                App.InvokeAction(delegate {
                    user = mainWindow.getSelectedUser();
                });
                string uname = user.username;
                string passw = MinecraftUser.decryptPwd(user.password_enc);
                if (!File.Exists(version.BinaryPath))
                {
                    version.SchuduleJarDownload();
                }
                version.Libraries.ForEach(l => { if (!File.Exists(l.Extractpath)) { l.ScheduleExtract(); } });
                DownloadManager.DownloadAll();
                App.Log("Waiting for minecraft download...");
                DownloadManager.WaitForAll();
                p.StartInfo.Arguments = version.GetStartArguments(uname, passw);
                App.Log("Starting Minecraft with arguments: " + p.StartInfo.FileName + " " + p.StartInfo.Arguments);
                p.StartInfo.UseShellExecute = false;
                p.EnableRaisingEvents = true;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.OutputDataReceived += (s, e) =>
                {
                    App.LogMinecraft(e.Data);
                };
                p.ErrorDataReceived += (s, e) =>
                {
                    App.LogMinecraft("Error > " + e.Data);
                };
                p.Start();
                p.BeginErrorReadLine();
                p.BeginOutputReadLine();
            }
            catch (Exception ex)
            {
                App.Log("An error occured while starting minecraft: " + ex.ToString());
            }
        }
    }
}
