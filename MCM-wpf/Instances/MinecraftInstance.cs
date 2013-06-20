using MCM.MinecraftFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCM.Instances
{
    class MinecraftInstance
    {
        public MinecraftVersion mcVersion { get; set; }
        public List<ResourcePack> resourcePacks { get; set; }

        /* Following is not implemented yet...
         * public MinecraftSettings settings { get; set; }
         * public ServerList serverlist { get; set; }
         * public List<WorldSave> worldSaves { get; set; }
         * public List<ModloaderMod> mods { get; set; }
         * public List<ModloaderCoreMod> coremods { get; set; }
         */
    }
}
