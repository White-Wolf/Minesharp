using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Minecraft_CS
{
    public static class Settings
    {
        #region Properties
        private static Boolean _fullscreen;
        private static Boolean _showfps;
        private static Boolean _vtrace;
        private static Int32 _antialiasing;
        private static Int32 _height;
        private static Int32 _width;
        private static Version _version;

        public static Boolean Fullscreen
        {
            get { return _fullscreen; }
            set { _fullscreen = value; }
        }
        public static Boolean ShowFPS
        {
            get { return _showfps; }
            set { _showfps = value; }
        }
        public static Boolean VTrace
        {
            get { return _vtrace; }
            set { _vtrace = value; }
        }
        public static Int32 AntiAliasing
        {
            get { return _antialiasing; }
            set { _antialiasing = value; }
        }
        public static Int32 Height
        {
            get { return _height; }
            set { _height = value; }
        }
        public static Int32 Width
        {
            get { return _width; }
            set { _width = value; }
        }
        public static Version Version
        {
            get { return _version; }
        }
        #endregion

        #region Functions
        #region loadSettings
        public static void loadSettings()
        {
            StreamReader settingsFile;
            settingsFile = new StreamReader("settings.conf");
            String[] settingsLines = settingsFile.ReadToEnd().Split(new char[] {'\n', '\r'}, StringSplitOptions.RemoveEmptyEntries);
            foreach (String settingsLine in settingsLines)
            {
                String[] lineTemp = settingsLine.Split(new char[] { '=' });
                lineTemp[0] = lineTemp[0].Trim();
                lineTemp[1] = lineTemp[1].Trim();

                switch (lineTemp[0].ToLower())
                {
                    case "fullscreen":
                        _fullscreen = Convert.ToBoolean(lineTemp[1]);
                        break;
                    case "showfps":
                        _showfps = Convert.ToBoolean(lineTemp[1]);
                        break;
                    case "vtrace":
                        _vtrace = Convert.ToBoolean(lineTemp[1]);
                        break;
                    case "antialiasing":
                        _antialiasing = Int32.Parse(lineTemp[1]);
                        break;
                    case "height":
                        _height = Int32.Parse(lineTemp[1]);
                        break;
                    case "width":
                        _width = Int32.Parse(lineTemp[1]);
                        break;
                }
            }
            _version = new Version("0.0.1");
            settingsFile.Close();
            settingsFile.Dispose();
            settingsLines = null;
        }
        #endregion

        #region toDictionary
        public static Dictionary<String, Object> toDictionary()
        {
            Dictionary<String, Object> tempSettings = new Dictionary<String, Object>();
            tempSettings.Add("Fullscreen", _fullscreen);
            tempSettings.Add("VTrace", _vtrace);
            tempSettings.Add("Width", _width);
            tempSettings.Add("Height", _height);
            tempSettings.Add("Antialiasing", _antialiasing);
            tempSettings.Add("ShowFPS", _showfps);

            return tempSettings;
        }
        #endregion

        #region saveSettings
        public static void saveSettings()
        {
            StreamWriter settingsWriter = new StreamWriter("settings.conf", false);
            settingsWriter.AutoFlush = true;
            settingsWriter.WriteLine("Antialiasing = " + _antialiasing);
            settingsWriter.WriteLine("Fullscreen = " + _fullscreen);
            settingsWriter.WriteLine("Height = " + _height);
            settingsWriter.WriteLine("ShowFPS = " + _showfps);
            settingsWriter.WriteLine("VTrace = " + _vtrace);
            settingsWriter.WriteLine("Width = " + _width);
            settingsWriter.Close();
            settingsWriter.Dispose();
        }
        #endregion
        #endregion
    }
}
