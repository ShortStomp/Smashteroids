using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityLibrary.Components.Interface;
using EntityLibrary.Components.Objects;

namespace EntityLibrary.Components
{
	class PlayerComponent : IComponent
	{
		internal Player Player { get; set; }
	}
}
