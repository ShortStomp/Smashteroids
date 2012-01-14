using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace EntityLibrary.Components.Objects
{
	public class Player
	{
		#region Properties

		internal string _name { get; set; }
		internal Sprite _sprite { get; set; }

		#endregion

		internal Player()
		{
		}
	}
}
