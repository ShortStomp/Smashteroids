using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityLibrary.Repositories.EntityRepository;
using EntityLibrary.Entity;
using EntityLibrary.Components.Interface;

namespace EntityLibrary.Controllers
{
	public interface IEntityController
	{
		void CreateEntity();
		void AddComponentToEntity(IEntity entity, IComponent component);
		void AddComponentsToEntity(IEntity entity, IEnumerable<IComponent> components);
	}
}
