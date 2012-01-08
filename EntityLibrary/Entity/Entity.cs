using System.Collections.Generic;
using System.Linq;
using EntityLibrary.Components.Interface;
using LogSystem;
using System;

namespace EntityLibrary.Entity
{
	/// <summary>
	/// TODO: doc
	/// </summary>
	internal class Entity : IEntity
	{
		#region Properties

		private ICollection<IComponent> Components { get; set; }

		#endregion

		#region Constructors

		internal Entity()
		{
			Components = new List<IComponent>();
		}

		#endregion


		#region IEntity Members

		public void AddComponent(IComponent component)
		{
			if (ContainsComponent(component))
			{
				Logger.WriteExceptionThenQuit(
					MessageType.RuntimeException,
					new InvalidOperationException(
						string.Format("Attempting to add already existing component {0} to entity {1}",
						component.ToString(), this.ToString()))
				);
			}

			// safe to attach component to entity
			Components.Add(component);
		}


		public void RemoveComponent(IComponent component)
		{
			if (!ContainsComponent(component))
			{
				Logger.WriteExceptionThenQuit(
					MessageType.RuntimeException,
					new InvalidOperationException(
						string.Format("Attempting to remove non-existing component {0} to entity {1}.",
						component.ToString(), this.ToString()))
				);
			}

			// safe to remove component
			Components.Remove(component);
		}


		/// <summary>
		/// Return true if the entity contains the component, otherwise false.
		/// </summary>
		/// <param name="component"></param>
		/// <returns></returns>
		public bool ContainsComponent(IComponent component)
		{
			return Components
				.SingleOrDefault(com => (com == component)) != default(IComponent);
		}


		/// <summary>
		/// Return the components this entity has as a collection.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<IComponent> GetComponents()
		{
			return Components;
		}

		#endregion
	}
}
