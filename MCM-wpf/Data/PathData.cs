using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MCM.Data
{
    public static class PathData
    {
        public static string StartPath = Environment.CurrentDirectory;
        public static string AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.mcm2";
        public static string OldAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.mcm";
        public static string DocumentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MC Manager";

        public static string SettingsPath { get { return AppDataPath + "\\settings.xml"; } }
        public static string InstacesPath {get { return DocumentPath + "\\instaces"; }}

        public static void InitDirectories()
        {
            Directory.CreateDirectory(AppDataPath);
            Directory.CreateDirectory(DocumentPath);
            Directory.CreateDirectory(InstacesPath);
        }
    }
}
