using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MCM.MinecraftFramework
{
    public class VersionManager
    {
        public static List<TinyMinecraftVersion> versions { get; set; }

        public static void LoadJson(string json)
        {
            versions = new List<TinyMinecraftVersion>();
            JObject obj = JObject.Parse(json);

            foreach (JObject obj2 in obj["versions"].Children<JObject>())
            {
                TinyMinecraftVersion mcv = new TinyMinecraftVersion();
                mcv.Key = (string)obj2["id"];
                try
                {
                    mcv.Type = (ReleaseType)Enum.Parse(typeof(ReleaseType), (string)obj2["type"]);
                }
                catch
                {
                    mcv.Type = ReleaseType.unknown;
                }
                versions.Add(mcv);
            }
        }
    }
}
