using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel.Syndication;
using System.Text;
using System.Windows.Controls;
using System.Windows.Navigation;

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
            HideScriptErrors(webBrowser_launcherFeed_Mojang, true);
            webBrowser_launcherFeed_Mojang.Navigate("https://mojang.com/");

            NewsBlocked = false;
            HideScriptErrors(webBrowser_launcherFeed_Twitter, true);
            //webBrowser_launcherFeed_Twitter.Source = new Uri("file:///C:/Users/Jens/Desktop/test.html");

            //webBrowser_launcherFeed_Mojang.NavigateToString(parseMojang());
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

        private string parseMojang()
        {
            SyndicationFeed feed = new SyndicationFeed("Mojang", "Mojang feed", new Uri("https://mojang.com/feed/"));

            int i = 0;
            StringBuilder sb = new StringBuilder();

            foreach (SyndicationItem item in feed.Items)
            {
                sb.AppendLine(item.Content.ToString());
                if (i == 5)
                    break;
                i++;

            }


            return sb.ToString();
        }

    }
}
