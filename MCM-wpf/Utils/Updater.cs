using MCM.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace MCM.Utils
{
    public static class Updater
    {
        public static string Version
        {
            get
            {
                return App.version;
            }
        }

        public static bool CheckForUpdate()
        {
            if (DownloadManager.hasInternet)
            {
                Download dl = DownloadManager.ScheduleDownload("MCM update check", "https://github.com/Northcode/MCM-reboot/blob/dev/MCM-wpf/VersionInformation/ver.txt?raw=true", false);
                dl.WaitForComplete();
                string Uver = Encoding.Default.GetString(dl.Data.Skip(3).ToArray());
                if (Uver != Version)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
