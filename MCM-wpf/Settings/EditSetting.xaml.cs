using MahApps.Metro.Controls;
using Microsoft.Win32;
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
using System.Windows.Shapes;

namespace MCM.Settings
{
    /// <summary>
    /// Interaction logic for EditSetting.xaml
    /// </summary>
    public partial class EditSetting : MetroWindow
    {
        public EditSetting()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "Any File|*.*";
            fd.Multiselect = false;
            fd.Title = "Import file path";
            fd.ShowDialog();
            this.SettingValue.Text = fd.FileName;
        }
    }
}
