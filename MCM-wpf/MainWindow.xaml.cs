using MahApps.Metro.Controls;
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
using MCM.Utils;
using MCM.MinecraftFramework;
using MCM.Settings;
using System.Diagnostics;

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

            updateStatus(null,null);
            // News feed display
            initializeNewsFeed();
            updateUsersList();
            updateInstances();

        }

        void timerTick(object sender, System.Timers.ElapsedEventArgs e)
        {
            App.InvokeAction(delegate
            {
                updateDownloadConsole();
            });
        }

        /// <summary>
        /// Start Minecraft
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartMinecraftButton(object sender, RoutedEventArgs e)
        {
            if (lstBackup.SelectedItem != null)
            {
                Task t = new Task(delegate
                {
                    MinecraftFramework.MinecraftVersion v = null;
                    App.InvokeAction(delegate
                    {
                        v = ((lstBackup.SelectedItem as Label).Tag as MinecraftFramework.TinyMinecraftVersion).FullVersion;
                    });
                    App.StartMinecraft(v);
                    
                });
                t.Start();
            }
        }

        /// <summary>
        /// Make the webbrowser navigate to the link but in the default webbrowser.
        /// <value name="webBrowser_willNavigate">The count of the webbrowsers. Update this if you add/remove a webbrowser with this void with this event handler</value>
        /// <param name="sender">The webbrowser that fired the event</param>
        /// </summary>
        private static int webBrowser_willNavigate = 0;
        private void webBrowser_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            if (webBrowser_willNavigate != 2)
            {
                webBrowser_willNavigate++;
                return;
            }

            e.Cancel = true;

            var startInfo = new ProcessStartInfo
            {
                FileName = e.Uri.ToString()
            };

            Process.Start(startInfo);
        }

        /// <summary>
        /// Check if the NewUser dialog has to be opened
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox_users.SelectedIndex == -1)
                return;
            if (((ListBoxItem)comboBox_users.SelectedItem).Uid == "(new)")
            {
                NewUser nu = new NewUser();
                nu.ShowDialog();
                comboBox_users.SelectedIndex = -1;
                updateUsersList();
            }
        }

        #region UpdateStuff

        private static int olddl;
        private void updateDownloadConsole()
        {
<<<<<<< HEAD
            List<Download> dls = DownloadManager.downloads;
            //if (dls.Count != olddl)
            //{
                olddl = dls.Count;
                listBox_downloadManager.Items.Clear();
                foreach (Download dl in dls)
=======
            App.InvokeAction(delegate { listBox_downloadManager.Items.Clear();
            List<Download> dls = DownloadManager.getAllDownloads();
            foreach (Download dl in dls)
            {
                DownloadControl control = new DownloadControl(dl.Key, dl.Url);
                if (dl.ShouldContinue)
>>>>>>> dev-Andreas
                {
                    DownloadControl control = new DownloadControl(dl.Key, dl.Url);
                    if (dl.ShouldContinue)
                    {
                        control.status = "downloading...";
                    }
                    listBox_downloadManager.Items.Add(control);
                }
<<<<<<< HEAD
            //}
=======
                listBox_downloadManager.Items.Add(control);
            }
            });
>>>>>>> dev-Andreas
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

        private void updateStatus(object sender, MouseButtonEventArgs e)
        {
            label_loginStatus.Content = "Refreshing...";
            label_multiplayerStatus.Content = "Refreshing...";
            Thread t = new Thread(updateStatuses);
            t.Start();
        }

        private void updateStatuses()
        {
            App.mcStatus.refreshStatus();
            App.InvokeAction(delegate { 
                label_loginStatus.Content = (App.mcStatus.login ? "Online" : "Offline");
                label_multiplayerStatus.Content = (App.mcStatus.multiplayer ? "Online" : "Offline");
            });
        }

        public void updateInstances()
        {
            treeView_instances.Items.Clear();

            foreach (Instance i in InstanceManager.instances)
            {
                treeView_instances.Items.Add(i.GetTreeViewItem());
            }
        }

        public void updateProgressBar(int i)
        {
            progressBar_dl.Value = i;
        }

        #endregion


        public MinecraftUser getSelectedUser()
        {
            if (comboBox_users.SelectedIndex == -1)
                throw new Exception("No user selected");
            foreach (MinecraftUser user in MinecraftUserData.users)
            {
                if (user.username + ";" + user.password_enc == ((ListBoxItem)comboBox_users.SelectedItem).Uid)
                {
                    return user;
                }
            }
            throw new Exception("Specified user not found!");
        }

        public Instance getSelectedInstance()
        {
            TreeViewItem item = (treeView_instances.SelectedItem as TreeViewItem);
            while (item.Parent.GetType() != typeof(TreeView))
            {
                item = (item.Parent as TreeViewItem);
            }

            return (item.Tag as Instance);
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void MetroWindow_Initialized(object sender, EventArgs e)
        {
        }

        private void cbxType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            lstBackup.Items.Filter = null;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            lstBackup.Items.Filter = (p) => { return ((p as Label).Tag as TinyMinecraftVersion).Type == ReleaseType.release; };
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            lstBackup.Items.Filter = (p) => { return ((p as Label).Tag as TinyMinecraftVersion).Type == ReleaseType.snapshot; };
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            DownloadManager.DownloadAll();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            StringPrompt sp = new StringPrompt("New Instance", "Name:");
            if (sp.ShowDialog() == true)
            {
                Instance i = new Instance(sp.theString);
                InstanceManager.instances.Add(i);

                updateInstances();
            }

        }

        private void treeView_instances_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            listBox_instanceInfo.Items.Clear();
            TreeViewItem item = treeView_instances.SelectedItem as TreeViewItem;
            if (item != null)
            {
                if ((item.Header as String).Contains("Minecraft version "))
                {
                    Label tb = new Label();
                    tb.Content = (item.Tag != null ? (item.Tag as MinecraftVersion).GetDescription() : "no version");
                    Button bt = new Button();
                    bt.Content = "Change version";
                    bt.Click += bt_Click;
                    listBox_instanceInfo.Items.Add(tb);
                    listBox_instanceInfo.Items.Add(bt);
                }
                else if ((item.Header as String).Contains("Mods"))
                {
                    Button bt = new Button();
                }
            }
        }

        void bt_Click(object sender, RoutedEventArgs e)
        {
            ChangeMCVersion cmv = new ChangeMCVersion();
            if (cmv.ShowDialog() == true)
            {
                Instance instance = getSelectedInstance();
                for (int i = 0; i < InstanceManager.instances.Count; i++)
                {
                    if (InstanceManager.instances[i] == instance)
                    {
                        InstanceManager.instances[i].Version = cmv.version.FullVersion;
                        (treeView_instances.SelectedItem as TreeViewItem).Header = "Minecraft version " + cmv.version.Key;
                        (treeView_instances.SelectedItem as TreeViewItem).Tag = cmv.version.FullVersion;
                        treeView_instances_SelectedItemChanged(null, null);
                        break;
                    }
                }
            }
        }
    }
}
