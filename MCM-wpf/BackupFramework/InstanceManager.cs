using MCM.Data;
using MCM.MinecraftFramework;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
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
                foreach (JObject obj2 in obj.Children<JObject>())
                {
                    Instance i = new Instance();
                    i.Name = obj2["name"].ToString();
                    i.Description = obj2["desc"].ToString();
                    instances.Add(i);
                }
            }
        }

        public static void SaveInstances()
        {
            JObject obj = new JObject();
            int i = 0;
            foreach (Instance instance in instances)
            {
                obj[i] = instance.ToJson();
                i++;
            }

            File.WriteAllText(InstanceFile, obj.ToString());
        }
    }
}
