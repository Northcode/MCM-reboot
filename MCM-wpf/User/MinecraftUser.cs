using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCM.User
{
    public class MinecraftUser
    {
        public string username { get; set; }
        public string displayname { get; set; }
        public string password_enc { get; set; }

        public MinecraftUser(string username, string displayname, string password_enc)
        {
            this.username = username;
            this.displayname = displayname;
            this.password_enc = password_enc;
        }

        public static string encryptPwd(string password)
        {
            return password;
        }

        public static string decryptPwd(string encryptedData)
        {
            return encryptedData;
        }
    }
}
