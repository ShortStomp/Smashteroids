using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smashteroids.Components;
using Microsoft.Xna.Framework.Graphics;

namespace Smashteroids.Services
{
	interface IControllable
	{
		Texture2D InputComponent { get; }
	}
}
