using System.Collections.Generic;
using System.Linq;
using EntityLibrary.Components.Interface;

namespace EntityLibrary.Entity
{
	public class Entity : IEntity
	{
		#region Properties
		public ICollection<IComponent> Components { get; set; }

		#endregion
		#region IEntity Members


		public bool ContainsComponent(IComponent component)
		{
			return Components
				.SingleOrDefault(com => (com == component)) == default(IComponent);
		}

		#endregion
	}
}
