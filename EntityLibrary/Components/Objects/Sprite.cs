using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EntityLibrary.Components.Objects
{
	internal class Sprite
	{
		#region Public Properties

		public Vector2 Position { get; set; }
		public Texture2D Texture { get; private set; }
		public string Filename { get; private set; }

		#endregion

		#region Public Constructors

		// TODO: make the first parameter an interface
		public Sprite(string filename)
		{
			Filename = filename;
		}

		/// <summary>
		/// Setter method for Texture
		/// </summary>
		/// <param name="texture"></param>
		public void SetTexture(Texture2D texture)
		{
			Texture = texture;
		}

		#endregion

		#region IMovable Members

		public void Move(Vector2 displacement)
		{
			Position += displacement;
		}

		#endregion
	}
}
