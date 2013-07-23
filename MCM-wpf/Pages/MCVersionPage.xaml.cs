using MahApps.Metro.Controls;
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
    public partial class MCVersionPage : MetroWindow
    {
        public MinecraftFramework.MinecraftVersion Version { get; set; }

        public MCVersionPage()
        {
            InitializeComponent();
        }
    }
}
