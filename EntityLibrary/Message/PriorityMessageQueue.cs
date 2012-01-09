using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityLibrary.Message
{
	internal class PriorityMessageQueue : IPriorityMessageQueue
	{
		#region Fields

		SortedList<DateTime, IMessage> _messageQueue;

		#endregion

		#region Constructors

		internal PriorityMessageQueue()
		{
			_messageQueue = new SortedList<DateTime, IMessage>();
		}

		#endregion

		public void AddMessage(IMessage message)
		{
			_messageQueue.Add(message.TimeToDeliver(), message);
		}

		public bool IsEmpty()
		{
			return !_messageQueue.Any();
		}

		public void DispatchMessage(IMessage messageToDeliver)
		{
			messageToDeliver.ExecuteMessage();
		}

		public IEnumerable<IMessage> PendingMessages()
		{
			ICollection<IMessage> temp = new List<IMessage>();
			var j = _messageQueue.TakeWhile(x => x.Key <= DateTime.Now);

			foreach (var pair in j)
			{
				temp.Add(pair.Value);
			}

			return temp;
		}
	}
}
