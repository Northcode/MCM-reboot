using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using MCM;

namespace MCM.Utils
{
    public static class DownloadManager
    {
        private static List<DownloadPackage> packages = new List<DownloadPackage>();
        private static List<Download> downloads = new List<Download>();
        public static bool hasInternet;
        private static bool isDownloading;

        public static List<Download> getAllDownloads()
        {
            List<Download> dlp = new List<Download>();
            foreach (DownloadPackage dlp_ in packages)
            {
                foreach (Download dl in dlp_.getDownloads())
                {
                    dlp.Add(dl);
                }
            }
            return dlp;
        }

        public static void ScheduleDownloadPackage(DownloadPackage dp)
        {
            packages.Add(dp);
            Download();
        }

        public static void ScheduleDownload(Download dl)
        {
            downloads.Add(dl);
            Download();
        }

        public static Download ScheduleDownload(string name, string url, bool MCRequire)
        {
            Download dl = new Download();
            dl.Key = name;
            dl.Url = url;
            dl.MCRequire = MCRequire;

            downloads.Add(dl);
            Download();

            return dl;
        }

        private static void Download()
        {
            if (!isDownloading)
            {
                int i = 0;
                foreach (DownloadPackage pck in packages)
                {
                    if (!pck.downloaded)
                    {
                        i = i + pck.GetUnfinished();
                        isDownloading = true;
                        pck.Downloaded += (dp) =>
                        {
                            isDownloading = false;
                            Download();
                        };
                        pck.Download();
                    }
                }

                foreach (Download dl in downloads)
                {
                    if (!dl.Complete)
                    {
                        i++;
                        isDownloading = true;
                        dl.Downloaded += (dp) =>
                        {
                            isDownloading = false;
                            Download();
                        };
                        dl.DoDownload();
                    }
                }
                App.InvokeAction(delegate
                {
                    App.mainWindow.label_dlCount.Content = i.ToString();
                });
            }
            
        }

        /*public static Download GetDownload(string Key)
        {
            Download d = packages.Find(dl => dl.Key == Key);
            if (d != null)
                return d;
            else
                throw new Exception("Download: " + Key + " not found in DownloadManger");
        }*/

        internal static void WaitForAllMCRequire()
        {
            while (true)
            {
                bool tmp = true;
                foreach (DownloadPackage dlp in DownloadManager.packages)
                {
                    if (dlp.MCRequire && !dlp.downloaded)
                        tmp = false;
                }
                if (tmp)
                    break;
                Thread.Sleep(100);
            }
        }

        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
