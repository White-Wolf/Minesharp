using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Minesharp.Screens
{
    class Loading : Screen
    {
        private bool showNext = false;
        private double timeSinceChange = 0;
        public Loading()
        {
            Console.WriteLine("Entering Loading Screen");
            screenFolder += "Loading/";
        }
        public override void Draw()
        {
            GL.ClearColor(Color.White);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.PushMatrix();
            GL.Ortho(0, Settings.Width, 0, Settings.Height, 0, 1);

            if (!showNext)
            {
                GL.BindTexture(TextureTarget.Texture2D, textures["mojang"].Id);
                GL.Begin(BeginMode.Quads);
                GL.TexCoord2(0, 0); GL.Vertex2(((Settings.Width / 2) + (textures["mojang"].Width / 2)) - textures["mojang"].Width, (Settings.Height / 2) - (textures["mojang"].Height / 2));
                GL.TexCoord2(0, 1); GL.Vertex2(((Settings.Width / 2) + (textures["mojang"].Width / 2)) - textures["mojang"].Width, ((Settings.Height / 2) - (textures["mojang"].Height / 2)) + textures["mojang"].Height);
                GL.TexCoord2(1, 1); GL.Vertex2((Settings.Width / 2) + (textures["mojang"].Width / 2), ((Settings.Height / 2) - (textures["mojang"].Height / 2)) + textures["mojang"].Height);
                GL.TexCoord2(1, 0); GL.Vertex2((Settings.Width / 2) + (textures["mojang"].Width / 2), (Settings.Height / 2) - (textures["mojang"].Height / 2));
                GL.End();
            }
            else
            {
                GL.BindTexture(TextureTarget.Texture2D, textures["whitewolf"].Id);
                GL.Begin(BeginMode.Quads);
                GL.TexCoord2(0, 0); GL.Vertex2(((Settings.Width / 2) + (textures["whitewolf"].Width / 2)) - textures["whitewolf"].Width, (Settings.Height / 2) - (textures["whitewolf"].Height / 2));
                GL.TexCoord2(0, 1); GL.Vertex2(((Settings.Width / 2) + (textures["whitewolf"].Width / 2)) - textures["whitewolf"].Width, ((Settings.Height / 2) - (textures["whitewolf"].Height / 2)) + textures["whitewolf"].Height);
                GL.TexCoord2(1, 1); GL.Vertex2((Settings.Width / 2) + (textures["whitewolf"].Width / 2), ((Settings.Height / 2) - (textures["whitewolf"].Height / 2)) + textures["whitewolf"].Height);
                GL.TexCoord2(1, 0); GL.Vertex2((Settings.Width / 2) + (textures["whitewolf"].Width / 2), (Settings.Height / 2) - (textures["whitewolf"].Height / 2));
                GL.End();
            }

            GL.PopMatrix();
        }
        public override void Load()
        {
            textures.Add("mojang", LoadTexture("mojang.png"));
            textures.Add("whitewolf", LoadTexture("whitewolf.png"));
        }
        public override void Update(FrameEventArgs e)
        {
            timeSinceChange += e.Time;
            if (timeSinceChange >= 2)
            {
                timeSinceChange = 0;
                if (!showNext)
                    showNext = true;
                else
                    ScreenManager.Push(new Screens.MainMenu(), true);
            }
        }
    }
}
