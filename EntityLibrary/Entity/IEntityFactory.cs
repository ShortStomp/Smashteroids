using System.Collections.Generic;
using EntityLibrary.Components.Base;

namespace EntityLibrary.Entity
{
	internal interface IEntityFactory
	{
		IEntity CreateEntity(IEnumerable<Component> componentCollection);
	}
}
