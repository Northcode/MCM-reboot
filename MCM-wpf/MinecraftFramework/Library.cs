using MCM.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
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

        public void ScheduleExtract(DownloadPackage pkg)
        {
            if (!File.Exists(Extractpath))
            {
                Download dl = pkg.ScheduleDownload(this.Name,this.Url);
                ApplyDlStuff(dl);
            }
        }

        private void ApplyDlStuff(Download dl)
        {
            dl.ShouldContinue = true;
            dl.Downloaded += (d) => { Extract_(d.Data); dl.Continue(); };
        }

        public void ScheduleExtract()
        {
            if (!File.Exists(Extractpath))
            {
                Download dl = DownloadManager.ScheduleDownload(Name, Url, true);
                ApplyDlStuff(dl);
            }
        }

        private void Extract_(byte[] fileData)
        {
            Directory.CreateDirectory(new FileInfo(Extractpath).DirectoryName);
            if (IsNative)
            {
                MemoryStream zipstream = new MemoryStream(fileData);
                ZipArchive zipArchive = new ZipArchive(zipstream, ZipArchiveMode.Read);
                App.Log("Opened liberary: " + Name);
                foreach (ZipArchiveEntry fileEntry in zipArchive.Entries)
                {
                    App.Log("Found file: " + fileEntry.FullName);
                    if (!fileEntry.FullName.Contains("META-INF") && ((!ExtractExclusions.All(s => fileEntry.FullName.Contains(s))) || ExtractExclusions.Count == 0))
                    {
                        new FileInfo(MinecraftData.NativesPath + "\\" + fileEntry.FullName).Directory.Create(); //make sure directory exists!
                        File.WriteAllBytes(MinecraftData.NativesPath + "\\" + fileEntry.FullName, StreamHelper.StreamToByteArray(fileEntry.Open()));
                        App.Log("Wrote file to: " + MinecraftData.NativesPath + "\\" + fileEntry.FullName);
                    }
                    else
                    {
                        App.Log("Exluded file");
                    }
                }
            }
            File.WriteAllBytes(Extractpath, fileData); //Do this anyway so we know if it is downloaded
        }
    }
}
