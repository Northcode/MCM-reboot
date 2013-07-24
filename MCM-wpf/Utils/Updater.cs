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
                return Properties.Resources.ver;
            }
        }

        public static bool CheckForUpdate()
        {
            if (DownloadManager.hasInternet)
            {
                Download dl = DownloadManager.ScheduleDownload("MCM update check", "https://github.com/Northcode/MCM-reboot/blob/dev/MCM-wpf/VersionInformation/ver.txt?raw=true", false);
                dl.WaitForComplete();
                string Uver = Encoding.ASCII.GetString(dl.Data);
                if (Uver != Version)
                {
                    Download udl = DownloadManager.ScheduleDownload("MCM updater", "https://github.com/Northcode/MCM-reboot/blob/dev/Setup/MC%20Manager.msi?raw=true", false);
                    udl.WaitForComplete();
                    File.WriteAllBytes(PathData.UpdaterPath,udl.Data);
                    Process.Start(PathData.UpdaterPath);
                    return true;
                }
            }
            return false;
        }
    }
}
