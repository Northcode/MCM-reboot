using MCM.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace MCM.Settings
{
    class SettingsManager
    {
        static List<Setting> settings;

        public static Setting GetSetting(string name)
        {
            return settings.Find(s => s.name == name);
        }

        public static void Load()
        {
            if (!File.Exists(PathData.SettingsPath))
            {
                File.WriteAllText(PathData.SettingsPath, "<settings></settings>");
            }
            
            string xml = File.ReadAllText(PathData.SettingsPath);

            StringReader sr = new StringReader(xml);
            XmlReader xmlr = XmlReader.Create(sr);

            settings = new List<Setting>();

            Setting current = null;
            while (xmlr.Read())
            {
                if (xmlr.Name == "setting" && xmlr.NodeType == XmlNodeType.Element)
                {
                    current = new Setting();
                    current.name = xmlr.GetAttribute("name");
                    current.group = xmlr.GetAttribute("group");
                    xmlr.Read();
                    current.data = xmlr.ReadContentAsString();
                    settings.Add(current);
                }
            }
            sr.Read();
        }

        public static void LoadList()
        {
            App.InvokeAction(delegate { App.mainWindow.lstSettings.Items.Clear(); });
            settings.ForEach((s) => {
                SettingItem si = new SettingItem();
                si.setting = s;
                si.SettingName.Content = s.name;
                si.SettingValue.Content = s.data.ToString();
                App.InvokeAction(delegate {
                    App.mainWindow.lstSettings.Items.Add(si);
                });
            });
        }

        public static void Save()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<settings>");
            settings.ForEach((s) => { sb.Append(s.ToXML()); });
            sb.AppendLine("</settings>");
            File.WriteAllText(PathData.SettingsPath, sb.ToString());
        }

        public static void AddDefault(string name, string group, string value)
        {
            if (!settings.Any((s) => s.name == name))
            {
                settings.Add(new Setting() { name = name, group = group, data = value });
            }
        }

    }
}
