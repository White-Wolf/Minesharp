using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Minecraft_CS.RenderEngine
{
	public static class RenderEngine
	{
		#region Properties
		private static Boolean isDrawing = false;
		private static Boolean isLoading = false;
		private static ContentManager _contentManager;
		private static Dictionary<String, Object> _settings;
		private static float[] lastFrames = new float[60];
		private static GraphicsDeviceManager _graphicsManager;
		private static Int16 _fps;
		private static Int16 frameIndex;
		private static Rectangle _titleSafeArea;
		private static RenderTarget2D screenTarget;
		private static SpriteBatch _spriteBatch;
		private static SpriteFont[] fonts = new SpriteFont[1];
		private static Stack<Screen> _currentScreen;
		private static Texture2D FPSWindow;

		public static ContentManager ContentManager
		{
			get { return _contentManager; }
		}
		public static Dictionary<String, Object> Settings
		{
			get { return _settings; }
		}
		public static Int16 FPS
		{
			get { return _fps; }
		}
		public static GraphicsDeviceManager GraphicsManager
		{
			get { return _graphicsManager; }
		}
		public static Rectangle TitleSafeArea
		{
			get { return _titleSafeArea; }
		}
		public static SpriteBatch SpriteBatch
		{
			get { return _spriteBatch; }
		}
		public static Stack<Screen> CurrentScreen
		{
			get { return _currentScreen; }
		}
		#endregion

		#region Events
		#region Drawing
		public struct DrawingArgs
		{
			private GameTime _gameTime;
			private SpriteBatch _sb;

			public GameTime GameTime
			{
				get { return _gameTime; }
			}
			public SpriteBatch sb
			{
				get { return _sb; }
			}

			public DrawingArgs(GameTime gameTime)
			{
				_gameTime = gameTime;
				_sb = _spriteBatch;
			}
		}
		public delegate void DrawingHandler(Object sender, DrawingArgs e);
		public static event DrawingHandler Drawing;
		#endregion

		#region Exiting
		public static event EventHandler Exiting;
		#endregion

		#region Loading
		public struct LoadingArgs
		{
			private Dictionary<String, Object> _settings;

			public Dictionary<String, Object> Settings
			{
				get { return _settings; }
			}

			public LoadingArgs(Dictionary<String, Object> settings)
			{
				_settings = settings;
			}
		}
		public delegate void LoadingHandler(Object sender, LoadingArgs e);
		public static event LoadingHandler Loading;
		#endregion

		#region PoppingScreen
		public struct PoppingScreenArgs
		{
			private Boolean _loadNextScreen;

			public Boolean LoadNextScreen
			{
				get { return _loadNextScreen; }
			}

			public PoppingScreenArgs(Boolean loadNextScreen)
			{
				_loadNextScreen = loadNextScreen;
			}
		}
		public delegate void PoppingScreenHandler(Object sender, PoppingScreenArgs e);
		public static event PoppingScreenHandler PoppingScreen;
		#endregion

		#region PushingScreen
		public struct PushingScreenArgs
		{
			private Screen _newScreen;
			private Screen _oldScreen;

			public Screen NewScreen
			{
				get { return _newScreen; }
			}
			public Screen OldScreen
			{
				get { return _oldScreen; }
			}

			public PushingScreenArgs(Screen newScreen, Screen oldScreen)
			{
				_newScreen = newScreen;
				_oldScreen = oldScreen;
			}
		}
		public delegate void PushingScreenHandler(Object sender, PushingScreenArgs e);
		public static event PushingScreenHandler PushingScreen;
		#endregion

		#region ReloadingScreen
		public struct ReloadingScreenArgs
		{
			private Dictionary<String, Object> _newSettings;
			private Dictionary<String, Object> _oldSettings;

			public Dictionary<String, Object> NewSettings
			{
				get { return _newSettings; }
			}
			public Dictionary<String, Object> OldSettings
			{
				get { return _oldSettings; }
			}

			public ReloadingScreenArgs(Dictionary<String, Object> newSettings, Dictionary<String, Object> oldSettings)
			{
				_newSettings = newSettings;
				_oldSettings = oldSettings;
			}
		}
		public delegate void ReloadingScreenHandler(Object sender, ReloadingScreenArgs e);
		public static event ReloadingScreenHandler ReloadingScreen;
		#endregion

		#region Unloading
		public static event EventHandler Unloading;
		#endregion

		#region Updating
		public struct UpdatingArgs
		{
			private GameTime _gameTime;

			public GameTime GameTime
			{
				get { return _gameTime; }
			}

			public UpdatingArgs(GameTime gameTime)
			{
				_gameTime = gameTime;
			}
		}
		public delegate void UpdatingHandler(Object sender, UpdatingArgs e);
		public static event UpdatingHandler Updating;
		#endregion
		#endregion

		#region Functions
		#region ClearScreen
		public static void ClearScreen(Color color)
		{
			_graphicsManager.GraphicsDevice.Clear(color);
		}
		#endregion

		#region Draw
        public static void Draw(Object sender, DrawingArgs e) { }
		public static void Draw(GameTime gameTime)
		{
			_spriteBatch.Begin();
			Drawing(new Object(), new DrawingArgs(gameTime));
			_currentScreen.Peek().Draw(gameTime, _spriteBatch);
			_spriteBatch.End();
		}
		#endregion

        #region Exit
        public static void Exit(Object sender, EventArgs e) { }
        #endregion

        #region Initializer
        public static void Init(GraphicsDeviceManager gdm, ContentManager cm)
		{
			_graphicsManager = gdm;
			_contentManager = cm;
			_currentScreen = new Stack<Screen>();

            //Set Events
            Drawing += new DrawingHandler(Draw);
            Exiting += new EventHandler(Exit);
            Loading += new LoadingHandler(Load);
            PoppingScreen += new PoppingScreenHandler(PopScreen);
            PushingScreen += new PushingScreenHandler(PushScreen);
            ReloadingScreen += new ReloadingScreenHandler(ReloadScreen);
            Unloading += new EventHandler(Unload);
            Updating += new UpdatingHandler(Update);
		}
		#endregion

		#region Load
        public static void Load(Object sender, LoadingArgs e) { }
		public static void Load(Dictionary<String, Object> settings)
		{
			_settings = settings;

            _graphicsManager.GraphicsDevice.RenderState.CullMode = CullMode.None;
			//_graphicsManager.GraphicsDevice.PresentationParameters.MultiSampleQuality = (int)_settings["Antialias"];
			//_graphicsManager.GraphicsDevice.RenderState.MultiSampleAntiAlias = ((int)_settings["Antialias"] < 1) ? false : true;
			_graphicsManager.SynchronizeWithVerticalRetrace = (bool)_settings["VTrace"];
            _graphicsManager.IsFullScreen = (bool)_settings["Fullscreen"];
            _graphicsManager.PreferredBackBufferWidth = (int)_settings["Width"];
            _graphicsManager.PreferredBackBufferHeight = (int)_settings["Height"];
            _graphicsManager.PreferredBackBufferFormat = SurfaceFormat.Color;
            _graphicsManager.ApplyChanges();

			_titleSafeArea = _graphicsManager.GraphicsDevice.Viewport.TitleSafeArea;
			_spriteBatch = new SpriteBatch(_graphicsManager.GraphicsDevice);

			PushScreen(new Base());

			if ((bool)_settings["ShowFPS"])
			{
				for (Int16 i = 0; i < lastFrames.Length; i++)
					lastFrames[i] = 1 / 30f;
			}

			//TODO: finish this =/
		}
		#endregion

		#region ReloadScreen
        public static void ReloadScreen(Object sender, ReloadingScreenArgs e) { }
		public static void ReloadScreen(Dictionary<String, Object> newSettings)
		{
			ReloadingScreen(new Object(), new ReloadingScreenArgs(newSettings, _settings));

			_settings["Width"] = newSettings["Width"];
			_settings["Height"] = newSettings["Height"];
			_settings["Fullscreen"] = newSettings["Fullscreen"];
			_settings["VTrace"] = newSettings["VTrace"];

            _graphicsManager.SynchronizeWithVerticalRetrace = (bool)_settings["VTrace"];
            _graphicsManager.IsFullScreen = (bool)_settings["Fullscreen"];
            _graphicsManager.PreferredBackBufferWidth = (int)_settings["Width"];
            _graphicsManager.PreferredBackBufferHeight = (int)_settings["Height"];
            _graphicsManager.PreferredBackBufferFormat = SurfaceFormat.Color;
			_graphicsManager.ApplyChanges();
            _titleSafeArea = _graphicsManager.GraphicsDevice.Viewport.TitleSafeArea;
		}
		#endregion

		#region Unload
        public static void Unload(Object sender, EventArgs e) { }
		public static void Unload()
		{
			Unloading(new Object(), new EventArgs());

			_currentScreen.Peek().Unload();
		}
		#endregion

		#region Update
        public static void Update(Object sender, UpdatingArgs e) { }
		public static void Update(GameTime gameTime)
		{
			Updating(new Object(), new UpdatingArgs(gameTime));
			_currentScreen.Peek().Update(gameTime);
		}
		#endregion

		#region Virtual Screen Controls
		#region PopScreen
        public static void PopScreen(Object sender, PoppingScreenArgs e) { }
		public static void PopScreen(Boolean loadNextScreen)
		{
			PoppingScreen(new Object(), new PoppingScreenArgs(loadNextScreen));

			//Unload the Current Screen
			Console.Write("Unloading old Screen...");
			_currentScreen.Peek().Unload();
			Console.WriteLine("DONE");

			//Pop the old Screen
			Console.Write("Popping old Screen...");
			_currentScreen.Pop();
			Console.WriteLine("DONE");

			Console.WriteLine("Old Screen Successfully popped.");

			//If there are no more screens, exit (should never happen)
			if (CurrentScreen.Count < 1)
			{
				Console.WriteLine("No more screens, exiting...");
				throw new Exception("No more Screens D:");
			}

			//Otherwise if loadNextScreen, load the next screen
			if (loadNextScreen)
			{
				Console.Write("Starting next Screen...");

				//Do not begin to load till finished drawing
				while (isDrawing) { }
				isLoading = true;

				_currentScreen.Peek().Load();

				Console.WriteLine("DONE");
				isLoading = false;
			}
		}
		#endregion

		#region PushScreen
        public static void PushScreen(Object sender, PushingScreenArgs e) { }
		public static void PushScreen(Screen newScreen)
		{
			PushingScreen(new Object(), new PushingScreenArgs(newScreen, (_currentScreen.Count > 0) ? _currentScreen.Peek() : null));

			//Do not begin to load till finished drawing
			while (isDrawing) { }
			isLoading = true;

			//Pop all Old Screens to Base screen
			/*while (!(_currentScreen.Peek() is Base))
			{
				Console.Write("Popping old Screen...");
				Console.WriteLine("DONE");
			}
			Console.WriteLine("Screen Stack popped down to Base screen.");*/

			//Push the new Screen to the top of the Stack
			Console.Write("Pushing the new Screen...");
			CurrentScreen.Push(newScreen);
			Console.WriteLine("DONE");

			//Load the new Screen
			Console.Write("Loading new Screen...");
			CurrentScreen.Peek().Load();
			Console.WriteLine("DONE");

			isLoading = false;

			Console.WriteLine("New Screen successfully pushed.");
		}
		#endregion
		#endregion
		#endregion
	}
}
