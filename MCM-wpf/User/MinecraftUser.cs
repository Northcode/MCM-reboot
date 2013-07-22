using MCM.Utils;
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
        public string password
        {
            get
            {
                return decryptPwd(password_enc);
            }
            set
            {
                password_enc = encryptPwd(value);
            }
        }
        private string password_enc { get; set; }

        private static string encryptionKey = "Tfih1DMvNXws05G679xeCX+G+ofKjZa1JUu1vOho0f/t";

        public MinecraftUser() { }
        
        public MinecraftUser(string username, string displayname, string password)
        {
            this.username = username;
            this.displayname = displayname;
            this.password = password;
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
            return Crypto.EncryptStringAES(password, encryptionKey);
        }

        public static string decryptPwd(string encryptedData)
        {
            return Crypto.DecryptStringAES(encryptedData, encryptionKey);
        }
    }
}
