using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCM.Settings
{
    class Settings
    {
        public string group;
        public string name;
        public Object data;

        public Settings(string group, string name, Object data)
        {
            this.group = group;
            this.name = name;
            this.data = data;
        }
    }
}
