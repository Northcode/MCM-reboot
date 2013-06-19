using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCM.Utils
{
    public static class DownloadManager
    {
        static List<Download> downloads = new List<Download>();

        public static Download ScheduleDownload(string Key, string Url)
        {
            Download d = new Download() { Key = Key, Url = Url };
            d.Downloaded += DownloadComplete;
            downloads.Add(d);
            App.Log("Scheduled download: " + Key + " for download with id: " + (downloads.Count - 1).ToString());
            return d;
        }

        static void DownloadComplete(Download sender)
        {
            downloads.Remove(sender);
        }

        public static void DownloadAll()
        {
            downloads.ForEach(d => { d.DoDownload(); });
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
            downloads.ForEach(d => { d.WaitForComplete(); });
        }
    }
}
