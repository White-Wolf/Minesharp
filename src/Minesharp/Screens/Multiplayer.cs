using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Minesharp.Screens
{
    class Multiplayer : Screen
    {
        public Multiplayer()
        {
            Console.WriteLine("Entering Multiplayer Screen");
            screenFolder += "Multiplayer/";
        }
        public override void Input_Controller(MouseDevice md, KeyboardDevice kd)
        {
        }
        public override void Draw()
        {
        }
    }
}
