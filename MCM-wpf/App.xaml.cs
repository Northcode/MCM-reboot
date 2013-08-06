using MCM.BackupFramework;
using MCM.Data;
using MCM.MinecraftFramework;
using MCM.News;
using MCM.Pages;
using MCM.PluginAPI;
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
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
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
        public static string version
        {
            get
            {
                return MCM.Properties.Resources.ver;
            }
        }
        private static string minecraftJsonFilePath = PathData.DataPath + "\\versions\\versions.json";
        private static string logFile;

        public static SystemTray sysTray;

        App()
        {
            InitializeComponent();
        }

        [STAThread]
        static void Main()
        {
            DownloadManager.CheckForInternetConnection();
            PathData.InitDirectories();
            string remoteVer = Updater.CheckForUpdate();
            if (remoteVer != null)
            {
                if(MessageBoxResult.Yes == System.Windows.MessageBox.Show("Do you want to update?" + Environment.NewLine +
                    "Current version: " + App.version + Environment.NewLine +
                        "Remote version: " + remoteVer,
                    "Update availible",MessageBoxButton.YesNo))
                {
                    Download udl = DownloadManager.ScheduleDownload("MCM updater", "https://github.com/Northcode/MCM-reboot/blob/dev/Setup/MC%20Manager.msi?raw=true", false);
                    udl.WaitForComplete();
                    File.WriteAllBytes(PathData.UpdaterPath,udl.Data);
                    Process.Start(PathData.UpdaterPath);
                    Environment.Exit(0);
                }
            }
            NewsStorage.InitDirectories();
            SettingsManager.Load();
            MinecraftUserData.loadUsers();
            PluginManager.LoadPlugins();
            PluginManager.EnablePlugins();
            Task.Factory.StartNew(delegate { PluginManager.EnablePlugins(); });

            SettingsManager.AddDefault("javapath", "java", "java.exe");
            SettingsManager.AddDefault("MinecraftRAM", "java", "2G");
            SettingsManager.AddDefault("Sync options", "sync", "true");
            SettingsManager.AddDefault("Sync serverlists", "sync", "true");

            App app = new App();
            App.sysTray = new SystemTray();

            mainWindow = new MainWindow();

            SettingsManager.LoadList();

            ScheduleMinecraftVersionJsonDownload();

            MinecraftAssetManager.LoadAssets();

            App.logFile = (PathData.LogPath + "\\" + DateTime.Now.ToString("s").Replace(':', '-') + ".log");
            App.Log(String.Format("====== Starting MC Manager version {0} ====== ({1})", App.version, DateTime.Now.ToString("s")));
            App.Log("Java version: " + GetJavaVersionInformation());

            app.Run(mainWindow);
            Task.Factory.StartNew(delegate { PluginManager.DisablePlugins(); });
            InstanceManager.SaveInstances();
            MinecraftUserData.saveUsers();
            SettingsManager.Save();
            AppendLogFile();
        }

        private static void StartInternetCheckTimer()
        {
            System.Timers.Timer t = new System.Timers.Timer(30000);
            t.Elapsed += delegate
            {
                DownloadManager.CheckForInternetConnection();
            };
            t.Start();
        }

        private static void ScheduleMinecraftVersionJsonDownload()
        {
            if (DownloadManager.hasInternet)
            {
                Download dl = DownloadManager.ScheduleDownload("Minecraft Version Json", MinecraftData.VersionsUrl, true);
                dl.Downloaded += (d) =>
                {
                    File.WriteAllBytes(minecraftJsonFilePath, d.Data);
                    LoadMinecraftVersionJson(d.Data);
                };
            }
            else if (File.Exists(minecraftJsonFilePath))
            {
                LoadMinecraftVersionJson(File.ReadAllBytes(minecraftJsonFilePath));
            }
            else
            {
                MCM.Utils.MessageBox.ShowDialog("Warning!", "You don't have the versions file downloaded and you have no internet connection! You can't view/change any versions!");
            }
        }

        private static void LoadMinecraftVersionJson(byte[] data)
        {
            string json = Encoding.ASCII.GetString(data);
            VersionManager.LoadJson(json);
            App.InvokeAction(delegate
            {
                foreach (TinyMinecraftVersion item in VersionManager.versions)
                {
                    item.CreateControl();
                    App.mainWindow.lstBackup.Items.Filter = (p) => { return ((p as Control).Tag as TinyMinecraftVersion).Type == ReleaseType.release; };
                }
            });
            /*App.InvokeAction(delegate
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
                    App.mainWindow.lstBackup.Items.Filter = (p) => { return ((p as Control).Tag as TinyMinecraftVersion).Type == ReleaseType.release; };
                }
            });*/
            InstanceManager.LoadInstances();
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
                    if (mainWindow.txtLog.LineCount > 500)
                    {
                        AppendLogFile();
                    }
                    mainWindow.txtLog.Text += Line + "\n";
                }));
            }
        }

        private static void AppendLogFile()
        {
            File.AppendAllText(App.logFile, mainWindow.txtLog.Text);
            mainWindow.txtLog.Clear();
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

        internal static void StartMinecraft(Instance instance)
        {
            try
            {
                if (instance.Version == null)
                {
                    App.InvokeAction(delegate { MCM.Utils.MessageBox.ShowDialog("Error", "Version not set in instance!"); });
                    return;
                }
                App.InvokeAction(delegate { App.mainWindow.btn_startMinecraft.IsEnabled = false; });
                App.Log("Waiting for downloads to finish...");
                DownloadManager.WaitForAllMCRequire();
                App.Log("Downloads should be finished!");
                Process p = new Process();
                string java = SettingsManager.GetSetting("javapath").data.ToString();
                p.StartInfo.FileName = java;
                MinecraftUser user = null;
                MinecraftData.AppdataPath = instance.Version.FullVersion.LocalPath;
                App.InvokeAction(delegate {
                    user = mainWindow.getSelectedUser();
                });
                if (!File.Exists(instance.MinecraftJarFilePath))
                {
                    throw new Exception("No version selected");
                }
                DownloadPackage dp = new DownloadPackage("Libraries", true);
                dp.ShouldContinue = true;
                instance.Version.FullVersion.Libraries.ForEach(l => { if (!File.Exists(l.Extractpath)) { l.ScheduleExtract(dp); } });
                if(dp.getDownloads().Count > 0)
                    DownloadManager.ScheduleDownload(dp);

                App.Log("Waiting for minecraft download...");
                DownloadManager.WaitForAllMCRequire();
                p.StartInfo.Arguments = instance.GetStartArguments(user.username, user.password);
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
                    // No prefix since everything minecraft outputs seems to be an error
                    App.LogMinecraft(e.Data);
                };
                p.Exited += (s, e) =>
                {
                    App.InvokeAction(delegate { App.mainWindow.btn_startMinecraft.IsEnabled = true; });
                    Thread.Sleep(200);
                    Syncronizer.SyncOptions(instance);
                    Syncronizer.SyncServerlist(instance);
                    PluginManager.onCloseMinecraft(instance,user);
                };
                PluginManager.onStartMinecraft(instance, user);
                p.Start();
                p.BeginErrorReadLine();
                p.BeginOutputReadLine();
            }
            catch (Exception ex)
            {
                App.InvokeAction(delegate { App.mainWindow.btn_startMinecraft.IsEnabled = true; });
                App.Log("An error occured while starting minecraft: " + ex.ToString());
            }
        }
    }
}
