using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCM.MinecraftFramework
{
    public class MinecraftVersion
    {
        public string Key { get; set; }
        public ReleaseType Type { get; set; }
        public ProcessArguments Arguments { get; set; }
        public string MinecraftArguments { get; set; }
        public int minimumLauncherVersion { get; set; }
        public string mainClass { get; set; }
        public List<Library> Libraries { get; set; }

        public static MinecraftVersion fromJson(string json)
        {
            MinecraftVersion version = new MinecraftVersion();

            JObject obj = JObject.Parse(json);

            version.Key = (string)obj["id"];
            version.Type = (ReleaseType)Enum.Parse(typeof(ReleaseType), (string)obj["type"]);
            version.Arguments = (ProcessArguments)Enum.Parse(typeof(ProcessArguments), (string)obj["processArguments"]);
            version.MinecraftArguments = (string)obj["minecraftArguments"];
            version.minimumLauncherVersion = Convert.ToInt32((string)obj["minimumLauncherVersion"]);
            version.mainClass = (string)obj["mainClass"];

            foreach(JObject obj2 in obj["libraries"].Children<JObject>())
            {
                Library lib = new Library();
                lib.Name = (string)obj2["name"];
                if (obj2["natives"] == null)
                    lib.IsNative = false;
                else
                    lib.IsNative = true;
                if (obj2["extract"] != null)
                {
                    foreach (JObject obj3 in obj2["extract"]["exclude"].Children<JObject>())
                    {
                        lib.ExtractExclusions.Add((string)obj3);
                    }
                }
                version.Libraries.Add(lib);
            }

            return version;
        }
    }
}
