using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityLibrary.Controllers;
using EntityLibrary.Controllers.Base;

namespace EntityLibrary.Message
{
	internal interface IMessageFactory
	{
		void CreateAndSendMessage(Delegate method, float msTimeToWait = 0, params object[] args);
	}
}
