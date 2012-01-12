using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityLibrary.Message
{
	public interface IMessage
	{
		bool IsDispatched();
		void ExecuteMessage();
		DateTime TimeToDeliver();
	}
}
