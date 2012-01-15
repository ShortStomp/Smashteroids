using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityLibrary.Components.Base;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.ComponentModel.DataAnnotations;

namespace EntityLibrary.Components
{
	internal class SpriteComponent : Component
	{
		#region Fields

		private float _rotation;

		#endregion

		#region Properties

		internal Texture2D Texture { get; set; }
		internal string Filename { get; set; }
		internal Rectangle? SourceRect { get; set; }
		internal Color Color { get; set; }

		[Range(0.0f, float.PositiveInfinity)]
		internal float Width { get; set; }

		[Range(0.0f, float.PositiveInfinity)]
		internal float Height { get; set; }

		internal float Rotatation
		{
			get { return _rotation % 360.0f; }
			set { _rotation = value; }
		}

		internal Vector2 Origin { get; set; }
		internal Vector2 Scale { get; set; }
		internal SpriteEffects SpriteEffect { get; set; }

		[Range(0.0f, 1.0f)]
		internal float DepthLayer { get; set; }

		#endregion
	}
}
