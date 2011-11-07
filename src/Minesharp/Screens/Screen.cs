using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;


namespace Minesharp
{
    public abstract class Screen
    {
        public abstract void Draw();
        public abstract void Input_Controller();
        public abstract void Load();
        public abstract void Unload();
        public abstract void Update();
    }
}
