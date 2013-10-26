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
using MCM.PluginAPI;
using System.IO;

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

            App.Log(String.Format("====== Starting MC Manager version {0} ====== ({1})", App.version, DateTime.Now.ToString("s")));
            App.Log("Java version: " + App.GetJavaVersionInformation());

            UpdateStatus(null, null);
            // News feed display
            InitializeNewsFeed();
            UpdateUsersList();
            UpdatePluginList();
        }

        /// <summary>
        /// Start Minecraft
        /// </summary>
        public void StartMinecraftButton(object sender, RoutedEventArgs e)
        {
            if (comboBox_instances.SelectedItem != null)
            {
                Task t = new Task(delegate
                    {
                        Instance i = null;
                        App.InvokeAction(delegate
                        {
                            i = (comboBox_instances.SelectedItem as Control).Tag as Instance;
                        });
                        App.StartMinecraft(i);
                    });
                t.Start();
            }
        }

        /// <summary>
        /// Make the webbrowser navigate to the link but in the default webbrowser.
        /// <value name="webBrowser_willNavigate">The count of the webbrowsers. Update this if you add/remove a webbrowser with this void with this event handler</value>
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
        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox_users.SelectedIndex == -1)
                return;
            if (((ListBoxItem)comboBox_users.SelectedItem).Uid == "(new)")
            {
                NewUser nu = new NewUser();
                nu.ShowDialog();
                comboBox_users.SelectedIndex = -1;
                UpdateUsersList();
            }
        }

        #region UpdateStuff

        private void UpdateUsersList()
        {
            comboBox_users.Items.Clear();
            ListBoxItem newItem = new ListBoxItem();
            newItem.Content = "(new/edit)";
            newItem.Uid = "(new)";
            foreach (MinecraftUser user in MinecraftUserData.users)
            {
                ListBoxItem item = new ListBoxItem();
                item.Content = user.displayname;
                item.Uid = user.username + ";" + user.password;
                comboBox_users.Items.Add(item);
            }
            comboBox_users.Items.Add(newItem);
            if (MinecraftUserData.users.Count > 0)
            {
                comboBox_users.SelectedIndex = 0;
            }
        }

        private void UpdateStatus(object sender, MouseButtonEventArgs e)
        {
            label_loginStatus.Content = "Refreshing...";
            label_multiplayerStatus.Content = "Refreshing...";
            Thread t = new Thread(UpdateStatuses);
            t.Start();
        }

        private void UpdateStatuses()
        {
            DownloadManager.CheckForInternetConnection();
            App.mcStatus.refreshStatus();
            App.InvokeAction(delegate { 
                label_loginStatus.Content = (App.mcStatus.login ? "Online" : "Offline");
                label_multiplayerStatus.Content = (App.mcStatus.multiplayer ? "Online" : "Offline");
            });
        }

        public void UpdateInstances()
        {
            treeView_instances.Items.Clear();
            comboBox_instances.Items.Clear();

            foreach (Instance i in InstanceManager.instances)
            {
                treeView_instances.Items.Add(i.GetTreeViewItem());
                comboBox_instances.Items.Add(new ComboBoxItem() { Content = i.Name, Tag = i });
            }

            if (comboBox_instances.Items.Count > 0)
            {
                comboBox_instances.SelectedIndex = 0;
            }
        }

        public void UpdateProgressBar(int i)
        {
            progressBar_dl.Value = i;
        }

        public void UpdatePluginList()
        {
            listBox_plugins.Items.Clear();

            listBox_plugins.Items.Add(new ListBoxItem() { Content = "Manage Plugins", Tag = "mainItem" });

            foreach (IPlugin plugin in PluginManager.plugins)
            {
                listBox_plugins.Items.Add(new ListBoxItem() { Content = plugin.Name, Tag = plugin });
            }
        }

        #endregion


        public MinecraftUser GetSelectedUser()
        {
            if (comboBox_users.SelectedIndex == -1)
                throw new Exception("No user selected");
            foreach (MinecraftUser user in MinecraftUserData.users)
            {
                if (user.username + ";" + user.password == ((ListBoxItem)comboBox_users.SelectedItem).Uid)
                {
                    return user;
                }
            }
            throw new Exception("Specified user not found!");
        }

        public Instance GetSelectedInstance()
        {
            TreeViewItem item = (treeView_instances.SelectedItem as TreeViewItem);
            while (item.Parent.GetType() != typeof(TreeView))
            {
                item = (item.Parent as TreeViewItem);
            }

            return (item.Tag as Instance);
        }

        #region Events

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void MetroWindow_Initialized(object sender, EventArgs e)
        {

        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Log(String.Format("------ Stopping MC Manager version {0} ------ ({1})", App.version, DateTime.Now.ToString("s")));
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
            lstBackup.Items.Filter = (p) => { return ((p as Control).Tag as TinyMinecraftVersion).Type == ReleaseType.release; };
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            lstBackup.Items.Filter = (p) => { return ((p as Control).Tag as TinyMinecraftVersion).Type == ReleaseType.snapshot; };
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            lstBackup.Items.Filter = (p) => { return ((p as Control).Tag as TinyMinecraftVersion).Type == ReleaseType.old_beta; };
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            lstBackup.Items.Filter = (p) => { return ((p as Control).Tag as TinyMinecraftVersion).Type == ReleaseType.old_alpha; };
        }

        private void Button_aInstance(object sender, RoutedEventArgs e)
        {
            StringPrompt sp = new StringPrompt("New Instance", "Name:");
            if (sp.ShowDialog() == true)
            {
                Instance i = new Instance(sp.theString);
                InstanceManager.instances.Add(i);

                UpdateInstances();
            }

        }

        private void treeView_instances_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            listBox_instanceInfo.Items.Clear();
        }

        void bt_Click(object sender, RoutedEventArgs e)
        {
            ChangeMCVersion cmv = new ChangeMCVersion();
            if (cmv.ShowDialog() == true)
            {
                Instance instance = GetSelectedInstance();
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

        private void Button_rInstance(object sender, RoutedEventArgs e)
        {
            InstanceManager.DeleteInstance(GetSelectedInstance());
            UpdateInstances();
        }

        private void Button_rnInstance(object sender, RoutedEventArgs e)
        {
            StringPrompt stp = new StringPrompt("Rename Instance", "New name:");
            stp.ShowDialog();
            if (stp.theString != "" && stp.DialogResult == true)
            {
                InstanceManager.RenameInstance(((treeView_instances.SelectedItem as TreeViewItem).Tag as Instance), stp.theString);
                (treeView_instances.SelectedItem as TreeViewItem).Header = stp.theString;
            }
        }

        private void listBox_plugins_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            grid_pluginData.Children.Clear();
            object tag = ((sender as ListBox).SelectedItem as ListBoxItem).Tag;
            if (tag.GetType() == typeof(string))
            {
                if ((string)tag == "mainItem")
                {
                    ViewPluginManager();
                }
            }
            else if (tag.GetType() == typeof(IPlugin))
            {
                IPlugin plugin = tag as IPlugin;
                foreach(UIElement control in plugin.GetConfigElemets())
                {
                    grid_pluginData.Children.Add(control);
                }
            }
        }

        #endregion

        private void ViewPluginManager()
        {
            Button btn_installPlugin = new Button() { Content = "Install Plugin", Height = 25, VerticalAlignment = System.Windows.VerticalAlignment.Top };
            btn_installPlugin.Click += btn_installPlugin_Click;

            grid_pluginData.Children.Add(btn_installPlugin);
        }

        void btn_installPlugin_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            ofd.Filter = "MCM Plugins |*.dll";
            ofd.CheckFileExists = true;
            if (System.Windows.Forms.DialogResult.OK == ofd.ShowDialog())
            {
                File.Copy(ofd.FileName, PathData.PluginsPath, true);
            }
        }
    }
}
