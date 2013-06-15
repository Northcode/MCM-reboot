using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCM.MinecraftFramework
{
    public class MinecraftVersion
    {
        public string Key { get; set; }
        public ReleaseType Type { get; set; }
        public ProcessArguments Arguments { get; set; }
        public string MinecraftArguments { get; set; }
        public int minimumLauncherVersion { get; set; }
        public string mainClass { get; set; }
        public Library[] Liberaries { get; set; }
    }
}
