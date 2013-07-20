using MCM.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace MCM.MinecraftFramework
{
    public class MinecraftAssetManager
    {
        public static List<MinecraftAsset> assets = new List<MinecraftAsset>();

        public static void LoadAssets()
        {
            DownloadPackage dlp = new DownloadPackage("Assets XML","Minecraft Assets", true);
            dlp.ScheduleDownload("Assets XML", MinecraftData.AssetsUrl);
            string xml = "";
            dlp.Downloaded += delegate
            {
                Download d = dlp.GetDownload("Assets XML");
                xml = Encoding.ASCII.GetString(d.Data);

                StringReader sr = new StringReader(xml);
                XmlReader xmlr = XmlReader.Create(sr);

                MinecraftAsset current = null;

                try
                {
                    while (xmlr.Read())
                    {
                        if (xmlr.Name == "Contents" && xmlr.NodeType == XmlNodeType.Element)
                        {
                            current = new MinecraftAsset();
                        }
                        else if (xmlr.Name == "Key" && current != null && xmlr.NodeType == XmlNodeType.Element)
                        {
                            xmlr.Read();
                            current.Key = xmlr.ReadContentAsString();
                        }
                        else if (xmlr.Name == "ETag" && current != null && xmlr.NodeType == XmlNodeType.Element)
                        {
                            xmlr.Read();
                            current.md5 = xmlr.ReadContentAsString();
                            if (current.md5.StartsWith("\""))
                            {
                                current.md5 = current.md5.Replace("\"", "");
                            }
                        }
                        else if (xmlr.Name == "Contents" && xmlr.NodeType == XmlNodeType.EndElement)
                        {
                            assets.Add(current);
                        }
                    }
                    ScheduleAssetDownloads();
                }
                catch (Exception ex)
                {
                    App.Log("An error occured while parsing assets xml: " + ex.ToString());
                    throw ex;
                }
                finally
                {
                    sr.Close();
                }
            };
        }

        internal static void ScheduleAssetDownloads()
        {
            DownloadPackage dlp = new DownloadPackage("Assets", "Minecraft Assets/Resources", true);
            List<MinecraftAsset> needDl = new List<MinecraftAsset>();
            assets.ForEach(a => {
                if (a.NeedsDownload())
                {
                    needDl.Add(a);
                    Download dl = dlp.ScheduleDownload(a.Key, a.Url);
                    dl.Downloaded += AssetDownloaded;
                }
            });
        }

        private static void AssetDownloaded(Download dl)
        {
            MinecraftAsset asset = assets.Find(a => a.Url == dl.Url);
            asset.StoreAsset(dl);
        }
    }
}
