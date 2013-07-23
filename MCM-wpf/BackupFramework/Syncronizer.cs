using MCM.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MCM.BackupFramework
{
    public class Syncronizer
    {
        public static void SyncOptions(Instance i)
        {
            if (SettingsManager.GetSetting("Sync options").data as string == "true")
            {
                App.Log("Syncing options from " + i.Name);
                string path = i.MinecraftDirPath + "\\options.txt";
                foreach (Instance instance in InstanceManager.instances)
                {
                    if (instance != i)
                    {
                        App.Log("Options synced to: " + instance.Name);
                        string opath = instance.MinecraftDirPath + "\\options.txt";
                        File.Copy(path, opath,true);
                        if(File.Exists(i.MinecraftDirPath + "\\optionsof.txt"))
                        {
                            File.Copy(i.MinecraftDirPath + "\\optionsof.txt",instance.MinecraftDirPath + "\\optionsof.txt",true);
                        }
                    }
                }
                App.Log("Options synced!");
            }
        }

        public static void SyncServerlist(Instance i)
        {
            if (SettingsManager.GetSetting("Sync serverlists").data as string == "true")
            {
                App.Log("Syncing serverlist from: " + i.Name);
                string path = i.MinecraftDirPath + "\\servers.dat";
                if (File.Exists(path))
                {
                    foreach (Instance instance in InstanceManager.instances)
                    {
                        if (instance != i)
                        {
                            App.Log("Serverlist synced to: " + instance.Name);
                            string ipath = instance.MinecraftDirPath + "\\servers.dat";
                            File.Copy(path, ipath, true);
                        }
                    }
                }
                App.Log("Serverlists synced!");
            }
        }
    }
}
