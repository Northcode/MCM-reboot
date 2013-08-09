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
            try
            {
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
                        current.type = (Setting._Type)Enum.Parse(typeof(Setting._Type), xmlr.GetAttribute("type").ToString(), true);
                        xmlr.Read();
                        switch (current.type)
                        {
                            case Setting._Type._string:
                                current.data = xmlr.ReadContentAsString();
                                break;
                            case Setting._Type._bool:
                                current.data = Convert.ToBoolean(xmlr.ReadContentAsString());
                                break;
                        }
                        settings.Add(current);
                    }
                }
                sr.Read();
            }
            catch (Exception e)
            {
                App.Log("Failed to load settings! Creating backup and loading default!" + Environment.NewLine + e.Message + Environment.NewLine + e.StackTrace);
                if (File.Exists(PathData.SettingsPath + ".backup"))
                    File.Delete(PathData.SettingsPath + ".backup");
                File.Move(PathData.SettingsPath, PathData.SettingsPath + ".backup");
                Load();
            }
        }

        public static void LoadList()
        {
            App.InvokeAction(delegate { App.mainWindow.lstSettings.Items.Clear(); });
            settings.ForEach((s) => {
                SettingItem si = new SettingItem();
                si.setting = s;
                si.SettingName.Content = s.name;
                switch (s.type)
                {
                    case Setting._Type._string:
                        si.SettingValue.Visibility = System.Windows.Visibility.Visible;
                        si.SettingValue.Content = s.data.ToString();
                        break;
                    case Setting._Type._bool:
                        si.SettingValueBool.Visibility = System.Windows.Visibility.Visible;
                        si.SettingValueBool.IsChecked = (bool)s.data;
                        break;
                }
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

        public static void AddDefault(string name, string group, object value, Setting._Type type)
        {
            if (!settings.Any((s) => s.name == name))
            {
                settings.Add(new Setting() { name = name, group = group, data = value, type = type });
            }
        }

    }
}
