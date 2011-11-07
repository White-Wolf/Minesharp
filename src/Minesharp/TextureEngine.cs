﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;


namespace Minesharp
{
    public static class TextureEngine
    {
        #region Properties
        private static List<Int32> loadedTextures = new List<Int32>();
        #endregion

        #region Functions
        #region Load Textures
        public static int LoadTexture(string filename)
        {
            //Check that the input string is not null or empty and that the file it references actually exists
            if (String.IsNullOrEmpty(filename) || !File.Exists("Resources/" + filename))
                throw new ArgumentException(filename);

            //generate a texture in our GL and assign id to it
            int id = GL.GenTexture();
            //then bind that texture to our GL instance
            GL.BindTexture(TextureTarget.Texture2D, id);

            //load the texture
            Bitmap bmp = new Bitmap("Resources/"+filename);
            //Lock the bitmap data into memory and assign it to bmp_data
            BitmapData bmp_data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            //specify the texture2D in our GL and set it's values
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmp_data.Width, bmp_data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bmp_data.Scan0);

            //unlock the bitmap data
            bmp.UnlockBits(bmp_data);

            // We haven't uploaded mipmaps, so disable mipmapping (otherwise the texture will not appear).
            // On newer video cards, we can use GL.GenerateMipmaps() or GL.Ext.GenerateMipmaps() to create
            // mipmaps automatically. In that case, use TextureMinFilter.LinearMipmapLinear to enable them.
            //this ^
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            //add the texture id to textureEngine's loaded texture list and return the id
            loadedTextures.Add(id);
            return id;
        }
        #endregion

        #region Unload
        public static void Unload()
        {
            //loop through all of our textures and delete them from the GL instance one at a time
            foreach (Int32 id in loadedTextures)
            {
                GL.DeleteTexture(id);
            }
            //unload the loaded textures list
            loadedTextures = null;
        }
        #endregion
        #endregion
    }
}