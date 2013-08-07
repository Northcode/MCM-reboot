using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for DownloadControl.xaml
    /// </summary>
    public partial class DownloadControl : UserControl
    {
        public Download reference;

        public DownloadControl(Download d)
        {
            InitializeComponent();
            reference = d;
            label_name.Content = reference.Key;
            reference.ProgressUpdated += UpdateProgress;
            reference.Downloaded += (dl) =>
            {
                App.InvokeAction(delegate { this.Dispatcher.Invoke((Action)delegate { CloseBtn.IsEnabled = true; }); });
            };

            ContextMenu cm = new ContextMenu();
            MenuItem mi_link = new MenuItem();
            mi_link.Header = d.Url;
            cm.Items.Add(mi_link);
            MenuItem mi_cp = new MenuItem();
            mi_cp.Header = "Copy to clipboard";
            mi_cp.Click += delegate
            {
                Clipboard.SetText(d.Url);
            };
            cm.Items.Add(mi_cp);
            this.ContextMenu = cm;
        }

        public void UpdateProgress(int i)
        {
            this.Dispatcher.Invoke((Action)delegate { 
            if (i == 0)
            {
                prgs.IsIndeterminate = true;
            }
            else
            {
                prgs.IsIndeterminate = false; prgs.Value = i;
            }
            });
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            App.InvokeAction(delegate { App.mainWindow.listBox_downloadManager.Items.Remove(this); });
        }
    }
}
