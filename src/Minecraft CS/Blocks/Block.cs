using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Minecraft_CS.Blocks
{
    enum BlockMaterial
    {
        sand = 0
    }
	abstract class Block
	{
		#region Properties
		private String blockName;
		protected Boolean blockConstructorCalled;
		protected Boolean enableStats;
		protected float blockHardness;
		protected float blockResistance;
		public Boolean canBlockGrass;
		public Boolean isBlockContainer;
		public Boolean requiresSelfNotify;
		public Boolean tickOnLoad;
		public Double maxX;
		public Double maxY;
		public Double maxZ;
		public Double minX;
		public Double minY;
		public Double minZ;
		public float blockParticleGravity;
		public float slipperiness;
		public Int16 lightOpacity;
		public Int16 lightValue;
		public Int16 blockIndexInTexture;
		public Int16 blockID;
		public BlockMaterial blockMaterial;
		public SoundEffect soundClothFootstep;
		public SoundEffect soundGlassFootstep;
		public SoundEffect soundGrassFootstep;
		public SoundEffect soundGravelFootstep;
		public SoundEffect soundMetalFootstep;
		public SoundEffect soundPowderFootstep;
		public SoundEffect soundSandFootstep;
		public SoundEffect soundStep;
		public SoundEffect soundStoneFootstep;
		public SoundEffect soundWoodFootstep;
		#endregion
		public abstract Boolean Init();

	}
}
