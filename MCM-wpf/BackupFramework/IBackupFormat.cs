using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCM.BackupFramework
{
    interface IBackupFormat
    {
        byte Signature { get; set; }
        string DisplayName { get; set; }

        IBackup CreateNew();
        IBackup Load(string path);
        void Save(string path, IBackup backup);
    }
}
