using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace MCM.Utils
{
    public class Download
    {
        public string Url { get; set; }
        public string Key { get; set; }

        public bool Complete { get; private set; }
        public byte[] Data { get; private set; }

        public Action<Download> Downloaded;

        public void DoDownload()
        {
            WebClient wc = new WebClient();
            wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wc_DownloadProgressChanged);
            wc.DownloadDataCompleted += new DownloadDataCompletedEventHandler(wc_DownloadDataCompleted);
            App.Log("Download: " + Key + " started!");
            wc.DownloadDataAsync(new Uri(Url));
        }

        void wc_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            Data = e.Result;
            Complete = true;
            App.InvokeAction(delegate
            {
                App.mainWindow.progressBar_dl.IsIndeterminate = false;
                App.mainWindow.progressBar_dl.Value = 0; 
            });
            App.Log("Download: " + Key + " Complete!");
            Downloaded(this);
        }

        private void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            int i;
            if (e.ProgressPercentage == 0)
            {
                App.InvokeAction(delegate { App.mainWindow.progressBar_dl.IsIndeterminate = true; });
            }
            else
            {
                App.InvokeAction(delegate
                {
                    if (App.mainWindow.progressBar_dl.Value < e.ProgressPercentage)
                    // Check if another download is already running so the bar doesn't spaz out
                    {
                        App.mainWindow.progressBar_dl.IsIndeterminate = false;
                        App.mainWindow.progressBar_dl.Value = e.ProgressPercentage;
                    }
                });
            }
        }

        public void WaitForComplete()
        {
            while (!Complete)
            {
                Thread.Sleep(100);
            }
        }
    }
}
