using MCM.BackupFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCM.Data
{
    class BackupData
    {
        public static List<IBackupFormat> formats {get; set; }

        public List<IBackup> backups { get; set; }

    }
}
