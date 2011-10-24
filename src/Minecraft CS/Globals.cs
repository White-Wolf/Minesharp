using System;
using System.Collections.Generic;
using System.Text;
using InputEventSystem;
using WindowSystem;

namespace Minecraft_CS
{
    public static class Globals
    {
        private static InputEvents _input;
        private static GUIManager _gui;

        public static InputEvents Input
        {
            get { return _input; }
            set { _input = value; }
        }
        public static GUIManager GUI
        {
            get { return _gui; }
            set { _gui = value; }
        }
    }
}
