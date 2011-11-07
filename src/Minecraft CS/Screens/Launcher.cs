using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Minecraft_CS.RenderEngine
{
	public class Launcher : Screen
	{
		#region Properties
        String contentPath;
		#region Image Textures
		Texture2D backgroundImage;
		Texture2D logoImage;
		#endregion

		#region loginForm objects
		private TextButton btnLogin;
        private TextButton btnOptions;
        private Label lblPassword;
        private Label lblUsername;
        private TextBox txtPassword;
        private TextBox txtUsername;
		#endregion
		#endregion

		#region Functions
		#region Overrides
		public override void Draw(GameTime gameTime, SpriteBatch sb)
		{
			RenderEngine.ClearScreen(new Color(new Vector3(0.1f, 0.1f, 0.1f)));
            for(Int16 i = 0; i <= RenderEngine.TitleSafeArea.Width/backgroundImage.Width; i++)
			    sb.Draw(backgroundImage, new Rectangle(0+(backgroundImage.Width * i), RenderEngine.TitleSafeArea.Height - backgroundImage.Height, backgroundImage.Width, backgroundImage.Height), Color.White);
			sb.Draw(logoImage, new Rectangle(10, (RenderEngine.TitleSafeArea.Height - backgroundImage.Height) + (backgroundImage.Height / 2 - logoImage.Height / 2), logoImage.Width, logoImage.Height), Color.White);
		}
        public void InitForm()
        {
            /*btnLogin = new TextButton();
            btnOptions = new TextButton();
            lblPassword = new Label();
            lblUsername = new Label();
            txtPassword = new TextBox();
            txtUsername = new TextBox();

            //loginForm
            loginForm.Width = 200;
            loginForm.Height = 102;
            loginForm.Location = new System.Drawing.Point(RenderEngine.TitleSafeArea.Width - loginForm.Width, RenderEngine.TitleSafeArea.Height - loginForm.Height);

            //btnLogin
            btnLogin.AutoSize = false;
            btnLogin.Width = 50;
            btnLogin.Text = "Login";
            btnLogin.Location = new System.Drawing.Point(btnLogin.Width + 10, btnLogin.Height + 10);
            btnLogin.Click += new EventHandler(DoLogin);

            //btnOption
            btnOptions.AutoSize = false;
            btnOptions.Width = 50;
            btnOptions.Text = "Options";
            btnOptions.Location = new System.Drawing.Point(btnOptions.Width + 10,btnLogin.Height + 20);
            btnOptions.Click += new EventHandler(OpenOptionsWindow);

            //lblPassword
            lblPassword.Text = "Password:";

            //lblUsername
            lblUsername.Text = "Username:";

            //txtPassword
            txtPassword.Location = new System.Drawing.Point(txtPassword.Width + btnLogin.Width + 5, txtPassword.Height + 10);
            txtPassword.UseSystemPasswordChar = true;

            //txtUsername
            txtUsername.Location = new System.Drawing.Point(txtUsername.Width + btnOptions.Width + 5, txtUsername.Height + txtPassword.Height + 10);
        */}
		public override void Input_Controller(Microsoft.Xna.Framework.Input.KeyboardState kCurrentState, Microsoft.Xna.Framework.Input.KeyboardState kPreviousState, Microsoft.Xna.Framework.Input.MouseState mCurrentState, Microsoft.Xna.Framework.Input.MouseState mPreviousState)
		{
			//throw new NotImplementedException();
		}
		public override void Load()
		{
            contentPath = "screens/launcher/";
			//TEMP: Automatically goes to the Main screen w/o login
			RenderEngine.PushScreen(new MainMenu());

			//Load Images
			//backgroundImage = RenderEngine.ContentManager.Load<Texture2D>(contentPath+"background");
			//logoImage = RenderEngine.ContentManager.Load<Texture2D>(contentPath+"logo");
			//TODO: Add the form stuff
            //InitForm();
            //loginForm.Show(Settings.HWND);
		}
		public override void Unload()
		{
			//loginForm.Dispose();
			//TODO: add other unload shit
		}
		public override void Update(GameTime gameTime)
		{
			//TODO: add the update shit
		}
		#endregion

		#region DoLogin
		private void DoLogin(Object sender, EventArgs e)
		{
			//Fuck login...for now.
		}
		#endregion

		#region OpenOptionsWindow
		private void OpenOptionsWindow(object sender, EventArgs e)
		{
			//TODO: add the option window opening shit
		}
		#endregion
		#endregion
	}
}