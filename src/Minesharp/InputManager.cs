using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;
using OpenTK.Input;

namespace Minesharp
{
    public static class InputManager
    {
        private static MouseDevice _mouse;
        private static KeyboardDevice _keyboard;
        public static MouseDevice Mouse
        {
            get { return _mouse; }
        }
        public static KeyboardDevice Keyboard
        {
            get { return _keyboard; }
        }

        public static void Init(MouseDevice md, KeyboardDevice kd)
        {
            _mouse = md;
            _keyboard = kd;
        }
    }
}
