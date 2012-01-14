using System;
using System.Collections.Generic;
using System.Linq;
using EntityLibrary.Message.PQueue;

namespace EntityLibrary.Message
{
	internal class PriorityMessageQueue : IPriorityMessageQueue
	{
		#region Fields

		IPriorityQueue<DateTime, IMessage> _messageQueue;

		#endregion

		#region Constructors

		internal PriorityMessageQueue(IPriorityQueue<DateTime, IMessage> priorityQueue)
		{
			_messageQueue = priorityQueue;
		}

		#endregion

		public void AddMessage(IMessage message)
		{
			if (message == null) { throw new ArgumentNullException("message"); }
			_messageQueue.Enqueue(message.TimeToDeliver(), message);
		}


		public int PendingMessageCount
		{
			get { return PendingMessages().Count(); }
		}


		public void DispatchPendingMessages()
		{
			foreach (var pendingMessage in PendingMessages())
			{
				DispatchMessage(pendingMessage);
			}
		}


		private void DispatchMessage(IMessage messageToDeliver)
		{
			messageToDeliver.ExecuteMessage();
		}


		private IEnumerable<IMessage> PendingMessages()
		{
			ICollection<IMessage> temp = new List<IMessage>();
			var msg = _messageQueue.Dequeue();

			// get all the messages that are past due
			while (msg != default(IMessage) && (msg.TimeToDeliver() <= DateTime.Now))
			{
				temp.Add(msg);
				msg = _messageQueue.Dequeue();
			}

			return temp;
		}
	}
}
