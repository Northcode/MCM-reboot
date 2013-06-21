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
        public string status
        {
            set
            {
                label_status.Content = value;
            }
        }

        public DownloadControl(string name, string source)
        {
            InitializeComponent();
            label_name.Content = name;
            label_source.Content = source;
            label_status.Content = "queued";
        }
    }
}
