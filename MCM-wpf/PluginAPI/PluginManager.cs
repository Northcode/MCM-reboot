using MCM.BackupFramework;
using MCM.Data;
using MCM.MinecraftFramework;
using MCM.Settings;
using MCM.User;
using MCM.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MCM.PluginAPI
{
    public class PluginManager
    {
        public static List<IPlugin> plugins = new List<IPlugin>();
        public static List<IBackup> backups = new List<IBackup>();

        public delegate void PluginEvent(params object[] parameters);

        public delegate void PluginEvent_Minecraft(Instance instance, MinecraftUser user);
        public delegate void PluginEvent_Instance(Instance instance);
        public delegate void PluginEvent_Download(Download download);
        public delegate void PluginEvent_Setting(Setting setting);
        public delegate void PluginEvent_Plugin(IPlugin plugin, params object[] parameters);

        public static PluginEvent onMinecraftVersionsDownload = dummy;
        public static PluginEvent_Minecraft onStartMinecraft = dummyA;
        public static PluginEvent_Minecraft onCloseMinecraft = dummyA;
        public static PluginEvent_Instance onCreateInstance = dummyB;
        public static PluginEvent_Instance onRemoveInstance = dummyB;
        public static PluginEvent_Instance onRenameInstance = dummyB;
        public static PluginEvent_Instance onChangeVersion = dummyB;
        public static PluginEvent_Download onVersionDownload = dummyC;
        public static PluginEvent_Plugin onPluginEvent = dummyE;
        public static PluginEvent_Download onNewsDownload = dummyC;
        public static PluginEvent_Setting onSettingChange = dummyD;

        static void dummy(object[] pars) { }
        static void dummyA(Instance instance, MinecraftUser user) { }
        static void dummyB(Instance instance) { }
        static void dummyC(Download download) { }
        static void dummyD(Setting setting) { }
        static void dummyE(IPlugin plugin, params object[] parameters) { }


        public static void LoadPlugin(string Dll)
        {
            try
            {
                if (File.Exists(Dll))
                {
                    Assembly asm = Assembly.LoadFile(Dll);

                    Type iPlugin = typeof(IPlugin);
                    Type iBackup = typeof(IBackup);

                    foreach (Type type in asm.GetTypes())
                    {
                        if (iPlugin.IsAssignableFrom(type))
                        {
                            IPlugin plugin = Activator.CreateInstance(type) as IPlugin;
                            plugins.Add(plugin);

                        }
                        if (iBackup.IsAssignableFrom(type))
                        {
                            IBackup backup = Activator.CreateInstance(type) as IBackup;
                            backups.Add(backup);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                App.Log("Error while loading plugin: " + Path.GetFileNameWithoutExtension(Dll));
                App.Log(e.Message);
            }
        }

        public static IPlugin GetPlugin(string name)
        {
            foreach (IPlugin plugin in plugins)
            {
                if (plugin.Name == name)
                {
                    return plugin;
                }
            }
            throw new Exception("Plugin not found: " + name);
        }

        public static void EnablePlugins()
        {
            foreach (IPlugin pl in plugins)
            {
                pl.Enable();
                App.Log(pl.Name + " version " + pl.Version + " has been loaded");
            }
        }

        public static void DisablePlugins()
        {
            foreach (IPlugin pl in plugins)
            {
                pl.Disable();
                App.Log(pl.Name + " version " + pl.Version + " has been disabled");
            }
        }

        internal static void LoadPlugins()
        {
            string[] files = Directory.GetFiles(PathData.PluginsPath, "*.dll");
            foreach (string dll in files)
            {
                LoadPlugin(dll);
            }
        }
    }
}
