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

        public void convertFromJson(string json)
        {
            JObject obj = JObject.Parse(json);

            this.Key = (string)obj["id"];
            this.Type = ((string)obj["type"] == "release" ? ReleaseType.release : ReleaseType.snapshot);
            this.Arguments = ProcessArguments.username_session_version; // Parse something here
            this.MinecraftArguments = (string)obj["minecraftArguments"];
            this.minimumLauncherVersion = Convert.ToInt32((string)obj["minimumLauncherVersion"]);
            this.mainClass = (string)obj["mainClass"];

            
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
                Libraries.Add(lib);
            }
        }
    }
}
