using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Minecraft_CS.RenderEngine
{
	class Base : Screen
	{
		public override void Draw(GameTime gameTime, SpriteBatch sb) { }
		public override void Input_Controller(KeyboardState kCurrentState, KeyboardState kPreviousState, MouseState mCurrentState, MouseState mPreviousState) { }
		public override void Load() { }
		public override void Unload() { }
		public override void Update(GameTime gameTime) { }
	}
}
