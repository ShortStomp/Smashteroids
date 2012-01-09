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
		void CreateAndSendMessage(Delegate method, DateTime exectuteTime, params object[] args);
	}
}
