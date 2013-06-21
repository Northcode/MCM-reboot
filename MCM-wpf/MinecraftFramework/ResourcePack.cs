using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MCM.MinecraftFramework
{
    class ResourcePack
    {
        public string name { get; set; }
        public ResourceInfo packInfo { get; set; }
        public string path { get; set; }

        public void getPath()
        {
            getPath(" { insert backup dir here } ");
        }

        public void getPath(string packDir)
        {

        }
    }

    class ResourceInfo
    {
        public int packFormat { get; set; }
        public string desc { get; set; }

        public void parseJson(string json)
        {
            JObject obj = JObject.Parse(json);

            this.packFormat = (int)obj["pack"]["pack_format"];
            this.desc = (string)obj["pack"]["description"];
        }
    }
}
