using MCM.Data;
using MCM.MinecraftFramework;
using MCM.PluginAPI;
using MCM.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MCM.BackupFramework
{
    public class Instance
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public TinyMinecraftVersion Version { get; set; }

        public List<object> metaData { get; set; }

        public string MinecraftDirPath { get { return this.Path + "\\minecraft"; } }
        public string ResourcePackDir { get { return this.MinecraftDirPath + "\\resourcepacks"; } }
        public string TexturePackDir { get { return this.MinecraftDirPath + "\\texturepacks"; } }
        public string SavesDir { get { return this.MinecraftDirPath + "\\saves"; } }
        public string MinecraftJarDir { get { return this.Path + "\\jar"; } }
        public string MinecraftJarFilePath { get { return MinecraftJarDir + String.Format("\\{0}.jar", Version.Key); } }

        private bool changingVersion;

        public Instance(string Name)
        {
            this.Name = Name;
            this.metaData = new List<object>();
            if (!Directory.Exists(this.Path))
                this.Create();
            PluginAPI.PluginManager.onCreateInstance(this);
        }

        public void Create()
        {
            Directory.CreateDirectory(this.Path);
            Directory.CreateDirectory(this.ResourcePackDir);
            Directory.CreateDirectory(this.TexturePackDir);
            Directory.CreateDirectory(this.SavesDir);
            Directory.CreateDirectory(this.MinecraftJarDir);
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

        public string GetStartArguments(string username,string password)
        {
            return this.Version.FullVersion.GetStartArguments(username, password, MinecraftJarFilePath, MinecraftDirPath);
        }

        private void CopyJar()
        {
            Directory.GetFiles(MinecraftJarDir).ToList().ForEach(jarfile => { File.Delete(jarfile); });
            File.Copy(this.Version.LocalPath + "\\" + this.Version.Key + ".jar", this.MinecraftJarFilePath);
        }

        public TreeViewItem GetTreeViewItem()
        {
            TreeViewItem node = new TreeViewItem();
            node.Header = this.Name;
            node.Tag = this;
            {
                ContextMenu cm = new ContextMenu();
                MenuItem item1 = new MenuItem() { Header = "Open instance in explorer" };
                item1.Click += delegate
                {
                    Process.Start(this.Path);
                };
                cm.Items.Add(item1);
                node.ContextMenu = cm;
            }

            // Minecraft version
            TreeViewItem mcVer = new TreeViewItem();
            mcVer.Header = "Minecraft version " + ((this.Version == null) ? "" : this.Version.Key);
            mcVer.MouseUp += (s, e) =>
            {
                Label tb = new Label();
                tb.Content = (this.Version == null ? "no version" : this.Version.Type.ToString() + " - " + this.Version.Key);
                Button bt = new Button();
                bt.Content = "Change version";
                bt.Click +=bt_Click;                
                App.mainWindow.listBox_instanceInfo.Items.Clear();
                App.mainWindow.listBox_instanceInfo.Items.Add(tb);
                App.mainWindow.listBox_instanceInfo.Items.Add(bt);
            };
            node.Items.Add(mcVer);

            foreach (IBackup b in PluginManager.backups)
            {
                node.Items.Add(b.treeItem(this));
            }
            node.ExpandSubtree();
            return node;
        }

        void bt_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (changingVersion)
            {
                MessageBox.ShowDialog("Warning", "The version is currently being changed/downloaded! Cannot change now!");
            }
            else
            {
                ChangeMCVersion cv = new ChangeMCVersion();
                if (cv.ShowDialog() == true)
                {
                    this.changingVersion = true;
                    TinyMinecraftVersion prevVer = this.Version;
                    this.Version = cv.version;
                    try
                    {
                        Task.Factory.StartNew(delegate
                        {
                            if (!File.Exists(cv.version.FullVersion.BinaryPath))
                            {

                                Download dl = cv.version.FullVersion.ScheduleJarDownload();
                                PluginAPI.PluginManager.onVersionDownload(dl);
                                DownloadPackage dp = new DownloadPackage("Libraries", true);
                                dp.ShouldContinue = true;
                                cv.version.FullVersion.Libraries.ForEach(l => { if (!File.Exists(l.Extractpath)) { l.ScheduleExtract(dp); } });
                                if (dp.getDownloads().Count > 0)
                                    DownloadManager.ScheduleDownload(dp);

                                dl.WaitForComplete();
                                CopyJar();
                                App.InvokeAction(delegate
                                {
                                    (App.mainWindow.listBox_instanceInfo.Items[0] as Label).Content = (this.Version == null ? "no version" : this.Version.Key);
                                    App.mainWindow.UpdateInstances();
                                });
                                this.changingVersion = false;

                            }
                            else
                            {
                                CopyJar();
                                App.InvokeAction(delegate
                                {
                                    (App.mainWindow.listBox_instanceInfo.Items[0] as Label).Content = (this.Version == null ? "no version" : this.Version.Key);
                                    App.mainWindow.UpdateInstances();
                                });
                                this.changingVersion = false;
                            }
                        });
                    }
                    catch (Exception ex)
                    {

                        MCM.Utils.MessageBox.ShowDialog("Error", "The selected version could not be changed because: " + ex.Message);
                        this.Version = prevVer;
                        (App.mainWindow.listBox_instanceInfo.Items[0] as Label).Content = (this.Version == null ? "no version" : this.Version.Key);
                        App.mainWindow.UpdateInstances();
                        this.changingVersion = false;
                    }
                }
                PluginManager.onChangeVersion(this);
            }
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
