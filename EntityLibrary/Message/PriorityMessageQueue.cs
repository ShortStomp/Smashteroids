using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityLibrary.Message
{
	internal class PriorityMessageQueue : IPriorityMessageQueue
	{
		#region Fields

		SortedList<float, IMessage> _messageQueue;

		#endregion
		internal PriorityMessageQueue()
		{
			_messageQueue = new SortedList<float, IMessage>();
		}

		public void AddMessage(IMessage message)
		{
			_messageQueue.Add(message.TimeToDeliver(), message);
		}

		public bool IsEmpty()
		{
			return !_messageQueue.Any();
		}

		public void DispatchMessage()
		{
			_messageQueue
				.FirstOrDefault()
				.Value.ExecuteMessage();
		}
	}
}
