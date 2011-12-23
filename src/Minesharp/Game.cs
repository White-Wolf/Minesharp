using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Minesharp
{
    class Game : GameWindow
    {
        #region Properties
        private double FPSUpdateTicker = 0;
        private float[] frames = new float[5];
        private int currentFrame = 0;
        private String WindowTitle;
        public float FPS;
        #endregion

        #region Construct
        public Game()
            : base(800, 600)
        {
            Console.WriteLine("Initializing the Game Window...");
            #region  Event Handlers & Input Manager
            //Initialize the Input Manager
            InputManager.Init(Mouse, Keyboard);

            //Hook all of our Event Handlers
            Keyboard.KeyDown += new EventHandler<KeyboardKeyEventArgs>(Event_KeyDown);
            Keyboard.KeyUp += new EventHandler<KeyboardKeyEventArgs>(Event_KeyUp);
            Mouse.ButtonDown += new EventHandler<MouseButtonEventArgs>(Event_MouseDown);
            Mouse.ButtonUp += new EventHandler<MouseButtonEventArgs>(Event_MouseUp);
            Mouse.Move += new EventHandler<MouseMoveEventArgs>(Event_MouseMove);
            Mouse.WheelChanged += new EventHandler<MouseWheelEventArgs>(Event_MouseWheelChange);
            #endregion

            //load our settings
            Settings.loadSettings();
            //set the windowtitle var to it's value
            WindowTitle = "Minesharp v"+Settings.Version.ToString(3)+" Based on Minecraft v"+Settings.MCVersion.ToString(3);
            //set the window title to our variable
            this.Title = WindowTitle;
            //initialize the screen manager
            Console.WriteLine("Game Window successfully initialized!");
        }
        #endregion

        #region Functions
        #region Event Handlers
        void Event_MouseWheelChange(object sender, MouseWheelEventArgs e)
        {
        }

        void Event_MouseMove(object sender, MouseMoveEventArgs e)
        {
        }

        void Event_MouseUp(object sender, MouseButtonEventArgs e)
        {
        }

        void Event_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        void Event_KeyUp(object sender, KeyboardKeyEventArgs e)
        {
        }

        void Event_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Exit();

            if (e.Key == Key.F11)
                this.WindowState = (this.WindowState == WindowState.Normal) ? WindowState.Fullscreen : WindowState.Normal;
        }
        #endregion

        #region Overrides
        protected override void OnLoad(EventArgs e)
        {
            //set graphics settings to those from the loaded settings
            this.VSync = (Settings.VSync) ? VSyncMode.On : VSyncMode.Off;
            this.Width = Settings.Width;
            this.Height = Settings.Height;

            //clear the screen to default CornflowerBlue ( uck XP )
            GL.ClearColor(Color.CornflowerBlue);

            //Enable Texture2D so we can bind our 2D textures
            GL.Enable(EnableCap.Texture2D);
            //enable blend for transparency
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            GL.Disable(EnableCap.DepthTest);

            //Push the first (loading) screen
            ScreenManager.Push(new Screens.Loading());
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            //update our FPS calculations
            UpdateFramerate(e.Time);

            //Clear the buffers
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            //Clear with default color (again XP, I'm gonna puke CornflowerBlue if i see it again)
            GL.ClearColor(Color.CornflowerBlue);

            //Draw the current screen
            ScreenManager.Draw();

            //move our current buffer to the screen
            this.SwapBuffers();
        }
        protected override void OnResize(EventArgs e)
        {
            //I wish I understood =/
            GL.Viewport(0, 0, Width, Height);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);
        }
        protected override void OnUnload(EventArgs e)
        {
            //NOTE: screen manager MUST be called before the texture engine because the screens unload their textures from the texture engine
            //  and it knows, but the screens don't know when the texture engine unloads their stuff
            //Tell the screen manager that all screens need to unload their resources
            ScreenManager.Unload();
            //Tell our texture engine to unload all resources
            TextureEngine.Unload();
            base.OnUnload(e);
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            //Send the keepalive to the MC HQ so we stay logged in
            //NOTE: Is this needed for anything but multiplayer?
            Network.KeepAlive();

            //call the current screen's input controller
            ScreenManager.InputController(Mouse, Keyboard);
            //update the current frame
            ScreenManager.Update(e);

            base.OnUpdateFrame(e);
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
        }
        #endregion

        #region Update Framerate
        private void UpdateFramerate(double time)
        {

            //This FPS algorithm updates every second
            //NOTE: is this a reliable FPS calculation?
            if (FPSUpdateTicker >= 1)
            {
                //keep collecting framerate data until the frames[] array is full
                if (currentFrame < frames.Length)
                {
                    frames[currentFrame] = (float)(1.0f / time);
                    currentFrame++;
                }
                else
                {
                    float tempFPS = 0;
                    currentFrame = 0;

                    //add all the frame interval times together
                    for (Int16 i = 0; i < frames.Length; i++)
                        tempFPS += frames[i];
                    //then divide that by the number of frame intervals collected
                    FPS = tempFPS / (frames.Length);
                    //Only display the data if the user has selected showFPS
                    if (Settings.ShowFPS)
                        this.Title = WindowTitle + " FPS: " + Math.Round(FPS, 2);
                    Console.WriteLine("FPS: "+Math.Round(FPS, 4));
                    FPSUpdateTicker = 0;
                }
            }
            else
            {
                FPSUpdateTicker += time;
            }
        }
        #endregion

        #endregion
    }
}
