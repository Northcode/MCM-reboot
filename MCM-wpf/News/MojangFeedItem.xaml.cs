using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for MojangFeedItem.xaml
    /// </summary>
    public partial class MojangFeedItem : UserControl
    {
        public string Data { get; set; }

        public MojangFeedItem()
        {
            InitializeComponent();
        }

        void MojangFeedItem_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (webViewer.Visibility == System.Windows.Visibility.Visible)
            {
                Height = (Parent as ListBox).ActualHeight - 40;
                webViewer.Height = Height - 50;
            }
        }

        public void Init()
        {
            webViewer.NavigateToString(Data);
            (Parent as ListBox).SizeChanged += MojangFeedItem_SizeChanged;
        }

        private void ExpandData(object sender, RoutedEventArgs e)
        {
            VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            webViewer.Visibility = System.Windows.Visibility.Visible;
            Height = (Parent as ListBox).ActualHeight - 40;
            webViewer.Height = Height - 50;
        }

        private void TitleText_Collapsed(object sender, RoutedEventArgs e)
        {
            VerticalAlignment = System.Windows.VerticalAlignment.Top;
            Height = 55;
            webViewer.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}
