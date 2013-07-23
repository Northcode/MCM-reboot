using MahApps.Metro.Controls;
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
using System.ComponentModel;

namespace MCM.Utils
{
    /// <summary>
    /// Interaction logic for StringPrompt.xaml
    /// </summary>
    public partial class StringPrompt : MetroWindow
    {
        public string theString
        {
            get
            {
                return this.textBox.Text;
            }
        }

        public StringPrompt(string head, string label)
        {
            InitializeComponent();
            this.Title = head;
            this.label.Content = label;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
