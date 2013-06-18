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
using MCM.User;
using MCM.Data;

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
            updateUsersList();

            
        }

        /// <summary>
        /// Start Minecraft
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartMinecraftButton(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            App.Log("SelC");
            if (comboBox_users.SelectedIndex == -1)
                return;
            if (((ListBoxItem)comboBox_users.SelectedItem).Uid == "(new)")
            {
                
                App.Log("new");
                NewUser nu = new NewUser();
                nu.ShowDialog();
                comboBox_users.SelectedIndex = -1;
                updateUsersList();
            }
        }

        private void updateUsersList()
        {
            comboBox_users.Items.Clear();
            ListBoxItem newItem = new ListBoxItem();
            newItem.Content = "(Create new)";
            newItem.Uid = "(new)";
            foreach (MinecraftUser user in MinecraftUserData.users)
            {
                ListBoxItem item = new ListBoxItem();
                item.Content = user.displayname;
                item.Uid = user.username + ";" + user.password_enc;
                comboBox_users.Items.Add(item);
            }
            comboBox_users.Items.Add(newItem);
        }
    }
}
