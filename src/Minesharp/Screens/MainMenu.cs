using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Minesharp.Screens
{
    class MainMenu : Screen
    {
        private Vector2[][] menuVects;
        private bool[] btnHover;
        public MainMenu()
        {
            Console.WriteLine("Entering MainMenu");
            screenFolder += "MainMenu/";

            //Attach our input handler functions to the Input Manager
            InputManager.Mouse.ButtonDown += new EventHandler<MouseButtonEventArgs>(Mouse_ButtonDown);
        }

        void Mouse_ButtonDown(object sender, MouseButtonEventArgs e)
        {
            for (Int16 i = 0; i < 6; i++)
            {
                if ((e.X > menuVects[i][0].X && e.X < menuVects[i][1].X) && ((Settings.Height - e.Y) > menuVects[i][0].Y && (Settings.Height - e.Y) < menuVects[i][2].Y))
                {
                    switch (i)
                    {
                        case 0: //Singleplayer clicked
                            ScreenManager.Push(new Screens.Singleplayer());
                            break;
                        case 1: //Multiplayer clicked
                            ScreenManager.Push(new Screens.Multiplayer());
                            break;
                        case 2: //Texture Packs clicked
                            break;
                        case 3: //Mods clicked
                            break;
                        case 4: //Options clicked
                            break;
                        case 5: //Exit clicked
                            Program.Close();
                            break;
                    }
                }
            }
        }
        public override void Draw()
        {
            GL.ClearColor(Color.White);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.PushMatrix();
            GL.Ortho(0, Settings.Width, 0, Settings.Height, 0, 1);

            GL.BindTexture(TextureTarget.Texture2D, textures["buttonsSheet"].Id);
            GL.Begin(BeginMode.Quads);
            for (double i = 0, j = 1.0; i < 6; i++, j -= 0.2)
            {
                double k = 0, l = 1;
                if (i == 4)
                    l = 0.5;
                if (i == 5)
                {
                    k = 0.5;
                    j += 0.2;
                }
                GL.TexCoord2(k, (j - 0.1) - ((btnHover[(int)i]) ? 0.1 : 0)); GL.Vertex2(menuVects[(int)i][0].X, menuVects[(int)i][0].Y);
                GL.TexCoord2(l, (j - 0.1) - ((btnHover[(int)i]) ? 0.1 : 0)); GL.Vertex2(menuVects[(int)i][1].X, menuVects[(int)i][1].Y);
                GL.TexCoord2(l, j - ((btnHover[(int)i]) ? 0.1 : 0)); GL.Vertex2(menuVects[(int)i][2].X, menuVects[(int)i][2].Y);
                GL.TexCoord2(k, j - ((btnHover[(int)i]) ? 0.1 : 0)); GL.Vertex2(menuVects[(int)i][3].X, menuVects[(int)i][3].Y);
            }
            GL.End();
            GL.PopMatrix();
        }
        public override void Input_Controller(MouseDevice md, KeyboardDevice kd)
        {
            for (Int16 i = 0; i < 6; i++)
            {
                btnHover[i] = false;
                if ((md.X > menuVects[i][0].X && md.X < menuVects[i][1].X) && ((Settings.Height - md.Y) > menuVects[i][0].Y && (Settings.Height - md.Y) < menuVects[i][2].Y))
                {
                    btnHover[i] = true;
                }
            }

                
        }
        public override void Load()
        {
            textures.Add("buttonsSheet", LoadTexture("ButtonsSheet.png"));
            SetupMenus();
        }
        public override void Update(FrameEventArgs e) 
        {
        }
        private void SetupMenus()
        {
            menuVects = new Vector2[6][];
            for (Int16 i = 5, j = 0; i > 0; i--, j++)
            {
                menuVects[j] = new Vector2[4];
                menuVects[j][0] = new Vector2((Settings.Width / 2) - (textures["buttonsSheet"].Width / 2), ((Settings.Height / 2) - (50 * 5 / 2)) + (50 * (i-1)) + 11);
                menuVects[j][1] = new Vector2((Settings.Width / 2) - (textures["buttonsSheet"].Width / 2) + textures["buttonsSheet"].Width, ((Settings.Height / 2) - (50 * 5 / 2)) + (50 * (i - 1)) + 11);
                menuVects[j][2] = new Vector2((Settings.Width / 2) - (textures["buttonsSheet"].Width / 2) + textures["buttonsSheet"].Width, (Settings.Height / 2) - (50 * 5 / 2) + 50 * i);
                menuVects[j][3] = new Vector2((Settings.Width / 2) - (textures["buttonsSheet"].Width / 2), (Settings.Height / 2) - (50 * 5 / 2) + 50 * i); 
            }
            menuVects[4] = new Vector2[4];
            menuVects[4][0] = new Vector2((Settings.Width / 2) - (textures["buttonsSheet"].Width / 2), ((Settings.Height / 2) - (50 * 5 / 2)) + (50 * 0) + 11);
            menuVects[4][1] = new Vector2((Settings.Width / 2) - 5, ((Settings.Height / 2) - (50 * 5 / 2)) + (50 * 0) + 11);
            menuVects[4][2] = new Vector2((Settings.Width / 2) - 5, (Settings.Height / 2) - (50 * 5 / 2) + 50 * 1);
            menuVects[4][3] = new Vector2((Settings.Width / 2) - (textures["buttonsSheet"].Width / 2), (Settings.Height / 2) - (50 * 5 / 2) + 50 * 1);
            menuVects[5] = new Vector2[4];
            menuVects[5][0] = new Vector2((Settings.Width / 2) +5, ((Settings.Height / 2) - (50 * 5 / 2)) + (50 * 0) + 11);
            menuVects[5][1] = new Vector2((Settings.Width / 2) - (textures["buttonsSheet"].Width / 2) + textures["buttonsSheet"].Width, ((Settings.Height / 2) - (50 * 5 / 2)) + (50 * 0) + 11);
            menuVects[5][2] = new Vector2((Settings.Width / 2) - (textures["buttonsSheet"].Width / 2) + textures["buttonsSheet"].Width, (Settings.Height / 2) - (50 * 5 / 2) + 50 * 1);
            menuVects[5][3] = new Vector2((Settings.Width / 2) + 5, (Settings.Height / 2) - (50 * 5 / 2) + 50 * 1);
            btnHover = new bool[6];
        }
    }
}
