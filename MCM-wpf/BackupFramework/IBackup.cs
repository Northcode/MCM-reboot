using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCM.BackupFramework
{
    interface IBackup
    {
        void Unpack();

        void Pack();
    }
}
