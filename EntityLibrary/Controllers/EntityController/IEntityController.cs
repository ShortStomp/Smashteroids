using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityLibrary.Repositories.EntityRepository;
using EntityLibrary.Entity;
using EntityLibrary.Components.Interface;
using EntityLibrary.Components;

namespace EntityLibrary.Controllers
{
	public interface IEntityController
	{
		void CreateEntity();
	}
}
