using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityLibrary.Components.Interface;

namespace Smashteroids.Data
{
	/// <summary>
	/// An aggregate containener of components, data
	/// </summary>
	internal class EntityData
	{
		public IEnumerable<IComponent> Components;
	}
}
