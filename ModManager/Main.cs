using MCM;
using MCM.BackupFramework;
using MCM.Data;
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
        {
            saveMods();
        }

        public void Enable()
        {
            PluginManager.onCreateInstance += onCreateInstance;
            PluginManager.onMinecraftVersionsDownload += loadMods;
        }

        public string Name
        {
            get { return "Mod Manager"; }
        }


        public string Version
        {
            get { return "1.0.0"; }
        }

        public List<UIElement> GetConfigElemets()
        {
            List<UIElement> elements = new List<UIElement>();

            return elements;
        }

        public static string DataPath
        {
            get { return PathData.DataPath + "\\mods"; }
        }

        public static string ModsJson
        {
            get { return DataPath + "\\mods.json"; }
        }

        public static string GetModsPath(Instance i) { return i.MinecraftDirPath + "\\mods"; }
        public static string GetCoreModsPath(Instance i) { return i.MinecraftDirPath + "\\coremods"; }
        public static string GetModJsonPath(Instance i) { return i.MinecraftDirPath + "\\mods.json"; }
        public static List<Mod> BackuppedMods { get; set; }
        public static List<Mod> GetModList(Instance i)
        {
            List<Mod> mods = new List<Mod>();
            if (i.metaData != null)
            {
                foreach (object obj in i.metaData)
                {
                    if (obj.GetType() == typeof(List<Mod>))
                    {
                        mods = (obj as List<Mod>);
                        break;
                    }
                }
            }
            return mods;
        }
        public static void AddModToInstance(Instance i, Mod mod)
        {
            GetModList(i).Add(mod);
        }


        private void loadMods(params object[] parameters)
        {
            BackuppedMods = new List<Mod>();
            if (File.Exists(ModsJson))
            {
                foreach (Mod mod in JsonToMods(ModsJson))
                {
                    if(File.Exists(mod.path) || Directory.Exists(mod.path))
                        BackuppedMods.Add(mod);
                }
            }

            foreach (Instance i in InstanceManager.instances)
            {
                i.metaData.Add(JsonToMods(GetModJsonPath(i)));
            }
            App.InvokeAction(delegate
            {
                App.mainWindow.UpdateInstances();
            });
        }

        private List<Mod> JsonToMods(string path)
        {
            JObject json = JObject.Parse(File.ReadAllText(path));
            List<Mod> mods = new List<Mod>();

            foreach (JObject obj in json["mods"].Children<JObject>())
            {
                TinyMinecraftVersion ver = null;
                foreach (TinyMinecraftVersion v in VersionManager.versions)
                {
                    if (v.Key == obj["version"].ToString())
                    {
                        ver = v;
                        break;
                    }
                }
                mods.Add(new Mod(
                    (Mod.ModType)Enum.Parse(typeof(Mod.ModType), obj["type"].ToString(), true),
                    (Mod.ModLevel)Enum.Parse(typeof(Mod.ModLevel), obj["level"].ToString(), true),
                    obj["name"].ToString(),
                    obj["path"].ToString(),
                    ver));
            }

            return mods;
        }

        private void saveMods()
        {
            if (BackuppedMods == null)
                return;
            if (!Directory.Exists(DataPath))
                Directory.CreateDirectory(DataPath);
            File.WriteAllText(ModsJson, ModsToJson(BackuppedMods));

            foreach (Instance i in InstanceManager.instances)
            {
                File.WriteAllText(GetModJsonPath(i), ModsToJson(GetModList(i)));
            }
        }

        private void onCreateInstance(Instance instance)
        {
            Directory.CreateDirectory(GetModsPath(instance));
            Directory.CreateDirectory(GetCoreModsPath(instance));
        }

        private string ModsToJson(List<Mod> mods)
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
                foreach (Mod mod in mods)
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
            return sb.ToString();
        }


        public List<UIElement> GetConfigControls()
        {
            throw new NotImplementedException();
        }
    }

    class BackupType : IBackup
    {
        public string Name
        {
            get { throw new NotImplementedException(); }
        }

        public TreeViewItem treeItem(Instance i)
        {
            TreeViewItem item = new TreeViewItem();
            item.Header = "Mods";
            item.MouseUp += onClick_root;

            ContextMenu cmr = new ContextMenu();
            MenuItem mi_deleteAll = new MenuItem();
            mi_deleteAll.Header = "Remove all mods";
            mi_deleteAll.Click += delegate
            {
                throw new NotImplementedException();
            };
            cmr.Items.Add(mi_deleteAll);
            item.ContextMenu = cmr;
            List<Mod> mods = Main.GetModList(i);
            foreach (Mod mod in mods)
            {
                TreeViewItem modItem = new TreeViewItem();
                modItem.Header = mod.name;
                modItem.Tag = mod;

                ContextMenu cm = new ContextMenu();
                MenuItem item_delete = new MenuItem();
                item_delete.Header = "Delete";
                item_delete.Click += delegate
                {
                    TinyMinecraftVersion version = i.Version;
                    Mod.DeleteMod(mod, i);
                    mods.Remove(mod);
                    MCM.App.InvokeAction(delegate { MCM.App.mainWindow.UpdateInstances(); });
                };
                cm.Items.Add(item_delete);
                modItem.ContextMenu = cm;
                item.Items.Add(modItem);
            }
            return item;
        }

        void onClick_root(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Button bt_add = new Button();
            bt_add.Content = "Add Mod";
            bt_add.Tag = ((sender as TreeViewItem).Parent as TreeViewItem).Tag;
            bt_add.Click += bt_add_Click;

            MCM.App.InvokeAction(delegate
            {
                MCM.App.mainWindow.listBox_instanceInfo.Items.Clear();
                MCM.App.mainWindow.listBox_instanceInfo.Items.Add(bt_add);
            });
        }

        void bt_add_Click(object sender, RoutedEventArgs e)
        {
            ModSelector ms = new ModSelector(((sender as Button).Tag as Instance));
            ms.ShowDialog();
            App.mainWindow.UpdateInstances();
        }
    }
}
