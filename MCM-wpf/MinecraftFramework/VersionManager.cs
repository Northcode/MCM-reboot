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
        public static List<MinecraftVersion> versions { get; set; }

        public void convertFromJson(string json)
        {
            JObject obj = JObject.Parse(json);

            foreach (JObject obj2 in obj["versions"].Children<JObject>())
            {
                String file = MinecraftData.VersionsPath + "\\" + (string)obj2["id"] + "\\" + (string)obj2["id"] + ".json";
                if (File.Exists(file))
                {
                    MinecraftVersion mcv = new MinecraftVersion();
                    mcv.convertFromJson(File.ReadAllText(file));
                    versions.Add(mcv);
                }
            }
        }
    }
}
