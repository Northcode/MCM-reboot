using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCM_reboot.MCVersions
{
    class MCVersion
    {
        public string name { get; set; }
        public string desc { get; set; }

        public enum mctypes { release, development, custom };
        public mctypes type { get; set; }

        public MCVersion()
        {
            
        }
    }
}
