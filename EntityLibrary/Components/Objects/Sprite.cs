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
		#region Properties

		internal Texture2D Texture { get; set; }
		internal string Filename { get; private set; }

		#endregion

		#region Constructors

		// TODO: make the first parameter an interface
		internal Sprite(string filename)
		{
			Filename = filename;
		}

		#endregion
	}
}
