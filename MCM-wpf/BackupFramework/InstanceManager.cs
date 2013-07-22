using MCM.Data;
using MCM.MinecraftFramework;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Windows.Controls;
namespace MCM.BackupFramework
{
    public static class InstanceManager
    {
        public static List<Instance> instances = new List<Instance>();
        private static string InstanceFile = PathData.InstancesPath + "\\instances.json";

        public static void LoadInstances()
        {
            if (File.Exists(InstanceFile))
            {
                string data = File.ReadAllText(InstanceFile);
                JObject obj = JObject.Parse(data);
                foreach (JObject obj2 in obj["instances"].Children<JObject>())
                {
                    Instance i = new Instance(obj2["name"].ToString());
                    i.Description = obj2["desc"].ToString();
                    i.Version = VersionManager.versions.Find(ver => obj2["version"].ToString() == ver.Key);
                    instances.Add(i);
                }
            }

            string[] dirs = Directory.GetDirectories(PathData.InstancesPath);
            foreach (string s in dirs)
            {
                Instance i = instances.Find(In => In.Path == s);
                if (i == null)
                {
                    Instance newI = new Instance(Path.GetFileName(s));
                    instances.Add(newI);
                }
            }

            App.InvokeAction(delegate
            {
                App.mainWindow.UpdateInstances();
            });
        }

        public static void SaveInstances()
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;
                writer.WriteStartObject();
                writer.WritePropertyName("instances");
                writer.WriteStartArray();

                foreach (Instance instance in instances)
                {
                    writer.WriteStartObject();
                    writer.WritePropertyName("name");
                    writer.WriteValue(instance.Name);
                    writer.WritePropertyName("desc");
                    writer.WriteValue(instance.Description);
                    writer.WritePropertyName("version");
                    writer.WriteValue((instance.Version == null ? "" : instance.Version.Key));
                    writer.WriteEndObject();
                }

                writer.WriteEnd();
            }

            File.WriteAllText(InstanceFile, sb.ToString());
        }

        public static Instance GetSelectedInstance(Control selectedItem)
        {
            foreach (Instance i in instances)
            {
                if (i == selectedItem.Tag)
                    return i;
            }
            throw new Exception("Selected Instance not found");
        }

        public static void DeleteInstance(Instance instance)
        {
            if (Directory.Exists(instance.Path))
                Directory.Delete(instance.Path,true);
            instances.Remove(instance);
        }
    }
}
