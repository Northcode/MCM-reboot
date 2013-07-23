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
        private static List<Download> downloads = new List<Download>();

        public static bool hasInternet;
        private static bool isDownloading;

        //public static List<Download> getAllDownloads()
        //{
        //    List<Download> dlp = new List<Download>();
        //    foreach (DownloadPackage dlp_ in downloads)
        //    {
        //        foreach (Download dl in dlp_.getDownloads())
        //        {
        //            dlp.Add(dl);
        //        }
        //    }
        //    return dlp;
        //}

        public static void ScheduleDownload(Download dl)
        {
            downloads.Add(dl);
            App.InvokeAction(delegate { DownloadControl dc = new DownloadControl(dl); App.mainWindow.listBox_downloadManager.Items.Add(dc); });
            Download();
        }

        public static Download ScheduleDownload(string name, string url, bool MCRequire)
        {
            Download dl = new Download();
            dl.Key = name;
            dl.Url = url;
            dl.MCRequire = MCRequire;

            ScheduleDownload(dl);

            return dl;
        }

        private static void Download()
        {
            if (!isDownloading)
            {
                foreach (Download dl in downloads)
                {
                    if (!dl.Complete)
                    {
                        isDownloading = true;
                        dl.Downloaded += delegate
                        {
                            isDownloading = false;
                            if(!dl.ShouldContinue)
                                downloads.Remove(dl);
                            updateDownloadLabel("");
                            Download();
                        };
                        dl.onContinue += delegate
                        {
                            downloads.Remove(dl);
                        };
                        updateDownloadLabel(dl.Key);
                        dl.DoDownload();
                        break;
                    }
                }
            }
        }

        private static void updateDownloadLabel(string text)
        {
            App.InvokeAction(delegate
            {
                App.mainWindow.label_dlCount.Content = text;
            });
        }

        internal static void WaitForAllMCRequire()
        {
            while (downloads.All(dl => dl.MCRequire && !dl.Complete && !dl.Continued && (dl is DownloadPackage ? (dl as DownloadPackage).getDownloads().Count > 0 : true)) && downloads.Count > 0)
            {
                Thread.Sleep(100);
            }

            //while (downloads.Any(dl => dl.MCRequire))
            //{
            //    Thread.Sleep(200);
            //}
        }

        public static void CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    hasInternet = true;
                }
            }
            catch
            {
                hasInternet = false;
            }
        }
    }
}
