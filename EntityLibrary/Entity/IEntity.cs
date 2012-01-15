using System.Collections.Generic;
using Microsoft.Xna.Framework;
using EntityLibrary.Components.Base;
using System.Collections;

namespace EntityLibrary.Entity
{
	internal interface IEntity : IEnumerable
	{
		/// <summary>
		/// Add a component to the entity.
		/// </summary>
		/// <param name="component"></param>
		void AddComponent(Component component);


		/// <summary>
		/// Remove a component from the entity.
		/// </summary>
		/// <param name="component">The component to remove.</param>
		void RemoveComponent(Component component);


		/// <summary>
		/// Gets the components as a collection.
		/// </summary>
		/// <returns></returns>
		IEnumerable<Component> GetComponents();


		/// <summary>
		/// Gets a reference to the component of type T within the entity.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		T GetComponent<T>() where T : Component;


		/// <summary>
		/// Determine if the entity contains the component
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <param name="component">The component.</param>
		/// <returns></returns>
		bool ContainsComponent<T>() where T : Component;


		/// <summary>
		/// Every entity has a position.
		/// </summary>
		/// <value>
		/// The position.
		/// </value>
		Vector2 Position { get; set; }
	}
}
