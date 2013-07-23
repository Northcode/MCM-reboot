using MCM.Utils;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace MCM.MinecraftFramework
{
    /// <summary>
    /// Interaction logic for MinecraftVersionControl.xaml
    /// </summary>
    public partial class MinecraftVersionControl : UserControl
    {
        public TinyMinecraftVersion version;

        public MinecraftVersionControl(TinyMinecraftVersion version)
        {
            InitializeComponent();
            this.version = version;
            this.Tag = version;
            this.label_verName.Content = version.Key;
            Update();

            this.MouseDoubleClick += MinecraftVersionControl_MouseDoubleClick;

        }

        void MinecraftVersionControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Pages.MCVersionPage wnd = new Pages.MCVersionPage();
            wnd.VersionNameText.Text = version.Key;
            if (Directory.Exists(this.version.LocalPath))
            {
                wnd.VersionInfoText.Text = version.FullVersion.ToString();
            }
            else
            {
                wnd.VersionInfoText.Text = version.ToString();
            }
            wnd.ShowDialog();
        }

        public void Update()
        {
            if (DownloadManager.hasInternet && !Directory.Exists(this.version.LocalPath))
            {
                button_dl.IsEnabled = true;
                label_status.Content = "not downloaded";
            }
            else
            {
                button_dl.IsEnabled = false;
                label_status.Content = "downloaded";
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.button_dl.IsEnabled = false;
            label_status.Content = "downloading...";
            try
            {
                Task.Factory.StartNew(delegate
                {
                    Download dl = version.FullVersion.ScheduleJarDownload();
                    dl.Downloaded += delegate
                    {
                        App.InvokeAction(delegate
                        {
                            label_status.Content = "downloaded";
                        });
                    };
                });
            }
            catch
            {
                button_dl.IsEnabled = true;
                label_status.Content = "download failed";
            }
        }
    }
}
