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

        public static void LoadPlugin(string Dll)
        {
            if (File.Exists(Dll))
            {
                Assembly asm = Assembly.LoadFile(Dll);

                Type iPlugin = typeof(IPlugin);
                Type iToolPlugin = typeof(IToolPlugin);

                foreach (Type type in asm.GetTypes())
                {
                    if (iToolPlugin.IsAssignableFrom(type))
                    {
                        IToolPlugin tool = Activator.CreateInstance(type) as IToolPlugin;
                        plugins.Add(tool);
                    }
                    else if (iPlugin.IsAssignableFrom(type))
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

        public static void LaunchTool(string key, params object[] parameters)
        {
            foreach (IToolPlugin tool in plugins)
	        {
                if (tool.ToolKey == key) { tool.LaunchTool(parameters); }
	        }
        }

        public static string GetToolHandlingArguments(params object[] parameters)
        {
            Type[] types = parameters.Select(o => o.GetType()).Cast<Type>().ToArray();
            foreach (IToolPlugin tool in plugins)
            {
                if (tool.Handles == types)
                {
                    return tool.ToolKey;
                }
            }
            return "";
        }
    }
}
