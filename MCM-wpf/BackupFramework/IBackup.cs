using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCM.BackupFramework
{
    interface IBackup
    {
        string Name { get; set; }
        string Description { get; set; }

        /// <summary>
        /// XAML object displayed in the listbox
        /// </summary>
        object ListItem { get; }

        //If null, works with any version
        MCVersion McVersion { get; set; }

        /// <summary>
        /// Restore the backup
        /// </summary>
        void Unpack();

        /// <summary>
        /// Remove the backup from .minecraft
        /// </summary>
        void Pack();
    }
}
