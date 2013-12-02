using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCM.MinecraftFramework
{
    public class SessionInfoYggdrasil : SessionInfo
    {
        public string accessToken { get; private set; }
        public string clientToken { get; private set; }

        public static SessionInfo Connect(string Username, string Password)
        {

        }
    }
}
