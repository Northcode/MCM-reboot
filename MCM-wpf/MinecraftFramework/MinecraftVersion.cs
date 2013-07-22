using MCM.BackupFramework;
using MCM.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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

        public string BinaryPath
        {
            get
            {
                return LocalPath + "\\" + Key + ".jar";
            }
        }

        public string GetStartArguments(string Username, string Password,string BinaryPath,string GameDataPath)
        {
            StringBuilder processArguments = new StringBuilder();
            processArguments.Append("-Xmx"); processArguments.Append(Settings.SettingsManager.GetSetting("MinecraftRAM").data.ToString()); processArguments.Append(" ");
            processArguments.Append("\"-Djava.library.path="); processArguments.Append(MinecraftData.NativesPath); processArguments.Append("\" ");
            processArguments.Append("-cp \""); Libraries.ForEach(l => { if (!l.IsNative) { processArguments.Append(l.Extractpath + ";"); } });
            processArguments.Append(BinaryPath + "\"; ");
            processArguments.Append(mainClass + " ");

            MinecraftData.currentSession = SessionInfo.Connect(Username, Password);
            string ArgumentsString = MinecraftArguments.Replace("${auth_player_name}", MinecraftData.currentSession.username).Replace("${auth_session}", MinecraftData.currentSession.sessionid).Replace("${game_directory}", "\"" + GameDataPath + "\"").Replace("${game_assets}", "\"" + MinecraftData.AssetsPath + "\"");
            if (MinecraftData.currentSession.sessionid == "")
                ArgumentsString = ArgumentsString.Replace("--session", "");
            processArguments.Append(ArgumentsString);
            return processArguments.ToString();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Minecraft Version: " + Key);
            sb.AppendLine("Process Arguments: " + Arguments.ToString());
            sb.AppendLine("Minecraft Arguments: " + MinecraftArguments);
            sb.AppendLine("minimumLauncherVersion: " + minimumLauncherVersion);
            sb.AppendLine("mainClass: "+ mainClass);
            sb.AppendLine("Lib count: " + Libraries.Count);
            return sb.ToString();
        }

        public Download ScheduleJarDownload()
        {
            Download dl = DownloadManager.ScheduleDownload(Key, JarUrl,true);
            dl.Downloaded += (d) => {
                new FileInfo(LocalPath).Directory.Create();
                File.WriteAllBytes(LocalPath + "\\" + Key + ".jar", d.Data);
            };
            return dl;
        }

        public static MinecraftVersion fromJson(string json)
        {
            MinecraftVersion version = new MinecraftVersion();

            try
            {

                JObject obj = JObject.Parse(json);

                version.Key = (string)obj["id"];
                version.Type = (ReleaseType)Enum.Parse(typeof(ReleaseType), (string)obj["type"]);
                version.Arguments = (ProcessArguments)Enum.Parse(typeof(ProcessArguments), (string)obj["processArguments"]);
                version.MinecraftArguments = (string)obj["minecraftArguments"];
                version.minimumLauncherVersion = Convert.ToInt32((string)obj["minimumLauncherVersion"]);
                version.mainClass = (string)obj["mainClass"];
                version.Libraries = new List<Library>();

                foreach (JObject obj2 in obj["libraries"].Children<JObject>())
                {
                    Library lib = new Library();
                    lib.Name = (string)obj2["name"];
                    if (obj2["natives"] == null)
                        lib.IsNative = false;
                    else
                    {
                        lib.IsNative = true;
                        lib.ExtractExclusions = new List<string>();
                    }
                    if (obj2["extract"] != null)
                    {
                        foreach (JObject obj3 in obj2["extract"]["exclude"].Children<JObject>())
                        {
                            lib.ExtractExclusions.Add((string)obj3);
                        }
                    }
                    bool allow = true;
                    if (obj2["rules"] != null)
                    {
                        
                        foreach (JObject obj3 in obj2["rules"].Children<JObject>())
                        {
                            if (obj3["action"].ToString() == "allow" && obj3["os"] != null)
                            {
                                if (obj3["os"]["name"].ToString() == "windows")
                                    allow = true;
                                else
                                    allow = false;
                            }
                            else if (obj3["action"].ToString() == "disallow" && obj3["os"] != null)
                            {
                                if (obj3["os"]["name"].ToString() == "windows")
                                    allow = false;
                            }
                        }
                    }

                    if(allow)
                        version.Libraries.Add(lib);
                }

                return version;
            }
            catch (Exception ex)
            {
                App.Log("An error occured while loading Minecraft version from json");
                App.Log("Error: " + ex.ToString());
                return null;
            }
        }

        public string GetDescription()
        {
            return
                "Version: " + this.Key;
        }
    }
}
