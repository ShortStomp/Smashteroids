using EntityLibrary.Entity;

namespace EntityLibrary.Components.Base
{
	internal abstract class Component
	{
		internal IEntity Entity { get; set; }
	}
}
