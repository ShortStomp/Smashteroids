using System.Collections.Generic;
using EntityLibrary.Components.Base;

namespace EntityLibrary.Entity
{
	internal class EntityFactory : IEntityFactory
	{
		#region IEntityFactory Members

		public IEntity CreateEntity(IEnumerable<Component> componentCollection)
		{
			IEntity entity = new Entity();

			foreach (Component component in componentCollection)
			{
				component.Entity = entity;
				entity.AddComponent(component);
			}

			return entity;
		}

		#endregion
	}
}
