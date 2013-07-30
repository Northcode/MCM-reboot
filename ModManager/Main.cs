using MCM.BackupFramework;
using MCM.MinecraftFramework;
using MCM.PluginAPI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ModManager
{
    class Main : IPlugin
    {
        public void Disable()
        { }

        public void Enable()
        {
            PluginManager.onCreateInstance += onCreateInstance;
            loadMods();
        }

        public string Name
        {
            get { return "Mod Manager"; }
        }


        public string Version
        {
            get { return "1.0.0"; }
        }

        public static string GetModsPath(Instance i) { return i.MinecraftDirPath + "\\mods"; }
        public static string GetCoreModsPath(Instance i) { return i.MinecraftDirPath + "\\coremods"; }
        public static string GetModJsonPath(Instance i) { return i.MinecraftDirPath + "\\mods.json"; }
        public static List<Mod> GetModList(Instance i)
        {
            List<Mod> mods = new List<Mod>();
            foreach (object obj in i.metaData)
            {
                if (obj.GetType() == typeof(List<Mod>))
                {
                    mods = (obj as List<Mod>);
                    break;
                }
            }
            return mods;
        }

        private void loadMods()
        {
            foreach (Instance i in InstanceManager.instances)
            {
                JObject json = JObject.Parse(File.ReadAllText(GetModJsonPath(i)));
                List<Mod> mods = new List<Mod>();

                foreach (JObject obj in json["mods"].Children<JObject>())
                {
                    TinyMinecraftVersion ver = null;
                    foreach(TinyMinecraftVersion v in VersionManager.versions)
                    {
                        if(v.Key == obj["version"].ToString())
                        {
                            ver = v;
                            break;
                        }
                    }
                    mods.Add(new Mod(
                        (Mod.ModType) Enum.Parse(typeof(Mod.ModType),obj["type"].ToString(),true),
                        (Mod.ModLevel) Enum.Parse(typeof(Mod.ModLevel), obj["level"].ToString(), true),
                        obj["name"].ToString(),
                        obj["path"].ToString(),
                        ver));
                }

                i.metaData.Add(mods);
            }
        }

        private void saveMods()
        {

            foreach (Instance i in InstanceManager.instances)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);

                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    writer.Formatting = Formatting.Indented;
                    writer.WriteComment("DO NOT EDIT THIS BY HAND WITHOUT KNOWING EXACTLY WHAT YOU ARE DOING" + Environment.NewLine + "THIS FILE IS CREATED BY MCM AND IS USED FOR KNOWING WHICH MODS ARE INSTALLED!");
                    writer.WriteStartObject();
                    writer.WritePropertyName("mods");
                    writer.WriteStartArray();
                    foreach (Mod mod in GetModList(i))
                    {
                        writer.WriteStartObject();
                        writer.WritePropertyName("name");
                        writer.WriteValue(mod.name);
                        writer.WritePropertyName("path");
                        writer.WriteValue(mod.path);
                        writer.WritePropertyName("type");
                        writer.WriteValue(mod.type);
                        writer.WritePropertyName("level");
                        writer.WriteValue(mod.level);
                        writer.WritePropertyName("version");
                        writer.WriteValue(mod.version.Key);
                        writer.WriteEndObject();
                    }
                    writer.WriteEnd();
                }
                File.WriteAllText(GetModJsonPath(i), sb.ToString());
            }
        }

        private void onCreateInstance(Instance instance)
        {
            Directory.CreateDirectory(GetModsPath(instance));
            Directory.CreateDirectory(GetCoreModsPath(instance));
        }
    }

    class BackupType : IBackup
    {
        public string Name
        {
            get { throw new NotImplementedException(); }
        }

        public TreeViewItem treeItem
        {
            get {
                
                TreeViewItem item = new TreeViewItem();
                item.Header = "Mods";
                item.MouseUp += item_MouseUp;
                foreach (Mod mod in Main.GetModList())
                {
                    TreeViewItem modItem = new TreeViewItem();
                    item.Header = mod.name;
                    item.Tag = mod;
                    item.MouseRightButtonUp += item_MouseRightButtonUp;
                    item.Items.Add(item);
                }
                return item;
            }
        }

        void item_MouseRightButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        void item_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
