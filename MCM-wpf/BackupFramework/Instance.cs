using MCM.Data;
using MCM.MinecraftFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MCM.BackupFramework
{
    public class Instance
    {
        public string Name { get; set; }
        public MinecraftVersion Version { get; set; }
        public ModPack mods { get; set; }

        public string Path
        {
            get
            {
                return PathData.InstacesPath + "\\" + Name;
            }
        }

        public string[] ResourcePacks
        {
            get
            {
                if (Directory.Exists(Path + "\\texturepacks"))
                {
                    return GetSubFilesAsStringArray(Path + "\\texturepacks");
                }
                else if (Directory.Exists(Path + "\\resourcepacks"))
                {
                    return GetSubFilesAsStringArray(Path + "\\resourcepacks");
                }
                else
                {
                    return new string[0];
                }
            }
        }

        public string[] Saves
        {
            get
            {
                if (Directory.Exists(Path + "\\saves"))
                {
                    return GetSubDirectoriesAsStringArray(Path + "\\saves");
                }
                else
                {
                    return new string[0];
                }
            }
        }

        private string[] GetSubFilesAsStringArray(string path)
        {
            return new DirectoryInfo(path).GetFiles().ToList().ConvertAll<string>((f) => { return f.FullName; }).ToArray();
        }

        private string[] GetSubDirectoriesAsStringArray(string path)
        {
            return new DirectoryInfo(path).GetFiles().ToList().ConvertAll<string>((f) => { return f.FullName; }).ToArray();
        }

    }
}
