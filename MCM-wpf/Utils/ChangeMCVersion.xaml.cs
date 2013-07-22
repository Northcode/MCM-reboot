using MahApps.Metro.Controls;
using MCM.MinecraftFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace MCM.Utils
{
    /// <summary>
    /// Interaction logic for ChangeMCVersion.xaml
    /// </summary>
    public partial class ChangeMCVersion : MetroWindow
    {
        public TinyMinecraftVersion version;

        public ChangeMCVersion()
        {
            InitializeComponent();
            try
            {
                foreach (TinyMinecraftVersion ver in VersionManager.versions)
                {
                    ListBoxItem item = new ListBoxItem();
                    item.Content = ver.Key;
                    item.Tag = ver;

                    if (ver.Type == ReleaseType.release)
                        lb_release.Items.Add(item);
                    else if (ver.Type == ReleaseType.snapshot)
                        lb_snapshot.Items.Add(item);
                    else
                        lb_instance.Items.Add(item);
                }
            }
            catch
            {
                MessageBox mb = new MessageBox("Warning!","The versions haven't initialized yet");
                mb.Show();
                this.Close();
            }
        }

        private void lb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListBox).SelectedIndex != -1)
            {
                if (sender != lb_release)
                    lb_release.SelectedIndex = -1;
                if (sender != lb_snapshot)
                    lb_snapshot.SelectedIndex = -1;
                if (sender != lb_instance)
                    lb_instance.SelectedIndex = -1;
                this.version = (((sender as ListBox).SelectedItem as ListBoxItem).Tag as TinyMinecraftVersion);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (version != null)
            {
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox mb = new MessageBox("Warning","Please select a version or press cancel!");
                mb.Show();
            }
        }
    }
}
