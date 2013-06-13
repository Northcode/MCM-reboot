using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCM_reboot.MCVersions;

namespace MCM_reboot.WorldSaves
{
    class WorldSave
    {
        public string name { get; set; }
        public string path { get; set; }
        public string author { get; set; }
        public string seed { get; set; }

        public MCVersion mcversion { get; set; }
        public DateTime lastused { get; set; }

        public WorldSave()
        {

        }
    }
}
