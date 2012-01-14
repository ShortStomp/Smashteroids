using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.ComponentModel.DataAnnotations;

namespace EntityLibrary.Components.Objects
{
	public class Sprite
	{
		#region Fields

		private float _rotation;

		#endregion

		#region Properties

		internal Texture2D Texture { get; set; }
		internal string Filename { get; private set; }
		internal Rectangle DestRect { get; set; }
		internal Rectangle? SourceRect { get; set; }
		internal Color Color { get; set; }

		internal float Rotatation 
		{ 
			get { return _rotation % 360.0f; } 
			set { _rotation = value; } 
		}

		internal Vector2 Origin { get; set; }
		internal float Scale { get; set; }
		internal SpriteEffects SpriteEffect { get; set; }

		[Range(0.0f, 1.0f)]
		internal float DepthLayer { get; set; }

		#endregion

		#region Constructors

		internal Sprite(string filename)
		{
			Filename = filename;
		}

		#endregion
	}
}
