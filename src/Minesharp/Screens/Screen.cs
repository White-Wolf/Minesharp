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
        protected Dictionary<String, Texture> textures;
        public Screen()
        {
            textures = new Dictionary<string, Texture>();
        }
        protected string screenFolder = "Screens/";
        public virtual void Draw() { }
        public virtual void Input_Controller(MouseDevice md, KeyboardDevice kd) { }
        public virtual void Load() { }
        public virtual void Unload()
        {
            foreach (KeyValuePair<string, Texture> textkeyval in textures)
            {
                GL.DeleteTexture(textkeyval.Value.Id);
                TextureEngine.RemoveFromList(textkeyval.Value.Id);
            }
            textures.Clear();
            textures = null;
        }
        public virtual void Update(FrameEventArgs e) { }
        protected Texture LoadTexture(String texture)
        {
            return TextureEngine.LoadTexture(screenFolder + texture);
        }
    }
}
