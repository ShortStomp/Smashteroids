using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityLibrary.Entity;

namespace EntityLibrary.Context
{
	internal interface IEntityContext
	{
		ICollection<IEntity> Entities { get; set; }
	}
}
