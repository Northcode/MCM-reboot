using MCM.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;

namespace MCM.MinecraftFramework
{
    public class Library
    {
        public string Name { get; set; }
        public bool IsNative { get; set; }
        public List<string> ExtractExclusions { get; set; }

        public string Url
        {
            get
            {
                string[] splits = Name.Split(':');
                string a = splits[0].Replace('.', '/') + "/" + splits[1] + "/" + splits[2];
                string b = splits[1] + "-" + splits[2] + (IsNative ? "-natives-windows.jar" : ".jar");
                return MinecraftData.LibraryUrl + a + "/" + b;
            }
        }

        public string Extractpath
        {
            get
            {
                string[] splits = Name.Split(':');
                string a = splits[0].Replace('.', '\\') + "\\" + splits[1] + "\\" + splits[2];
                string b = splits[1] + "-" + splits[2] + (IsNative ? "-natives-windows.jar" : ".jar");
                return MinecraftData.LibraryPath + "\\" + a + "\\" + b;
            }
        }

        public void Extract()
        {
            WebClient wc = new WebClient();
            wc.DownloadDataCompleted += wc_DownloadDataCompleted;
            App.Log("Starting download of: " + Name + " from: " + Url);
            wc.DownloadDataAsync(new Uri(Url));
        }

        void wc_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            App.Log("Download of: " + Name + " Complete!");
            if(!e.Cancelled) {
                byte[] fileData = e.Result;
                if (IsNative)
                {
                    MemoryStream zipstream = new MemoryStream(fileData);
                    ZipArchive zipArchive = new ZipArchive(zipstream, ZipArchiveMode.Read);
                    App.Log("Opened liberary: " + Name);
                    foreach (ZipArchiveEntry fileEntry in zipArchive.Entries)
                    {
                        App.Log("Found file: " + fileEntry.FullName);
                        if (!ExtractExclusions.All(s => fileEntry.FullName.Contains(s)))
                        {
                            File.WriteAllBytes(MinecraftData.NativesPath + "\\" + fileEntry.FullName, StreamHelper.StreamToByteArray(fileEntry.Open()));
                            App.Log("Wrote file to: " + MinecraftData.NativesPath + "\\" + fileEntry.FullName);
                        }
                        else
                        {
                            App.Log("Exluded file");
                        }
                    }
                }
            }
            App.Log("Exctraction of: " + Name + " Complete!\n");
        }
    }
}
