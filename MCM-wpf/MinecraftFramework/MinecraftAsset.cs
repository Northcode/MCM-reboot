using MCM.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace MCM.MinecraftFramework
{
    public class MinecraftAsset
    {
        public string Key { get; set; }
        public string md5 { get; set; }

        public bool IsDirectory
        {
            get
            {
                return Key.EndsWith("/");
            }
        }

        public string Path
        {
            get
            {
                return MinecraftData.AssetsPath + "\\" + Key.Replace("/", "\\");
            }
        }

        public string Url
        {
            get
            {
                return MinecraftData.AssetsUrl + Key;
            }
        }

        public void ScheduleDownload()
        {
            if (!File.Exists(Path))
            {
                Download dl = DownloadManager.ScheduleDownload(Key, Url);
                dl.Downloaded += (d) =>
                {
                    byte[] filedata = d.Data;
                    using (MD5 hasher = MD5.Create())
                    {
                        byte[] downloadedHash = hasher.ComputeHash(filedata);
                        StringBuilder sb = new StringBuilder();
                        downloadedHash.ToList().ForEach(b => sb.Append(b.ToString("x2")));
                        string hashString = sb.ToString();

                        if (hashString == md5)
                        {
                            Directory.CreateDirectory(new FileInfo(Path).DirectoryName); //MAKE SURE DIRECTORIES EXIST BEFORE WRITING!
                            if (!IsDirectory)
                            {
                                File.WriteAllBytes(Path, filedata);
                            }
                        }
                        else
                        {
                            throw new Exception("MD5 hashes do not match for MinecraftAsset: " + Key + " stored md5: " + md5 + " downloaded md5: " + hashString);
                        }
                    }
                };
            }
            else
            {
                App.Log("Asset: " + Key + " allready exists, skipping");
            }
        }

        void filedownloader_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            if(!e.Cancelled) {
                
            }
        }

    }
}
