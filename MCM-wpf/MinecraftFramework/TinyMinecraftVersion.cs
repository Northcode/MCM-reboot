﻿using System;
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

        public string Url
        {
            get
            {
                return "https://s3.amazonaws.com/Minecraft.Download/versions/" + Key + "/" + Key + ".json";
            }
        }

        public string JsonPath
        {
            get
            {
                return MinecraftData.VersionsPath + "\\" + Key + "\\" + Key + ".json";
            }
        }

        public void DownloadVersionInfo()
        {
            try
            {
                App.Log("Downloading json for minecraftversion: " + Key);
                WebClient wc = new WebClient();
                string data = wc.DownloadString(Url);
                var fi = new FileInfo(JsonPath);
                if (!Directory.Exists(fi.DirectoryName))
                {
                    App.Log("Creating directory: " + fi.DirectoryName);
                    Directory.CreateDirectory(fi.DirectoryName);
                }
                File.WriteAllText(JsonPath, data);
                App.Log("Json Downloaded, saved to: " + JsonPath);
            }
            catch (Exception e)
            {
                App.Log("An error occured while downloading json for: " + Key);
                App.Log("Error data: " + e.ToString());
            }
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
                    if (!File.Exists(JsonPath))
                    {
                        DownloadVersionInfo();
                    }
                    string data = File.ReadAllText(JsonPath);
                    return MinecraftVersion.fromJson(data);
                }
            }
        }
    }
}
