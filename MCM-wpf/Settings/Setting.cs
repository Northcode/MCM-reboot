using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCM.Settings
{
    public class Setting
    {
        public string group { get; set; }
        public string name { get; set; }
        public object data { get; set; }

        public string ToXML()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(String.Format("<setting name=\"{0}\" group=\"{1}\">", name, group));
            sb.Append(data.ToString());
            sb.AppendLine("</setting>");
            return sb.ToString();
        }
    }
}
