using MCM.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace MCM.MinecraftFramework
{
    public class MinecraftAssetManager
    {
        public static List<MinecraftAsset> assets = new List<MinecraftAsset>();

        public static void LoadAssets()
        {
            Download dl = DownloadManager.ScheduleDownload("Assets XML", MinecraftData.AssetsUrl);
            string xml = "";
            dl.Downloaded += (d) =>
            {
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
                }
                catch (Exception ex)
                {
                    App.Log("An error occured while parsing assets xml: " + ex.ToString());
                }
                finally
                {
                    sr.Close();
                    DownloadAssets();
                }
            };
            dl.DoDownload();
        }

        internal static void DownloadAssets()
        {
            assets.ForEach(a => { a.ScheduleDownload(); });
            DownloadManager.DownloadAll();
        }
    }
}
