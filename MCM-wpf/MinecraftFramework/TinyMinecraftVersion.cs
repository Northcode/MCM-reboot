using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCM.MinecraftFramework
{
    public class TinyMinecraftVersion
    {
        public string Key { get; set; }
        public ReleaseType Type { get; set; }

        public MinecraftVersion FullVersion
        {
            get
            {
                throw new NotImplementedException();
                if (this is MinecraftVersion)
                {
                    return this as MinecraftVersion;
                }
            }
        }
    }
}
