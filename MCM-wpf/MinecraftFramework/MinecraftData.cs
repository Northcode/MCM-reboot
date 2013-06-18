using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCM.MinecraftFramework
{
    public class MinecraftData
    {
        public static string AppdataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.minecraft";
        public static string LibraryPath = AppdataPath + "\\libraries";
        public static string BinPath = AppdataPath + "\\bin";
        public static string AssetsPath = AppdataPath + "\\assets";
        public static string NativesPath = BinPath + "\\natives";
        public static string VersionsPath = Data.PathData.AppDataPath + "\\versions";

        public const string AssetsUrl = "https://s3.amazonaws.com/Minecraft.Resources/";
        public const string LibraryUrl = "https://s3.amazonaws.com/Minecraft.Download/libraries/";
        public const string VersionsUrl = "http://s3.amazonaws.com/Minecraft.Download/versions/versions.json";

        public static string MinecraftRAM = "1G";

        /// <summary>
        /// USE STRING.FORMAT! {0} is username, {1} is password!
        /// </summary>
        public const string MinecraftLoginUrl = "http://login.minecraft.net/?user={0}&password={1}&version=14";

        public static SessionInfo currentSession { get; set; }
        public static MinecraftVersion selectedVersion { get; set; }

    }
}
