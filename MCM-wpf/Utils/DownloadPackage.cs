using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCM.Utils
{
    public class DownloadPackage : Download
    {
        private List<Download> files = new List<Download>();

        public Action<Download> finishedFile;

        public DownloadPackage(string name, bool MCRequire)
        {
            this.Key = name; this.MCRequire = MCRequire;
            finishedFile = delegate { };
        }

        public Download GetDownload(string Key)
        {
            Download d = files.Find(dl => dl.Key == Key);
            if (d != null)
                return d;
            else
                throw new Exception("Download not found");
        }

        public Download ScheduleDownload(Download dl)
        {
            files.Add(dl);
            dl.Downloaded += DownloadComplete;
            App.Log("Scheduled download: " + dl.Key);
            return dl;
        }

        public Download ScheduleDownload(string key, string url)
        {
            Download dl = new Download();
            dl.Key = key;
            dl.Url = url;
            return ScheduleDownload(dl);
        }

        private void DownloadComplete(Download obj)
        {
            finishedFile(obj);
        }

        public override void DoDownload()
        {
            if (files.Count > 0)
            {
                var dl = files.First();
                dl.onContinue += delegate
                {
                    files.Remove(dl);
                };
                dl.DoDownload();
                dl.WaitForComplete();
                DoDownload();
            }
        }

        public List<Download> getDownloads()
        {
            return files;
        }

        //public int IncompleteCount
        //{
        //    get
        //    {
        //        int i = 0;
        //        foreach (Download dl in files)
        //        {
        //            if (!dl.Complete)
        //                i++;
        //        }
        //        return i;
        //    }
        //}
    }
}
