using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityLibrary.Components.Base;
using Microsoft.Xna.Framework;

namespace EntityLibrary.Components
{
	internal class AccelerationComponent : Component
	{
		internal Vector2 Acceleration { get; set; }
	}
}
