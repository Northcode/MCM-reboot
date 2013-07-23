﻿using MCM.BackupFramework;
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
        static List<IPlugin> plugins = new List<IPlugin>();

        public delegate void PluginEvent(params object[] parameters);

        public delegate void PluginEvent_Minecraft(Instance instance, MinecraftUser user);
        public delegate void PluginEvent_Instance(Instance instance);
        public delegate void PluginEvent_Download(Download download);
        public delegate void PluginEvent_Setting(Setting setting);
        public delegate void PluginEvent_Plugin(IPlugin plugin, params object[] parameters);

        public static PluginEvent_Minecraft onStartMinecraft = dummyA;
        public static PluginEvent_Minecraft onCloseMinecraft = dummyA;
        public static PluginEvent_Instance onCreateInstance = dummyB;
        public static PluginEvent_Instance onRemoveInstance = dummyB;
        public static PluginEvent_Instance onRenameInstance = dummyB;
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
            if (File.Exists(Dll))
            {
                Assembly asm = Assembly.LoadFile(Dll);

                Type iPlugin = typeof(IPlugin);

                foreach (Type type in asm.GetTypes())
                {
                    if (iPlugin.IsAssignableFrom(type))
                    {
                        IPlugin plugin = Activator.CreateInstance(type) as IPlugin;
                    }
                }
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
            plugins.ForEach(p => p.Enable());
        }

        public static void DisablePlugins()
        {
            plugins.ForEach(p => p.Disable());
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
