﻿using System;
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
    /// Interaction logic for MojangFeedItem.xaml
    /// </summary>
    public partial class MojangFeedItem : UserControl
    {
        public string Data { get; set; }

        public MojangFeedItem()
        {
            InitializeComponent();
        }

        public void Init()
        {
            webViewer.NavigateToString(Data);
        }

        private void ExpandData(object sender, RoutedEventArgs e)
        {
            Height = 310;
            webViewer.Visibility = System.Windows.Visibility.Visible;
        }

        private void TitleText_Collapsed(object sender, RoutedEventArgs e)
        {
            Height = 55;
            webViewer.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}
