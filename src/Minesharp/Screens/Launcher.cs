using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Minesharp
{
    public class Launcher : Screen
    {
        private string screenFolder = "Screens/Launcher/";
        private Dictionary<String, Int32> textures;
        public Launcher()
        {
            textures = new Dictionary<string, int>();
        }
        public override void Draw()
        {
            GL.ClearColor(0.5f, 0.5f, 0.5f, 1.0f);
            /*GL.BindTexture(TextureTarget.Texture2D, textures["logo"]);
            GL.Begin(BeginMode.Quads);

            GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(-0.6f, -0.4f);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(0.6f, -0.4f);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(0.6f, 0.4f);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(-0.6f, 0.4f);

            GL.End();*/

        }
        public override void Input_Controller() { }
        public override void Load()
        {
            textures.Add("background", TextureEngine.LoadTexture(screenFolder + "background.png"));
            textures.Add("logo", TextureEngine.LoadTexture(screenFolder + "logo.png"));
        }
        public override void Unload() { }
        public override void Update() { }
    }
}
