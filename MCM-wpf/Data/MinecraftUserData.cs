using MCM.User;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace MCM.Data
{
    class MinecraftUserData
    {
        public static List<MinecraftUser> users = new List<MinecraftUser>();
        private static string userDataSavePath = PathData.AppDataPath + "\\users.xml";

        private static string usersToXml(List<MinecraftUser> users)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<Users>");

            foreach (MinecraftUser user in users)
            {
                sb.AppendLine("\t<User>");
                sb.AppendLine(String.Format("\t\t<username value=\"{0}\" />",user.username));
                sb.AppendLine(String.Format("\t\t<displayname value=\"{0}\" />", user.displayname));
                sb.AppendLine(String.Format("\t\t<password value=\"{0}\" />", user.password_enc));
                sb.AppendLine("\t</User>");
            }

            sb.AppendLine("</Users>");

            return sb.ToString();
        }

        private static List<MinecraftUser> xmlToUsers(FileStream xmlfs)
        {
            List<MinecraftUser> usr = new List<MinecraftUser>();
            XmlReader xmlr = XmlReader.Create(xmlfs);
            MinecraftUser current = null;

            while (xmlr.Read())
            {
                

                if (xmlr.Name == "User" && xmlr.NodeType == XmlNodeType.Element)
                {
                    current = new MinecraftUser(null,null,null);
                }
                else if (xmlr.Name == "username")
                {
                    current.username = xmlr.GetAttribute("value");
                }
                else if (xmlr.Name == "displayname")
                {
                    current.displayname = xmlr.GetAttribute("value");
                }
                else if (xmlr.Name == "password")
                {
                    current.password_enc = xmlr.GetAttribute("value");
                }
                else if (xmlr.Name == "User" && xmlr.NodeType == XmlNodeType.EndElement)
                {
                    usr.Add(current);
                    current = null;
                }
            }

            return usr;
        }


        public static void loadUsers()
        {
            if (File.Exists(userDataSavePath))
            {
                FileStream fs = new FileStream(userDataSavePath, FileMode.Open);
                users = xmlToUsers(fs);
                fs.Close();
            }
        }

        public static void saveUsers()
        {
            File.WriteAllText(userDataSavePath, usersToXml(users));
        }
    }
}
