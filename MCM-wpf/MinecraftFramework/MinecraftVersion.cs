using MCM.BackupFramework;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCM.MinecraftFramework
{
    public class MinecraftVersion : TinyMinecraftVersion
    {
        public ProcessArguments Arguments { get; set; }
        public string MinecraftArguments { get; set; }
        public int minimumLauncherVersion { get; set; }
        public string mainClass { get; set; }
        public List<Library> Libraries { get; set; }

        public MCVersion MC_Version
        {
            get
            {
                MCVersion mcv = new MCVersion();
                mcv.Name = Key;
                if (Type == ReleaseType.release)
                {
                    mcv.IsSnapshot = false;
                    string[] versions = Key.Split('.');
                    if (versions.Length == 3)
                    {
                        mcv.Major = Convert.ToInt32(versions[0]);
                        mcv.Minor = Convert.ToInt32(versions[1]);
                        mcv.Revision = Convert.ToInt32(versions[2]);
                    }
                    else
                    {
                        mcv.Major = Convert.ToInt32(versions[0]);
                        mcv.Minor = Convert.ToInt32(versions[1]);
                    }
                }
                else
                {
                    mcv.IsSnapshot = true;
                    string[] subA = Key.Split('w');
                    mcv.SnapshotYear = 2000 + Convert.ToInt32(subA[0]);
                    mcv.SnapshotWeek = Convert.ToInt32(subA[1].Substring(0,subA[1].Length - 2));
                    mcv.SnapshotWeekVer = (int)(subA[1].Last()) - 92;
                }
                return mcv;
            }
        }

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
