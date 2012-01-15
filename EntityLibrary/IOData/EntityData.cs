using System.Collections.Generic;
using EntityLibrary.Components.Base;

namespace EntityLibrary.IOData
{
	/// <summary>
	/// An aggregate containener of components, data
	/// </summary>
	internal class EntityData
	{
		internal EntityData()
		{
			Components = new List<Component>();
		}
		internal ICollection<Component> Components { get; set; }
	}
}
