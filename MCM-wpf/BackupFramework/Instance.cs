using MCM.Data;
using MCM.MinecraftFramework;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MCM.BackupFramework
{
    public class Instance
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public MinecraftVersion Version { get; set; }
        public ModPack mods { get; set; }

        public string Path
        {
            get
            {
                return PathData.InstancesPath + "\\" + this.Name;
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

        public TreeNode GetTreeViewNode()
        {
            TreeNode node = new TreeNode();
            node.Text = this.Name;

            // Minecraft version
            TreeNode mcVer = new TreeNode("Minecraft version");
            node.Nodes.Add(mcVer);

            // Modpack
            TreeNode modPack = new TreeNode("Mods");
            foreach (Mod mod in this.mods.Mods)
            {
                TreeNode thisMod = new TreeNode(mod.Name);
                thisMod.Tag = mod;
                modPack.Nodes.Add(thisMod);
            }
            node.Nodes.Add(modPack);

            // ResourcePacks
            TreeNode resPack = new TreeNode("Resource Packs");
            foreach (ResourcePack pack in this.ResourcePacks)
            {
                TreeNode thisPack = new TreeNode(pack.name);
                thisPack.ToolTipText = pack.packInfo.desc;
                thisPack.Tag = pack;
                resPack.Nodes.Add(thisPack);
            }
            node.Nodes.Add(resPack);

            // Texturepacks
            TreeNode texturePack = new TreeNode("Texture Packs");
            foreach (string pack in this.texturePacks)
            {
                texturePack.Nodes.Add(pack);
            }
            node.Nodes.Add(texturePack);

            // World Saves
            TreeNode worldSave = new TreeNode("World Saves");
            foreach (string save in this.Saves)
            {
                worldSave.Nodes.Add(save);
            }
            node.Nodes.Add(worldSave);

            return node;
        }

        private string[] GetSubFilesAsStringArray(string path)
        {
            return new DirectoryInfo(path).GetFiles().ToList().ConvertAll<string>((f) => { return f.FullName; }).ToArray();
        }

        private string[] GetSubDirectoriesAsStringArray(string path)
        {
            return new DirectoryInfo(path).GetFiles().ToList().ConvertAll<string>((f) => { return f.FullName; }).ToArray();
        }

        public JObject ToJson()
        {
            JObject obj = new JObject();
            obj["name"] = this.Name;
            obj["desc"] = this.Description;

            return obj;
        }
    }
}
