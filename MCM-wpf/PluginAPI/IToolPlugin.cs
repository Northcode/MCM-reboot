using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCM.PluginAPI
{
    public interface IToolPlugin : IPlugin
    {
        string ToolKey { get; }
        Type[] Handles { get; }

        void LaunchTool(params object[] parameters);
    }
}
