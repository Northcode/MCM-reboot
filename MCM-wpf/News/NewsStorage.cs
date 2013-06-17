using MCM.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MCM.News
{
    public static class NewsStorage
    {
        public static string NewsPath = PathData.AppDataPath + "\\rss";
        public static string MojangFeedPath = NewsPath + "\\mojang.xml";

        public static void InitDirectories()
        {
            Directory.CreateDirectory(NewsPath);
        }

        public static bool MojangFeedExists
        {
            get
            {
                return File.Exists(MojangFeedPath);
            }
        }
    }
}
