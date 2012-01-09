using System.Collections.Generic;
using EntityLibrary.Components.Interface;

namespace Smashteroids.Data
{
	/// <summary>
	/// An aggregate containener of components, data
	/// </summary>
	internal class EntityData
	{
		internal IEnumerable<IComponent> Components;
	}
}
