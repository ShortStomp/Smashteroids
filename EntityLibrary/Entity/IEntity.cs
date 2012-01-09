using System.Collections.Generic;
using EntityLibrary.Components.Interface;

namespace EntityLibrary.Entity
{
	public interface IEntity
	{
		/// <summary>
		/// Add a component to the entity.
		/// </summary>
		/// <param name="component"></param>
		void AddComponent(IComponent component);


		// Remove a component from the entity.
		void RemoveComponent(IComponent component);


		/// <summary>
		/// Gets the components as a collection.
		/// </summary>
		/// <returns></returns>
		IEnumerable<IComponent> GetComponents();


		/// <summary>
		/// Gets a reference to the component of type T within the entity.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		T GetComponent<T>() where T : IComponent;


		/// <summary>
		/// Determine if the entity contains the component
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <param name="component">The component.</param>
		/// <returns></returns>
		bool ContainsComponent<T>() where T : IComponent;
	}
}
