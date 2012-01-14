using System;
using System.Collections.Generic;
using System.Linq;
using EntityLibrary.Components.Interface;
using LogSystem;
using EntityLibrary.Components;

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

		#region Private Members

		private void ResolveComponentDependencies()
		{
			var r = Components.Where(c => c.GetType() == typeof(RenderableComponent));
			var p = Components.Where(c => c.GetType() == typeof(PlayerComponent));

			// if the entity has both renderable component and a player component
			if (r.Any() && p.Any())
			{
				(p.FirstOrDefault() as PlayerComponent).Player._sprite = (r.FirstOrDefault() as RenderableComponent).Sprite;
			}

			// otherwise we need to check if we need to remove any dependences (ie when removing a component dynamically)
			else
			{
				// if there is a player, and no renderable component
				// NOTE: the !r.any() is unnecessary for now, but leaving it in for when we expand this system.
				if (p.Any() && !r.Any())
				{
					(r.FirstOrDefault() as RenderableComponent).Sprite = null;
				}
			}
		}

		#endregion

		#region IEntity Members

		public void AddComponent(IComponent component)
		{
			if (ContainsComponent(component))
			{
				DefaultLogger.WriteExceptionThenQuit(
					MessageType.RuntimeException,
					new InvalidOperationException(
						string.Format("Attempting to add already existing component {0} to entity {1}",
						component.ToString(), this.ToString()))
				);
			}

			// safe to attach component to entity
			Components.Add(component);

			ResolveComponentDependencies();
		}


		public void RemoveComponent(IComponent component)
		{
			if (!ContainsComponent(component))
			{
				DefaultLogger.WriteExceptionThenQuit(
			        MessageType.RuntimeException,
			        new InvalidOperationException(
			            string.Format("Attempting to remove non-existing component {0} to entity {1}.",
			            component.ToString(), this.ToString()))
			    );
			}

			// safe to remove component
			Components.Remove(component);

			ResolveComponentDependencies();
		}


		/// <summary>
		/// Return true if the entity contains the component, otherwise false.
		/// </summary>
		/// <param name="component"></param>
		/// <returns></returns>
		public bool ContainsComponent(IComponent component)
		{
			return Components
				.Where(c => c.GetType() == component.GetType())
				.Any();
		}


		public T GetComponent<T>() where T : IComponent
		{
			return Components
				.OfType<T>()
				.SingleOrDefault();
		}


		/// <summary>
		/// Return the components this entity has as a collection.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<IComponent> GetComponents()
		{
			return Components;
		}


		public bool ContainsComponent<T>() where T : IComponent
		{
			return Components
				.OfType<T>()
				.Any();
		}

		#endregion
	}
}
