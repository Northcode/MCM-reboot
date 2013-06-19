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

        public void Download()
        {
            WebClient wc = new WebClient();
            App.Log("Download: " + Key + " started!");
            byte[] data = wc.DownloadData(Url);
            Data = data;
            Complete = true;
            App.Log("Download: " + Key + " Complete!");
            Downloaded(this);
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
