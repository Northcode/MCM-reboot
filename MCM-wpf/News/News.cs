using MahApps.Metro.Controls;
using MCM.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Xml;
using System.Xml.Linq;

namespace MCM
{
    public partial class MainWindow
    {
        public bool NewsBlocked;

        public void initializeNewsFeed()
        {
            //Set NewsBlocked to False for every Navigate call
            NewsBlocked = false;
            HideScriptErrors(webBrowser_launcherFeed, true);
            webBrowser_launcherFeed.Navigate("http://mcupdate.tumblr.com/");


            NewsBlocked = false;
            HideScriptErrors(webBrowser_launcherFeed_Twitter, true);
            webBrowser_launcherFeed_Twitter.Source = new Uri("file:///C:/Users/Jens/Desktop/test.html");
            //webBrowser_launcherFeed_Mojang.NavigateToString(parseMojang());
            Task t = new Task(parseMojang);
            t.Start();
        }

        public void HideScriptErrors(WebBrowser wb, bool hide)
        {
            var fiComWebBrowser = typeof(WebBrowser).GetField("_axIWebBrowser2", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fiComWebBrowser == null) return;
            var objComWebBrowser = fiComWebBrowser.GetValue(wb);
            if (objComWebBrowser == null)
            {
                wb.Loaded += (o, s) => HideScriptErrors(wb, hide); //In case we are to early
                return;
            }
            objComWebBrowser.GetType().InvokeMember("Silent", BindingFlags.SetProperty, null, objComWebBrowser, new object[] { hide });
        }

        private void BlockWebbrowser(object sender, NavigatingCancelEventArgs e)
        {
            //App.Log("Navigating to: " + e.Uri.ToString());
            e.Cancel = NewsBlocked;
            NewsBlocked = true;
        }

        private void parseMojang()
        {
            XmlReader rssr = XmlReader.Create("https://mojang.com/feed/");
            SyndicationFeed feed = SyndicationFeed.Load(rssr);

            foreach (SyndicationItem item in feed.Items.Take(5))
            {
                MojangFeedItem feeditem = null;
                App.InvokeAction((Action)(() => { 
                    feeditem = new MojangFeedItem();
                    feeditem.TitleText.Header = item.Title.Text;
                    XElement x = item.ElementExtensions.First(p => p.OuterName == "encoded").GetObject<XElement>();
                    feeditem.Data = x.Value;
                    feeditem.DateText.Text = item.PublishDate.ToString();
                    feeditem.Init();
                }));
                App.InvokeAction((Action)(() =>
                {
                    App.mainWindow.lstMojangFeed.Items.Add(feeditem);
                }));
            }
        }

    }
}
