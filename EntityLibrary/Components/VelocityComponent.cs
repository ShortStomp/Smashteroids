using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityLibrary.Components.Base;
using Microsoft.Xna.Framework;

namespace EntityLibrary.Components
{
	internal class VelocityComponent : Component
	{
		internal Vector2 Velocity { get; set; }
	}
}
