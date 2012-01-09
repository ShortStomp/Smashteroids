using System.Collections.Generic;
using EntityLibrary.Components.Interface;

namespace EntityLibrary.Entity
{
	internal interface IEntityFactory
	{
		IEntity CreateEntity(IEnumerable<IComponent> componentCollection);
	}
}
