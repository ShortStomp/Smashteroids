using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityLibrary.Controllers.Base
{
	public abstract class Controller
	{
		protected Controller()
		{
			//Initialize();
		}
		protected abstract void Initialize();
	}
}
