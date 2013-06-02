using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace MCM_reboot
{
    class Config
    {
        public string key { get; set; }
        public string value { get; set; }

        public Config(string key, string value)
        {
            this.key = key;
            this.value = value;
        }

        public string configToXml(Config con, string tabIndex)
        {
            return String.Format("{0}<config key=\"{1}\">{2}</config>",tabIndex,con.key,con.value);
        }

        public string configlistToXml(List<Config> configs)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<Configuration>");
            foreach (Config c in configs)
            {
                sb.AppendLine(configToXml(c,"\t"));
            }
            sb.AppendLine("</Configuration>");
            return sb.ToString();
        }

        public List<Config> xmlToConfiglist(string xml)
        {
            throw new NotImplementedException();
            XmlReader xmlr = XmlReader.Create(new StringReader(xml));
            while (xmlr.Read())
            {
                
            }
            return null;
        }
    }
}
