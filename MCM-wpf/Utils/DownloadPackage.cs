using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCM.Utils
{
    public class DownloadPackage
    {
        public string name;
        public string desc;
        private List<Download> files = new List<Download>();
        public bool MCRequire;
        public bool downloaded;

        public Action<DownloadPackage> Downloaded;
        public Action<Download> finishedFile;

        public DownloadPackage(string name, string desc, bool MCRequire)
        {
            this.name = name; this.desc = desc; this.MCRequire = MCRequire;
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

        public void Download()
        {
            foreach (Download dl in files)
            {
                dl.DoDownload();
                dl.WaitForComplete();
            }
            downloaded = true;
            Downloaded(this);
        }

        public List<Download> getDownloads()
        {
            return files;
        }

        public int GetUnfinished()
        {
            int i = 0;
            foreach (Download dl in files)
            {
                if (!dl.Complete)
                    i++;
            }
            return i;
        }
    }
}
