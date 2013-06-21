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
        public static string DataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.mcm2";
        public static string OldAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.mcm";
        public static string DocumentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MC Manager";

<<<<<<< HEAD
<<<<<<< HEAD
        public static string SettingsPath { get { return AppDataPath + "\\settings.xml"; } }
        public static string InstacesPath {get { return DocumentPath + "\\instaces"; }}
=======
        public static string SettingsPath { get { return DataPath + "\\settings.xml"; } }
>>>>>>> 9da41f3c3ca6ca34ab3b82757248cf2dd3fc5914
=======
        public static string SettingsPath { get { return DataPath + "\\settings.xml"; } }
>>>>>>> 9da41f3c3ca6ca34ab3b82757248cf2dd3fc5914

        public static void InitDirectories()
        {
            Directory.CreateDirectory(DataPath);
            Directory.CreateDirectory(DocumentPath);
            Directory.CreateDirectory(InstacesPath);
        }
    }
}
