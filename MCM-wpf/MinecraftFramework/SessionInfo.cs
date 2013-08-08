using MCM.Utils;
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
                try
                {
                    if (DownloadManager.hasInternet)
                    {
                        Download dl = DownloadManager.ScheduleDownload("minecraft session info", String.Format(MinecraftData.MinecraftLoginUrl, Username, Password), true);
                        dl.WaitForComplete();
                        string result = Encoding.ASCII.GetString(dl.Data);
                        if (result.ToUpper() == "BAD LOGIN")
                        {
                            throw new Exception("Bad Login!");
                        }
                        string[] arguments = result.Split(':');
                        SessionInfo si = new SessionInfo();
                        si.username = arguments[2];
                        si.sessionid = arguments[3];
                        return si;
                    }
                    else
                    {
                        SessionInfo si = new SessionInfo();
                        si.username = Username;
                        si.sessionid = "";
                        return si;
                    }
                }
                catch (Exception ex)
                {
                    App.Log("An error occured while loging in to minecraft: " + ex.ToString());
                    throw ex;
                }
            }
            else
            {
                throw new Exception("Missing login info");
            }
        }
    }
}
