using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace Minecraft_CS
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content/Resources";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Window.Title = "Minecraft CS Beta";

            RenderEngine.RenderEngine.Init(graphics, Content);
            RenderEngine.RenderEngine.Exiting += new EventHandler(Exit);

            Settings.loadSettings();
            Globals.Input = new InputEventSystem.InputEvents(this);
            Globals.GUI = new WindowSystem.GUIManager(this);

            base.IsMouseVisible = true;
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += new EventHandler(WindowResized);
            base.Initialize();
        }

        private void WindowResized(object sender, EventArgs e)
        {
            Dictionary<String, Object> newSettings = Settings.toDictionary();
            newSettings["Width"] = Window.ClientBounds.Width;
            newSettings["Height"] = Window.ClientBounds.Height;
            RenderEngine.RenderEngine.ReloadScreen(newSettings);
        }

        public void Exit(object sender, EventArgs e)
        {
            this.Exit();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            RenderEngine.RenderEngine.Load(Settings.toDictionary());

            RenderEngine.RenderEngine.PushScreen(new RenderEngine.Launcher());
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            RenderEngine.RenderEngine.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            RenderEngine.RenderEngine.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            RenderEngine.RenderEngine.Draw(gameTime);

            base.Draw(gameTime);
        }
    }

    public class WindowWrapper : System.Windows.Forms.IWin32Window
    {
        public WindowWrapper(IntPtr handle)
        {
            _hwnd = handle;
        }

        public IntPtr Handle
        {
            get { return _hwnd; }
        }

        private IntPtr _hwnd;
    }
}
