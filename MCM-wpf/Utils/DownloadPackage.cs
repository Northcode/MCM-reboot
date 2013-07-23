using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MCM.Utils
{
    public class DownloadPackage : Download
    {
        private List<Download> files = new List<Download>();

        int totalSize = -1;

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
            ProgressUpdated((int)(((totalSize - (files.Count-1)) / ((float)totalSize)) * 100));
        }

        public override void DoDownload()
        {
            if (totalSize == -1)
                totalSize = files.Count;
            if (files.Count > 0)
            {
                var dl = files.First();
                bool tmp = false;
                if (dl.ShouldContinue)
                    dl.onContinue += delegate
                    {
                        files.Remove(dl);
                    };
                else
                    tmp = true;
                dl.DoDownload();
                dl.WaitForComplete();
                if (tmp) files.Remove(dl);
                DoDownload();
            }

            if (files.Count == 0)
            {
                this.Complete = true;
                this.Downloaded(this);
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
