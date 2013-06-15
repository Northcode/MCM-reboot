using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace MCM.MinecraftFramework
{
    class MinecraftAsset
    {
        public string Key { get; set; }
        public string md5 { get; set; }

        public string Path
        {
            get
            {
                return MinecraftData.AssetsPath + "\\" + Key;
            }
        }

        public string Url
        {
            get
            {
                return MinecraftData.AssetsUrl + Key;
            }
        }

        public void Download()
        {
            WebClient filedownloader = new WebClient();
            filedownloader.DownloadDataCompleted += filedownloader_DownloadDataCompleted;
            filedownloader.DownloadDataAsync(new Uri(Url));
        }

        void filedownloader_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            if(!e.Cancelled) {
                byte[] filedata = e.Result;
                using (MD5 hasher = MD5.Create())
                {
                    byte[] downloadedHash = hasher.ComputeHash(filedata);
                    StringBuilder sb = new StringBuilder();
                    downloadedHash.ToList().ForEach(b => sb.Append(b.ToString("x2")));
                    string hashString = sb.ToString();

                    if (hashString == md5)
                    {
                        File.WriteAllBytes(Path, filedata);
                    }
                    else
                    {
                        throw new Exception("MD5 hashes do not match for MinecraftAsset: " + Key + " stored md5: " + md5 + " downloaded md5: " + hashString);
                    }
                }
            }
        }

    }
}
