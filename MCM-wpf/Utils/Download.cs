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
        public bool MCRequire { get; set; }

        public bool Complete { get; protected set; }
        public byte[] Data { get; protected set; }

        public Action<Download> Downloaded;
        public bool ShouldContinue { get; set; }
        public bool Continued { get; protected set; }
        public Action<Download> onContinue;

        public Action<int> ProgressUpdated = delegate { };

        public virtual void DoDownload()
        {
            Continued = false;
            this.onContinue += delegate { this.Continued = true; };
            if (DownloadManager.hasInternet)
            {
                WebClient wc = new WebClient();
                try
                {
                    wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wc_DownloadProgressChanged);
                    wc.DownloadDataCompleted += new DownloadDataCompletedEventHandler(wc_DownloadDataCompleted);
                    wc.DownloadDataAsync(new Uri(Url));
                }
                catch (Exception e)
                {
                    App.Log("Error while downloading " + Key + ": " + e.ToString());
                    Downloaded(this);
                }
            }
            else
            {
                throw new Exception("No internet Connection");
            }
        }

        public void Continue()
        {
            onContinue(this);
        }

        void wc_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            try
            {
                this.Data = e.Result;
                this.Complete = true;
                App.InvokeAction(delegate
                {
                    App.mainWindow.progressBar_dl.IsIndeterminate = false;
                    App.mainWindow.progressBar_dl.Value = 0;
                });
                App.Log("Download: " + Key + " Complete!");
                Downloaded(this);
            }
            catch (Exception ex)
            {
                App.Log("Error while downloading " + Key + ": " + ex.ToString());
                Downloaded(this);
            }
        }

        private void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            try
            {
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
                            ProgressUpdated(e.ProgressPercentage);
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                App.Log("Error while downloading " + Key + ": " + ex.ToString());
            }
        }

        public void WaitForComplete()
        {
            while (!Complete || (ShouldContinue ? !Continued : true))
            {
                Thread.Sleep(100);
            }
        }
    }
}
