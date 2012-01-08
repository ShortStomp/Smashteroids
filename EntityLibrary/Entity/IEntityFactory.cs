using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityLibrary.Components.Interface;

namespace EntityLibrary.Entity
{
	internal interface IEntityFactory
	{
		IEntity CreateEntity(IEnumerable<IComponent> componentCollection);
	}
}
