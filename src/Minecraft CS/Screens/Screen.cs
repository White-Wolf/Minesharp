using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Minecraft_CS.RenderEngine
{
	public abstract class Screen
	{
		public abstract void Draw(GameTime gameTime, SpriteBatch sb);
		public abstract void Input_Controller(KeyboardState kCurrentState, KeyboardState kPreviousState, MouseState mCurrentState, MouseState mPreviousState);
		public abstract void Load();
		public abstract void Unload();
		public abstract void Update(GameTime gameTime);
	}
}
