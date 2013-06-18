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

namespace MCM.Pages
{
    /// <summary>
    /// Interaction logic for MCVersionPage.xaml
    /// </summary>
    public partial class MCVersionPage : UserControl
    {
        public delegate void ChooseMinecraftVersion(MinecraftFramework.MinecraftVersion version, MCVersionPage page);

        public ChooseMinecraftVersion ChooseVersion;

        public MinecraftFramework.MinecraftVersion Version { get; set; }

        public MCVersionPage()
        {
            InitializeComponent();
        }

        private void ChooseVersionButton_Click(object sender, RoutedEventArgs e)
        {
            ChooseVersion(Version, this);
        }

        private void CloseTab_Click(object sender, RoutedEventArgs e)
        {
            ((Parent as TabItem).Parent as TabControl).Items.Remove(Parent);
        }
    }
}
