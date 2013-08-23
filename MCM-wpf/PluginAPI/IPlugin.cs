using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MCM.PluginAPI
{
    public interface IPlugin
    {
        string Name { get; }
        string Version { get; }

        void Enable();

        void Disable();

        List<UIElement> GetConfigElemets();
    }
}
