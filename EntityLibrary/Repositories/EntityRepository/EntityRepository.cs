using System;
using System.Collections.Generic;
using System.Linq;
using EntityLibrary.Context;
using EntityLibrary.Entity;
using LogSystem;
using EntityLibrary.Components.Base;

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
				DefaultLogger.WriteExceptionThenQuit(
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
				DefaultLogger.WriteExceptionThenQuit(
					MessageType.RuntimeException,
					new InvalidOperationException(
						string.Format("Attempting to remove entity {0} which is not in the entity repository.",
						entity.ToString()))
				);
			}

			// safe to remove entity
			_context.Entities.Remove(entity);
		}


		/// <summary>
		/// Gets all entities that have the matching component type.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="U"></typeparam>
		/// <returns></returns>
		public IEnumerable<IEntity> GetEntitiesWithComponents<T, U, V>() 
			where T : Component 
			where U : Component
			where V : Component
		{
			return GetEntitiesWithComponents<T, U>()
				.Union(GetEntitiesWithComponents<V>());
		}


		/// <summary>
		/// Gets all entities that have the matching component type.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="U"></typeparam>
		/// <returns></returns>
		public IEnumerable<IEntity> GetEntitiesWithComponents<T, U>() 
			where T : Component 
			where U : Component
		{
			return GetEntitiesWithComponents<T>()
				.Union(GetEntitiesWithComponents<U>());
		}



		/// <summary>
		/// Gets all entities with the mathing component type.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public IEnumerable<IEntity> GetEntitiesWithComponents<T>() where T : Component
		{
			return _context.Entities
				.Where(entity =>
					entity.ContainsComponent<T>() == true);
		}



		/// <summary>
		/// Get all components with the component type T.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public IEnumerable<T> GetComponentsOfType<T>() where T : Component
		{
			// todo: cache this list probably
			ICollection<T> components = new List<T>(_context.Entities.Count);

			foreach (IEntity entity in _context.Entities)
			{
				if (entity.ContainsComponent<T>())
				{
					components.Add(entity.GetComponent<T>());
				}
			}

			// done processing, return the collection
			return components;
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
