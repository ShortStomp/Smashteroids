﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityLibrary.Message
{
	public interface IPriorityMessageQueue
	{
		void AddMessage(IMessage message);
		void DispatchPendingMessages();

		int PendingMessageCount { get; }
	}
}
