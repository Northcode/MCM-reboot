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

    }
}
