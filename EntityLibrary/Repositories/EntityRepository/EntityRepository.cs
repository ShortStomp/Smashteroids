using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityLibrary.Context;
using EntityLibrary.Entity;
using EntityLibrary.Components.Interface;
using LogSystem;

namespace EntityLibrary.Repositories.EntityRepository
{
	/// <summary>
	/// Repository for Entity's
	/// </summary>
	internal class EntityRepository : IEntityRepository
	{
		#region Private Fields

		private IEntityContext _context;

		#endregion

		#region Internal Constructors

		internal EntityRepository(IEntityContext context)
		{
			_context = context;
		}

		#endregion


		#region IEntityRepository Members

		public void InsertEntity(IEntity entity)
		{
			if (_context.Entities.Contains(entity))
			{
				Logger.WriteExceptionThenQuit(
					MessageType.RuntimeException,
					new InvalidOperationException(
						string.Format("Attempting to add already existing entity {0} to entity repository",
						entity.ToString()))
				);
			}

			// safe to add entity
			_context.Entities.Add(entity);
		}

		public void InsertBatchEntities(IEnumerable<IEntity> entityBatch)
		{
			foreach (IEntity entity in entityBatch)
			{
				InsertEntity(entity);
			}
		}

		public void RemoveEntity(IEntity entity)
		{
			if (!_context.Entities.Contains(entity))
			{
				Logger.WriteExceptionThenQuit(
					MessageType.RuntimeException,
					new InvalidOperationException(
						string.Format("Attempting to remove entity {0} which is not in the entity repository.",
						entity.ToString()))
				);
			}

			// safe to remove entity
			_context.Entities.Remove(entity);
		}


		public IEnumerable<IEntity> GetEntitiesWithComponent(IComponent component)
		{
			return _context.Entities
				.Where(ent => ent.ContainsComponent(component));
		}

		#endregion

		#region Private Members


		private bool EntityInCollection(IEntity entity)
		{
			return _context.Entities
				.SingleOrDefault(ent => (ent == entity)) == default(IEntity);
		}

		#endregion
	}
}
