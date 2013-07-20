using MCM.Data;
using MCM.MinecraftFramework;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Forms;

namespace MCM.BackupFramework
{
    public class Instance
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public MinecraftVersion Version { get; set; }
        public ModPack mods { get; set; }

        private string ResourcePackDir { get { return this.Path + "\\resourcepacks"; } }
        private string TexturePackDir { get { return this.Path + "\\texturepacks"; } }
        private string SavesDir { get { return this.Path + "\\saves"; } }

        public Instance(string Name)
        {
            this.Name = Name;
            if (!Directory.Exists(this.Path))
                this.Create();
        }

        public void Create()
        {
            Directory.CreateDirectory(this.Path);
            Directory.CreateDirectory(this.ResourcePackDir);
            Directory.CreateDirectory(this.TexturePackDir);
            Directory.CreateDirectory(this.SavesDir);
        }

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
                if (Directory.Exists(this.ResourcePackDir))
                {
                    List<ResourcePack> packs = new List<ResourcePack>();
                    foreach (string file in Directory.GetFiles(this.ResourcePackDir))
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
                if (Directory.Exists(this.TexturePackDir))
                {
                    return GetSubFilesAsStringArray(this.TexturePackDir);
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
                if (Directory.Exists(this.SavesDir))
                {
                    return GetSubDirectoriesAsStringArray(this.SavesDir);
                }
                else
                {
                    return new string[0];
                }
            }
        }

        public TreeViewItem GetTreeViewItem()
        {
            TreeViewItem node = new TreeViewItem();
            node.Header = this.Name;
            node.Tag = this;

            // Minecraft version
            TreeViewItem mcVer = new TreeViewItem();
            mcVer.Header = "Minecraft version " + ((this.Version == null) ? "" : this.Version.Key);
            mcVer.Tag = "mcver";
            node.Items.Add(mcVer);

            // Modpack
            TreeViewItem modPack = new TreeViewItem();
            modPack.Header = "Mods";
            if (this.mods != null)
            {
                foreach (Mod mod in this.mods.Mods)
                {
                    TreeViewItem thisMod = new TreeViewItem();
                    thisMod.Header = mod.Name;
                    thisMod.Tag = mod;
                    modPack.Items.Add(thisMod);
                }
            }
            node.Items.Add(modPack);

            // ResourcePacks
            TreeViewItem resPack = new TreeViewItem();
            resPack.Header = "Resource Packs";
            foreach (ResourcePack pack in this.ResourcePacks)
            {
                TreeViewItem thisPack = new TreeViewItem();
                thisPack.Header = pack.name;
                thisPack.ToolTip = pack.packInfo.desc;
                thisPack.Tag = pack;
                resPack.Items.Add(thisPack);
            }
            node.Items.Add(resPack);

            // Texturepacks
            TreeViewItem texturePack = new TreeViewItem();
            texturePack.Header = "Texture Packs";
            foreach (string pack in this.texturePacks)
            {
                TreeViewItem thisPack = new TreeViewItem();
                thisPack.Header = pack;
                texturePack.Items.Add(thisPack);
            }
            node.Items.Add(texturePack);

            // World Saves
            TreeViewItem worldSave = new TreeViewItem();
            worldSave.Header = "World Saves";
            foreach (string save in this.Saves)
            {
                TreeViewItem thisSave = new TreeViewItem();
                thisSave.Header = System.IO.Path.GetFileName(save);
                thisSave.Tag = save;
                worldSave.Items.Add(thisSave);
            }
            node.Items.Add(worldSave);

            node.ExpandSubtree();
            return node;
        }

        private string[] GetSubFilesAsStringArray(string path)
        {
            return new DirectoryInfo(path).GetFiles().ToList().ConvertAll<string>((f) => { return f.FullName; }).ToArray();
        }

        private string[] GetSubDirectoriesAsStringArray(string path)
        {
            return new DirectoryInfo(path).GetDirectories().ToList().ConvertAll<string>((f) => { return f.FullName; }).ToArray();
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
