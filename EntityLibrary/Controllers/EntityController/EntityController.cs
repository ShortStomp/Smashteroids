using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityLibrary.Repositories.EntityRepository;
using EntityLibrary.Entity;
using EntityLibrary.Controllers.Base;
using EntityLibrary.EntityIO;

namespace EntityLibrary.Controllers
{
	public class EntityController : Controller, IEntityController
	{
		#region Private Fields

		private IEntityRepository _entityRepository;
		private IEntityFactory _entityFactory;

		#endregion

		#region Internal Constructors

		internal EntityController(IEntityRepository repository, IEntityFactory factory)
		{
			_entityRepository = repository;
			_entityFactory = factory;

			// TODO: somehow make this automatic (base constructor of controllers didn't work out)
			Initialize();
		}

		#endregion

		#region Controller overrides

		protected override void Initialize()
		{
			IEntityParser entityParser = new EntityParser();

			entityParser.OpenFile("../../../Data/Entities.xml");
			var entityDataCollection = entityParser.LoadEntities();

			foreach (var entityData in entityDataCollection)
			{
				_entityRepository.InsertEntity(_entityFactory.CreateEntity(entityData.Components));
			}
		}

		#endregion

		#region IEntityController Members

		public void CreateEntity()
		{
			_entityRepository.InsertEntity(_entityFactory.CreateEntity(null));
		}

		public void AddComponentToEntity(Entity.IEntity entity, Components.Interface.IComponent component)
		{
			throw new NotImplementedException();
		}

		public void AddComponentsToEntity(Entity.IEntity entity, IEnumerable<Components.Interface.IComponent> components)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
