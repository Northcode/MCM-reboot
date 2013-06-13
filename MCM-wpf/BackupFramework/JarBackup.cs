using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCM.BackupFramework
{
    class JarBackup : IBackup
    {
        string name { get; set; }
        

        public void Unpack()
        {
            throw new NotImplementedException();
        }

        public void Pack()
        {
            throw new NotImplementedException();
        }
    }
}
