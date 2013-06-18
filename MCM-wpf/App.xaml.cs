﻿using MCM.Data;
using MCM.MinecraftFramework;
using MCM.News;
using MCM.Pages;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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

        public static void ChooseVersion(MinecraftVersion verison, MCVersionPage page)
        {
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

        public static void InvokeAction(Action a)
        {
            mainWindow.Dispatcher.Invoke(a);
        }
    }
}
