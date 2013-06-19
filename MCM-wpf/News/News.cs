﻿using MCM.News;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.ServiceModel.Syndication;
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

        public UIElement[] MojangFeedContentPlaceholder = null;


        public void initializeNewsFeed()
        {
            //Set NewsBlocked to False for every Navigate call
            NewsBlocked = false;
            HideScriptErrors(webBrowser_launcherFeed, true);
            webBrowser_launcherFeed.Navigate("http://mcupdate.tumblr.com/");

            NewsBlocked = false;
            HideScriptErrors(webBrowser_launcherFeed_Twitter, true);
            webBrowser_launcherFeed_Twitter.Source = new Uri("http://mcm.northcode.no/resources/twitter_mojang.html");

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

        void OpenMojangFeedItem(object sender, RoutedEventArgs e)
        {
            MojangFeedItem mitem = ((sender as Button).Parent as Grid).Parent as MojangFeedItem;
            string data = mitem.Data;

            MojangFeedContentPlaceholder = new UIElement[grdMojangFeed.Children.Count];
            grdMojangFeed.Children.CopyTo(MojangFeedContentPlaceholder, 0);
            grdMojangFeed.Children.Clear();

            MojangFeedDisplay feeddisp = new MojangFeedDisplay();
            feeddisp.Title_Text.Text = mitem.TitleText.Content as string;
            feeddisp.web.NavigateToString("<style>body { font-family: \"Helvetica Neue\", Helvetica, Arial, sans-serif; };</style>" + data);
            feeddisp.BackButton.Click += CloseMojangFeedItem;

            grdMojangFeed.Children.Add(feeddisp);
        }

        void CloseMojangFeedItem(object sender, RoutedEventArgs e)
        {
            grdMojangFeed.Children.Clear();
            (MojangFeedContentPlaceholder as UIElement[]).ToList().ForEach(el => { grdMojangFeed.Children.Add(el); });
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
            try
            {
                WebClient wc = new WebClient();
                App.Log("Downloading new feed from: https://mojang.com/feed/ to: " + NewsStorage.MojangFeedPath);
                byte[] data = wc.DownloadData("https://mojang.com/feed/");
                File.WriteAllBytes(NewsStorage.MojangFeedPath, data);
                App.Log("Download complete!");
                ParseMojangXml(NewsStorage.MojangFeedPath);
            }
            catch (Exception ex)
            {
                App.Log("An error occured while downlaoding mojang feed: " + ex.ToString());
            }
        }

        private void ParseMojangXml(string Path)
        {
            try
            {
                App.Log("Loading mojang feed from: " + Path);
                XmlReader rssr = XmlReader.Create(Path);
                SyndicationFeed feed = SyndicationFeed.Load(rssr);

                App.InvokeAction(delegate { App.mainWindow.lstMojangFeed.Items.Clear(); });
                //App.Log("Loaded items: ");
                foreach (SyndicationItem item in feed.Items.Take(15))
                {
                    //App.Log(item.Id);
                    MojangFeedItem feeditem = null;
                    App.InvokeAction(delegate
                    {
                        feeditem = new MojangFeedItem();
                        feeditem.TitleText.Content = item.Title.Text;
                        XElement x = item.ElementExtensions.First(p => p.OuterName == "encoded").GetObject<XElement>();
                        feeditem.Data = x.Value;
                        feeditem.DateText.Text = item.PublishDate.ToString();
                    });
                    App.InvokeAction(delegate
                    {
                        App.mainWindow.lstMojangFeed.Items.Add(feeditem);
                        feeditem.TitleText.Click += OpenMojangFeedItem;
                    });
                }
                rssr.Close();
                App.Log("Load of feed: " + Path + " complete!");
            }
            catch (Exception ex)
            {
                App.Log("Error while loading mojang feed from: " + Path + " , Error: " + ex.ToString());
            }
        }
    }
}
