using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace MCM.News
{
    /// <summary>
    /// Interaction logic for MojangFeedDisplay.xaml
    /// </summary>
    public partial class MojangFeedDisplay : UserControl
    {
        public MojangFeedDisplay()
        {
            InitializeComponent();
        }

        private static bool webBrowser_willNavigate;
        private void webBrowser_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            /*
            if (!webBrowser_willNavigate)
            {
                webBrowser_willNavigate = true;
                return;
            }

            e.Cancel = true;

            var startInfo = new ProcessStartInfo
            {
                FileName = e.Uri.ToString()
            };

            Process.Start(startInfo);
            */
        }
    }
}
