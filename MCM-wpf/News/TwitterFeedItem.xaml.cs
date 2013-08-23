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

namespace MCM.News
{
    /// <summary>
    /// Interaction logic for TwitterFeedItem.xaml
    /// </summary>
    public partial class TwitterFeedItem : UserControl
    {
        public TwitterFeedItem()
        {
            InitializeComponent();
        }

        private void UpdateSize(object sender, NavigationEventArgs e)
        {
            mshtml.HTMLDocument htmlDoc = webBrowser.Document as mshtml.HTMLDocument;
            if (htmlDoc != null && htmlDoc.body != null)
            {
                mshtml.IHTMLElement2 body = (mshtml.IHTMLElement2)htmlDoc.body;
                this.Height = body.scrollHeight + 51;
            }
        }
    }
}
