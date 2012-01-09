using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EntityLibrary.Components.Objects
{
	public class Sprite
	{
		#region Public Properties

		public Texture2D Texture { get; set; }
		public string Filename { get; private set; }

		#endregion

		#region Public Constructors

		// TODO: make the first parameter an interface
		public Sprite(string filename)
		{
			Filename = filename;
		}

		#endregion
	}
}
