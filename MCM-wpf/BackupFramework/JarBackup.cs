using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCM.BackupFramework
{
    class JarBackup : IBackup
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public string Path { get; set; }

        public MCVersion McVersion { get; set; }

        public void Unpack()
        {
            throw new NotImplementedException();
        }

        public void Pack()
        {
            throw new NotImplementedException();
        }


        public object ListItem
        {
            get { throw new NotImplementedException(); }
        }
    }
}
