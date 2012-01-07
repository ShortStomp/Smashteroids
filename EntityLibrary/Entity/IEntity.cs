using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityLibrary.Components.Interface;

namespace EntityLibrary.Entity
{
	public interface IEntity
	{
		/// <summary>
		/// Collection of components
		/// </summary>
		ICollection<IComponent> Components { get; set; }


		/// <summary>
		/// Determine if the entity contains the component
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <param name="component">The component.</param>
		/// <returns></returns>
		bool ContainsComponent(IComponent component);
	}
}
