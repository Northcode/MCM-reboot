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
using System.Windows;
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
            //webBrowser_launcherFeed_Twitter.Source = new Uri("file:///C:/Users/Jens/Desktop/test.html");

            //webBrowser_launcherFeed_Mojang.NavigateToString(parseMojang());
            Task t = new Task(ParseMojangFeed);
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
            App.Log("Navigating to: " + e.Uri.ToString());
            e.Cancel = NewsBlocked;
            NewsBlocked = true;
        }

        void CollapseCheck(object sender,RoutedEventArgs e)
        {
            
        }

        void TitleText_Collapsed(object sender, RoutedEventArgs e)
        {
            
        }

        private void ParseMojangFeed()
        {
            if (NewsStorage.MojangFeedExists)
            {
                App.Log("Local mojang feed found, preloading that...");
                //Load local feed
                ParseMojangXml(NewsStorage.MojangFeedPath);

                //Download remote feed & load
                Task t = new Task(DownloadMojangFeed);
                t.Start();
            }
            else
            {
                DownloadMojangFeed();
            }
        }

        private void DownloadMojangFeed()
        {
            WebClient wc = new WebClient();
            App.Log("Downloading new feed from: https://mojang.com/feed/ to: " + NewsStorage.MojangFeedPath);
            wc.DownloadFile("https://mojang.com/feed/", NewsStorage.MojangFeedPath);
            App.Log("Download complete!");
            ParseMojangXml(NewsStorage.MojangFeedPath);
        }

        private void ParseMojangXml(string Path)
        {
            App.Log("Loading mojang feed from: " + Path);
            XmlReader rssr = XmlReader.Create(Path);
            SyndicationFeed feed = SyndicationFeed.Load(rssr);

            App.InvokeAction(delegate { App.mainWindow.lstMojangFeed.Items.Clear(); });
            App.Log("Loaded items: ");
            foreach (SyndicationItem item in feed.Items.Take(5))
            {
                App.Log(item.Id);
                MojangFeedItem feeditem = null;
                App.InvokeAction(delegate
                {
                    feeditem = new MojangFeedItem();
                    feeditem.TitleText.Header = item.Title.Text;
                    XElement x = item.ElementExtensions.First(p => p.OuterName == "encoded").GetObject<XElement>();
                    feeditem.Data = x.Value;
                    feeditem.DateText.Text = item.PublishDate.ToString();
                });
                App.InvokeAction(delegate
                {
                    App.mainWindow.lstMojangFeed.Items.Add(feeditem);
                    feeditem.Init();
                    feeditem.TitleText.Expanded += CollapseCheck;
                    feeditem.TitleText.Collapsed += TitleText_Collapsed;
                });
            }
            rssr.Close();
            App.Log("Load of feed: " + Path + " complete!");
        }
    }
}
