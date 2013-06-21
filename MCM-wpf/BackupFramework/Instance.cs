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
                return PathData.InstacesPath + "\\" + this.Name;
            }
        }

        public List<ResourcePack> ResourcePacks
        {
            get
            {
                if (Directory.Exists(this.Path + "\\resourcepacks"))
                {
                    List<ResourcePack> packs = new List<ResourcePack>();
                    foreach (string file in Directory.GetFiles(this.Path + "\\resourcepacks"))
                    {
                        FileInfo finfo = new FileInfo(file);
                        if (finfo.Extension == ".zip")
                        {
                            ResourcePack pack = new ResourcePack(file);
                            packs.Add(pack);
                        }
                    }
                    return packs;
                }
                else
                {
                    return new List<ResourcePack>();
                }
            }
        }

        public string[] texturePacks
        {
            get
            {
                if (Directory.Exists(this.Path + "\\texturepacks"))
                {
                    return GetSubFilesAsStringArray(this.Path + "\\texturepacks");
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
                if (Directory.Exists(this.Path + "\\saves"))
                {
                    return GetSubDirectoriesAsStringArray(this.Path + "\\saves");
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
