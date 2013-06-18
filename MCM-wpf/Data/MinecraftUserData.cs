using MCM.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCM.Data
{
    class MinecraftUserData
    {
        public static List<MinecraftUser> users = new List<MinecraftUser>();

        public static string usersToXml(List<MinecraftUser> users)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<Users>");

            foreach (MinecraftUser user in users)
            {
                sb.AppendLine(String.Format("\t<username value=\"{0}\" />",user.username));
                sb.AppendLine(String.Format("\t<displayname value=\"{0}\" />", user.displayname));
                sb.AppendLine(String.Format("\t<password value=\"{0}\" />", user.password_enc));
            }

            sb.AppendLine("</Users>");

            return sb.ToString();
        }
    }
}
