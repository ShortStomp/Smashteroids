using System;
using System.Collections.Generic;
using System.Linq;
using EntityLibrary.Components;
using EntityLibrary.Components.Base;
using LogSystem;
using Microsoft.Xna.Framework;
using System.Collections;

namespace EntityLibrary.Entity
{
	/// <summary>
	/// TODO: doc
	/// </summary>
	internal class Entity : IEntity
	{
		#region Properties

		public Vector2 Position { get; set; }

		#endregion

		#region Fields

		private ICollection<Component> _components;

		#endregion

		#region Constructors

		internal Entity()
		{
			_components = new List<Component>();
		}

		#endregion

		#region Private Members

		//private void ResolveComponentDependencies()
		//{
		//    var r = Components.Where(c => c.GetType() == typeof(RenderableComponent));
		//    var p = Components.Where(c => c.GetType() == typeof(PlayerComponent));

		//    // if the entity has both renderable component and a player component
		//    if (r.Any() && p.Any())
		//    {
		//        (p.FirstOrDefault() as PlayerComponent).Player.Sprite = (r.FirstOrDefault() as RenderableComponent).Sprite;
		//    }

		//    // otherwise we need to check if we need to remove any dependences (ie when removing a component dynamically)
		//    else
		//    {
		//        // if there is a player, and no renderable component
		//        // NOTE: the !r.any() is unnecessary for now, but leaving it in for when we expand this system.
		//        if (p.Any() && !r.Any())
		//        {
		//            (r.FirstOrDefault() as RenderableComponent).Sprite = null;
		//        }
		//    }
		//}

		#endregion

		#region IEntity Members

		public void AddComponent(Component component)
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
			_components.Add(component);

			//ResolveComponentDependencies();
		}


		public void RemoveComponent(Component component)
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
			_components.Remove(component);

			//ResolveComponentDependencies();
		}


		/// <summary>
		/// Return true if the entity contains the component, otherwise false.
		/// </summary>
		/// <param name="component"></param>
		/// <returns></returns>
		public bool ContainsComponent(Component component)
		{
			return _components
				.Where(c => c.GetType() == component.GetType())
				.Any();
		}


		public T GetComponent<T>() where T : Component
		{
			return _components
				.OfType<T>()
				.SingleOrDefault();
		}


		/// <summary>
		/// Return the components this entity has as a collection.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<Component> GetComponents()
		{
			return _components;
		}


		public bool ContainsComponent<T>() where T : Component
		{
			return _components
				.OfType<T>()
				.Any();
		}

		#endregion

		#region IEnumerable Members

		public IEnumerator GetEnumerator()
		{
			return _components.GetEnumerator();
		}

		#endregion
	}
}
