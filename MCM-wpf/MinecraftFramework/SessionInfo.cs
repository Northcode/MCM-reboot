using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace MCM.MinecraftFramework
{
    public class SessionInfo
    {
        public string username { get; private set; }
        public string sessionid { get; private set; }

        public static SessionInfo Connect(string Username, string Password)
        {
            if (Username != null && Password != null)
            {
                WebClient wc = new WebClient();
                string result = wc.DownloadString(String.Format(MinecraftData.MinecraftLoginUrl, Username, Password));
                if (result == "Bad Login")
                {
                    throw new Exception("Bad Login!");
                }
                string[] arguments = result.Split(':');

                SessionInfo si = new SessionInfo();
                si.username = Username;
                si.sessionid = arguments[3];

                return si;
            }
            else
            {
                throw new Exception("Missing login info");
            }
        }
    }
}
