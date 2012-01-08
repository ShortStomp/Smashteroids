using System.Collections.Generic;
using EntityLibrary.Components.Interface;

namespace EntityLibrary.Entity
{
	internal class EntityFactory : IEntityFactory
	{
		#region IEntityFactory Members

		public IEntity CreateEntity(IEnumerable<IComponent> componentCollection)
		{
			IEntity entity = new Entity();

			foreach (IComponent component in componentCollection)
			{
				entity.AddComponent(component);
			}

			return entity;
		}

		#endregion
	}
}
