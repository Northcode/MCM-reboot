using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using MCM.Utils;

namespace MCM.MinecraftFramework
{
    public class MinecraftStatus
    {
        public bool login { get; set; }
        public bool multiplayer { get; set; }

        public void refreshStatus()
        {
            if (DownloadManager.hasInternet)
            {
                this.login = false;
                this.multiplayer = false;

                WebClient wc = new WebClient();

                string content = wc.DownloadString("http://status.mojang.com/check");

                content = "{ \"data\": " + content;
                content = content + "}";
                JObject json = JObject.Parse(content);

                foreach (JObject obj in json["data"].Children<JObject>())
                {
                    if ((string)obj["login.minecraft.net"] == "green")
                    {
                        login = true;
                    }
                    if ((string)obj["session.minecraft.net"] == "green")
                    {
                        multiplayer = true;
                    }
                }
            }
            else
            {
                login = false;
                multiplayer = false;
            }
        }
    }
}
