﻿using MahApps.Metro.Controls;
using MCM.BackupFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MCM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            // News feed display
            initializeNewsFeed();
            //MessageBox.Show(twitterFeed);
            //Testing mcversion stuff
            MCVersion mrds = new MCVersion() { Major = 1, Minor = 5, Revision = 2, IsSnapshot = false, Name = "Redstone Update!" };
            lstBackup.Items.Add(new Label() { Content = mrds.Name + " - " + mrds.ToString() });
            MCVersion sncht = new MCVersion() { Name = "Horses snaphot!" , IsSnapshot = true, SnapshotYear = 2013, SnapshotWeek = 16, SnapshotWeekVer = 0 };
            lstBackup.Items.Add(new Label() { Content = sncht.Name + " - " + sncht.ToString() });
            MCVersion adv = new MCVersion() { Major = 0, Minor = 8, Revision = 0, IsSnapshot = false, Name = "Adventure Update!" };
            lstBackup.Items.Add(new Label() { Content = adv.Name + " - " + adv.ToString() });

            MinecraftFramework.Library lb = new MinecraftFramework.Library() { IsNative = true, ExtractExclusions = new List<string>(new string[]{"META-INF/"}), Name = "org.lwjgl.lwjgl:lwjgl-platform:2.9.0" };

            lstBackup.Items.Add(new TextBox() { Text = lb.Url, IsReadOnly = true });
            lstBackup.Items.Add(new TextBox() { Text = lb.Extractpath, IsReadOnly = true });

        }

        /// <summary>
        /// Start Minecraft
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartMinecraft(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (((ListBoxItem)comboBox_users.SelectedItem).Uid == "(new)")
            {

            }
        }

        
    }
}
