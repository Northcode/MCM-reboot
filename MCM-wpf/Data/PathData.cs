﻿using System;
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
        public static string LogPath = DataPath + "\\logs";

        public static string InstancesPath {get { return DataPath + "\\instances"; }}
        public static string SettingsPath { get { return DataPath + "\\settings.xml"; } }

        public static void InitDirectories()
        {
            Directory.CreateDirectory(DataPath);
            Directory.CreateDirectory(DocumentPath);
            Directory.CreateDirectory(InstancesPath);
            Directory.CreateDirectory(LogPath);
        }
    }
}
