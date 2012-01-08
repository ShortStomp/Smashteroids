using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityLibrary.Entity;

namespace EntityLibrary.Context
{
	internal class EntityContext : IEntityContext
	{
		#region IEntityContext Members

		public ICollection<IEntity> Entities { get; set; }

		#endregion

		#region Contructors

		internal EntityContext()
		{
			Entities = new List<IEntity>();
		}

		#endregion

	}
}
