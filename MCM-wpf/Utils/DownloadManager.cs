using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MCM.Utils
{
    public static class DownloadManager
    {
        public static List<Download> downloads = new List<Download>();

        public static Download ScheduleDownload(string Key, string Url, bool AutoClose, bool MCRequire)
        {
            Download d = new Download() { Key = Key, Url = Url };
            d.ShouldContinue = AutoClose;
            d.MCRequire = MCRequire;
            d.Downloaded += DownloadComplete;
            d.onContinue += DownloadContinue;
            downloads.Add(d);
            App.Log("Scheduled download: " + Key + " for download with id: " + (downloads.Count - 1).ToString());
            return d;
        }

        public static Download ScheduleDownload(string Key, string Url, bool AutoClose)
        {
            return ScheduleDownload(Key, Url, AutoClose, false);
        }

        public static List<Download> getAllDownloads()
        {
            return downloads;
        }

        static void DownloadComplete(Download sender)
        {
            if (sender.ShouldContinue)
                DownloadContinue(sender);
        }

        static void DownloadContinue(Download sender)
        {
            downloads.Remove(sender);
            App.InvokeAction(delegate { App.mainWindow.label_dlCount.Content = downloads.Count + " left"; });
            if (downloads.Count == 0)
            {
                App.InvokeAction(delegate { App.mainWindow.label_dlCount.Content = ""; });
            }
        }

        public static void DownloadAll()
        {
            downloads.ForEach(d =>
            {
                App.InvokeAction(delegate { App.mainWindow.label_dlCount.Content = downloads.Count + " left"; });
                d.DoDownload();
            });
        }

        public static Download GetDownload(string Key)
        {
            Download d = downloads.Find(dl => dl.Key == Key);
            if (d != null)
                return d;
            else
                throw new Exception("Download: " + Key + " not found in DownloadManger");
        }

        internal static void WaitForAll()
        {
            while (downloads.Count > 0)
            {
                Thread.Sleep(100);
            }
        }

        internal static void WaitForAllMCRequire()
        {
            while (true)
            {
                bool tmp = true;
                foreach (Download dl in DownloadManager.downloads)
                {
                    if (dl.MCRequire)
                        tmp = false;
                }
                if (tmp)
                    break;
                Thread.Sleep(100);
            }
        }
    }
}
