using MCM.Data;
using MCM.MinecraftFramework;
using MCM.News;
using MCM.Pages;
using MCM.User;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
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
            MinecraftUserData.loadUsers();

            LoadMinecraftVersions();

            mainWindow = new MainWindow();
            App app = new App();
            app.Run(mainWindow);

            MinecraftUserData.saveUsers();
        }

        private static void LoadMinecraftVersions()
        {
            Task t = new Task(delegate
            {
                WebClient wc = new WebClient();
                string json = wc.DownloadString(MinecraftData.VersionsUrl);
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
            });
            t.Start();
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
                    mainWindow.txtLog.ScrollToEnd();
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
                    mainWindow.mcLog.ScrollToEnd();
                }));
            }
        }

        public static void InvokeAction(Action a)
        {
            mainWindow.Dispatcher.Invoke(a);
        }

        internal static void StartMinecraft(MinecraftVersion version)
        {
            try
            {
                Process p = new Process();
                p.StartInfo.FileName = "java.exe";
                MinecraftUser user = null;
                App.InvokeAction(delegate {
                    user = mainWindow.getSelectedUser();
                });
                string uname = user.username;
                string passw = MinecraftUser.decryptPwd(user.password_enc);
                if (!File.Exists(version.BinaryPath))
                {
                    version.DownloadJar();
                }
                version.Libraries.ForEach(l => { if (!File.Exists(l.Extractpath)) { l.Extract(false); } });
                p.StartInfo.Arguments = version.GetStartArguments(uname, passw);
                App.LogMinecraft("Starting Minecraft with arguments: " + p.StartInfo.Arguments);
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
                App.LogMinecraft("---------------------------- MINECRAFT OUTPUT --------------------------------");
                p.Start();
                p.BeginErrorReadLine();
                p.BeginOutputReadLine();
                p.Exited += (s, e) =>
                {
                    App.LogMinecraft("------------------------ END OF MINECRAFT OUTPUT ----------------------------");
                };
            }
            catch (Exception ex)
            {
                App.LogMinecraft("An error occured while starting minecraft: " + ex.ToString());
            }
        }
    }
}
