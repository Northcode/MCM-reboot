using MCM.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MCM.MinecraftFramework
{
    public class TinyMinecraftVersion
    {
        public string Key { get; set; }
        public ReleaseType Type { get; set; }
        private MinecraftVersionControl control;

        public void CreateControl()
        {
            App.InvokeAction(delegate
            {
                this.control = new MinecraftVersionControl(this);
                App.mainWindow.lstBackup.Items.Add(control);
            });
        }

        public string JsonUrl
        {
            get
            {
                return BaseUrl + ".json";
            }
        }

        public string JarUrl
        {
            get
            {
                return BaseUrl + ".jar";
            }
        }

        public string BaseUrl
        {
            get
            {
                return "https://s3.amazonaws.com/Minecraft.Download/versions/" + Key + "/" + Key;
            }
        }

        public string JsonPath
        {
            get
            {
                return MinecraftData.VersionsPath + "\\" + Key + "\\" + Key + ".json";
            }
        }

        public string LocalPath
        {
            get
            {
                return MinecraftData.VersionsPath + "\\" + Key;
            }
        }

        public string BinaryPath
        {
            get
            {
                return LocalPath + "\\" + Key + ".jar";
            }
        }

        public override string ToString()
        {
            return "Key: " + this.Key + Environment.NewLine +
                "Releasetype: " + this.Type.ToString() + Environment.NewLine +
                "Base URL: " + this.BaseUrl;
        }

        public Download DownloadVersionInfo()
        {
            App.Log("Downloading json for minecraftversion: " + Key);
            Download dl = DownloadManager.ScheduleDownload("Minecraft json", JsonUrl, false);
            dl.Downloaded += (d) =>
            {
                string data = Encoding.ASCII.GetString(d.Data);
                var fi = new FileInfo(JsonPath);
                if (!Directory.Exists(fi.DirectoryName))
                {
                    App.Log("Creating directory: " + fi.DirectoryName);
                    Directory.CreateDirectory(fi.DirectoryName);
                }
                File.WriteAllText(JsonPath, data);
                App.Log("Json Downloaded, saved to: " + JsonPath);
            };
            return dl;
        }

        public MinecraftVersion FullVersion
        {
            get
            {
                if (this is MinecraftVersion)
                {
                    return this as MinecraftVersion;
                }
                else
                {
                    string data;
                    if (!File.Exists(JsonPath))
                    {
                        Download dl = DownloadVersionInfo();
                        dl.WaitForComplete();
                        data = Encoding.ASCII.GetString(dl.Data);
                    }
                    else
                    {
                        data = File.ReadAllText(JsonPath);
                    }
                    return MinecraftVersion.fromJson(data);
                }
            }
        }
    }
}
