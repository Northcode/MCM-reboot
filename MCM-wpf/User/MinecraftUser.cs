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

        public MinecraftUser() { }
        
        public MinecraftUser(string username, string displayname, string password_enc)
        {
            this.username = username;
            this.displayname = displayname;
            this.password_enc = password_enc;
        }


        public override string ToString()
        {
            return
                this.username + Environment.NewLine +
                this.displayname + Environment.NewLine +
                this.password_enc + Environment.NewLine;
        }

        public void loadFromStringArray(string[] data)
        {
            this.username = data[0];
            this.displayname = data[1];
            this.password_enc = data[2];
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
