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
        private static string userDataSavePath = PathData.DataPath + "\\users\\";

        public static void loadUsers()
        {
            if (Directory.Exists(userDataSavePath))
            {
                foreach (string file in Directory.GetFiles(userDataSavePath))
                {
                    MinecraftUser user = new MinecraftUser();
                    user.loadFromStringArray(File.ReadAllLines(file));
                    users.Add(user);
                }
            }
        }

        public static void saveUsers()
        {
            if (!Directory.Exists(userDataSavePath)) { Directory.CreateDirectory(userDataSavePath); }
            foreach (string file in Directory.GetFiles(userDataSavePath))
            {
                File.Delete(file);
            }

            foreach (MinecraftUser user in MinecraftUserData.users)
            {
                string suffix = "";
                int i = 0;
                while (File.Exists(userDataSavePath + user.username + suffix))
                {
                    suffix = "_" + i.ToString();
                    i++;
                }
                File.WriteAllText(userDataSavePath + user.username + suffix + ".udat", user.ToString());
            }
        }
    }
}
