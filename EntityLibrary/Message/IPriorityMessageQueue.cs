using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityLibrary.Message
{
	public interface IPriorityMessageQueue
	{
		bool IsEmpty();

		void AddMessage(IMessage message);
		void DispatchMessage();
	}
}
