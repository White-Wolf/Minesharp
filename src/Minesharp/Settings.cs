using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Minesharp
{
    public static class Settings
    {
        #region Properties
        private static Boolean _fullscreen;
        private static Boolean _showfps;
        private static Boolean _vsync;
        private static Int32 _height;
        private static Int32 _width;
        private static Version _mcversion;
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
        public static Boolean VSync
        {
            get { return _vsync; }
            set { _vsync = value; }
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
        public static Version MCVersion
        {
            get { return _mcversion; }
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
            Console.WriteLine("Loading Settings...");
            StreamReader settingsFile;
            try
            {
                settingsFile = new StreamReader("settings.conf");
                
            }
            catch (FileNotFoundException fe)
            {
                Console.WriteLine("\tSettings file does not exist, creating one with default values");
                File.Create("settings.conf").Close();
                _fullscreen = false;
                _showfps = true;
                _vsync = false;
                _height = 600;
                _width = 800;
                saveSettings();
                settingsFile = new StreamReader("settings.conf");
            }
            string settingsString = settingsFile.ReadToEnd();
            String[] settingsLines = settingsString.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
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
                    case "vsync":
                        _vsync = Convert.ToBoolean(lineTemp[1]);
                        break;
                    case "height":
                        _height = Int32.Parse(lineTemp[1]);
                        break;
                    case "width":
                        _width = Int32.Parse(lineTemp[1]);
                        break;
                }
            }

            settingsFile.Close();
            settingsFile.Dispose();
            settingsLines = null;
            _mcversion = new Version("1.8.1");
            _version = new Version("0.0.1");
            Console.WriteLine("Settings successfully loaded!");
        }
        #endregion

        #region toDictionary
        public static Dictionary<String, Object> toDictionary()
        {
            Dictionary<String, Object> tempSettings = new Dictionary<String, Object>();
            tempSettings.Add("fullscreen", _fullscreen);
            tempSettings.Add("vsync", _vsync);
            tempSettings.Add("width", _width);
            tempSettings.Add("height", _height);
            tempSettings.Add("showfps", _showfps);
            return tempSettings;
        }
        #endregion

        #region saveSettings
        public static void saveSettings()
        {
            StreamWriter settingsWriter = new StreamWriter("settings.conf", false);
            settingsWriter.AutoFlush = true;
            settingsWriter.WriteLine("fullscreen = " + _fullscreen);
            settingsWriter.WriteLine("height = " + _height);
            settingsWriter.WriteLine("showfps = " + _showfps);
            settingsWriter.WriteLine("vsync = " + _vsync);
            settingsWriter.WriteLine("width = " + _width);
            settingsWriter.Close();
            settingsWriter.Dispose();
        }
        #endregion
        #endregion
    }
}
