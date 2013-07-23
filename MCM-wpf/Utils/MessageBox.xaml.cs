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
using System.Windows.Shapes;

namespace MCM.Utils
{
    /// <summary>
    /// Interaction logic for MessageBox.xaml
    /// </summary>
    public partial class MessageBox : MetroWindow
    {
        public MessageBox(string head,string text)
        {
            InitializeComponent();
            this.Title = head;
            label_text.Content = text;
        }

        public static void Show(string head, string text)
        {
            MessageBox mb = new MessageBox(head, text);
            mb.Show();
        }

        public static void ShowDialog(string head, string text)
        {
            MessageBox mb = new MessageBox(head, text);
            mb.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
