using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Ionic.Zip;
using System.Windows.Forms;

namespace MCM.BackupFramework
{
    public class ResourcePack
    {
        public string name { get; set; }
        public ResourceInfo packInfo { get; set; }
        public string path { get; set; }

        public ResourcePack(string path)
        {
            this.path = path;
            this.name = Path.GetFileNameWithoutExtension(path);
            this.packInfo.parseJsonFromZip(path);
        }

        public void getPath()
        {
            getPath(" { insert backup dir here } ");
        }

        public void getPath(string packDir)
        {
            throw new NotImplementedException();
        }
    }

    public class ResourceInfo
    {
        public int packFormat { get; set; }
        public string desc { get; set; }

        public void parseJsonFromZip(string path)
        {
            Stream s = Stream.Null;
            using (ZipFile zip = ZipFile.Read(path))
            {
                ZipEntry e = zip["pack.mcmeta"];
                e.Extract(s);
            }
            MessageBox.Show(s.ToString());
        }

        public void parseJson(string json)
        {
            JObject obj = JObject.Parse(json);

            this.packFormat = (int)obj["pack"]["pack_format"];
            this.desc = (string)obj["pack"]["description"];
        }
    }
}
