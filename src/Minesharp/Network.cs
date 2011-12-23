using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Minesharp
{
    static public class Network
    {
        #region Properties
        private static Boolean _loggedIn;
        private static Int32 keepAliveTicks = 0;
        private static String _sessionId;
        private static String _username;
        public static Boolean LoggedIn
        {
            get { return _loggedIn; }
            set { _loggedIn = value; }
        }
        public static String SessionId
        {
            get { return _sessionId; }
            set { _sessionId = value; }
        }
        public static String Username
        {
            get { return _username; }
            set { _username = value; }
        }
        #endregion

        #region Authenticate
        public static Boolean Authenticate(String user, String pass)
        {
            //check that the user is a legit MC user

            WebRequest webreq = WebRequest.Create("https://login.minecraft.net");
            webreq.Method = "POST";
            string postdata = "user=" + user + "&password=" + pass + "&version=500";
            webreq.ContentType = "application/x-www-form-urlencoded";
            webreq.ContentLength = Encoding.UTF8.GetByteCount(postdata);
            Stream datastream = webreq.GetRequestStream();
            datastream.Write(Encoding.UTF8.GetBytes(postdata), 0, Encoding.UTF8.GetByteCount(postdata));
            datastream.Close();
            WebResponse response = webreq.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            String strResp = reader.ReadToEnd();
            reader.Close();
            response.Close();
            Console.WriteLine(strResp);
            String[] Data = strResp.Split(new char[] { ':' });

            if (Data.Length == 4)
            {
                _sessionId = Data[3];
                _username = Data[2];
                _loggedIn = true;
                return true;
            }
            else
                return false;
        }
        #endregion

        #region Keep Alive
        public static void KeepAlive()
        {
            keepAliveTicks++;
            if (keepAliveTicks == 6000)
            {
                //TODO: get this to work. Right now the server returns a 504 error (forbidden?) which causes the webclient to throw an exception.
                //WebClient webcli = new WebClient();
                //Console.WriteLine(webcli.UploadString("https://login.minecraft.net/session?name=" + _username + "&session=" + _sessionId, " "));
                keepAliveTicks = 0;
            }
        }
        #endregion
    }
}
