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

namespace MCM.Settings
{
    /// <summary>
    /// Interaction logic for SettingItem.xaml
    /// </summary>
    public partial class SettingItem : UserControl
    {
        public Setting setting { get; set; }

        public SettingItem()
        {
            InitializeComponent();
        }

        private void SettingValue_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditSetting edit = new EditSetting();
            edit.SettingName.Content = setting.name;
            edit.SettingValue.Text = setting.data.ToString();
            edit.SettingSave.Click += (s, ea) =>
            {
                setting.data = edit.SettingValue.Text;
                PluginAPI.PluginManager.onSettingChange(setting);
                edit.Close();
                this.SettingValue.Content = setting.data.ToString();
            };
            edit.Show();
        }

        private void SettingValueBool_Checked(object sender, RoutedEventArgs e)
        {
            setting.data = true;
        }

        private void SettingValueBool_Unchecked(object sender, RoutedEventArgs e)
        {
            setting.data = false;
        }
    }
}
