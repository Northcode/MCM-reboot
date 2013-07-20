using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCM.PluginAPI
{
    public interface IPlugin
    {
        string Name { get; }

        void Enable();

        void Disable();
    }
}
