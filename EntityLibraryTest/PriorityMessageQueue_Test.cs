using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EntityLibrary.Message;
using Moq;
using EntityLibrary.Message.PQueue;

namespace EntityLibraryTest
{
	[TestClass]
	public class PriorityMessageQueue_Test
	{
		[TestMethod]
		public void AddNullMessage_ArgumentNullException()
		{
			var pq = new Mock<IPriorityQueue<DateTime, IMessage>>();
			var pmq = new PriorityMessageQueue(pq.Object);


			try
			{
				pmq.AddMessage(null);
				Assert.IsTrue(false, "Adding null message should throw an argument null exception.");
			}
			catch (ArgumentNullException)
			{

			}
		}

		[TestMethod]
		public void AddMessage_InternallyCallsEnqueue()
		{
			/* Arrange */
			var pq = new Mock<IPriorityQueue<DateTime, IMessage>>();
			var pmq = new PriorityMessageQueue(pq.Object);
			var stubMessage = new Mock<IMessage>();

			/* Act */
			pmq.AddMessage(stubMessage.Object);

			/* Assert */
			pq.Verify(x => x.Enqueue(It.IsAny<DateTime>(), stubMessage.Object), Times.Once());
		}

		[TestMethod]
		public void AddMessage_WithFutureExpiration_IsNotPendingMessage()
		{
			/* Arrange */
			var pq = new Mock<IPriorityQueue<DateTime, IMessage>>();
			var pmq = new PriorityMessageQueue(pq.Object);
			var stubMessage = new Mock<IMessage>();

			// Setub the message to return a datetime in the future
			stubMessage.Setup(x => x.TimeToDeliver()).Returns(DateTime.Now.AddDays(10));

			/* Act */
			pmq.AddMessage(stubMessage.Object);

			/* Assert */
			Assert.AreEqual(0, pmq.PendingMessageCount);
		}

		[TestMethod]
		public void AddMessage_WithExpirationNow_IsPendingMessage()
		{
			/* Arrange */
			var pq = new Mock<IPriorityQueue<DateTime, IMessage>>();
			var pmq = new PriorityMessageQueue(pq.Object);
			var stubMessage = new Mock<IMessage>();

			// Setub the message to return a datetime in the future
			stubMessage.Setup(x => x.TimeToDeliver()).Returns(DateTime.Now);


			// first call to Dequeue() returns stubMessage, second call returns a default IMessage
			pq.Setup(x => x.Dequeue())
				.Returns(stubMessage.Object)
				.Callback( () => pq.Setup(y => y.Dequeue()) 
					.Returns(default(IMessage)));			

			/* Act */
			pmq.AddMessage(stubMessage.Object);

			/* Assert */
			Assert.AreEqual(1, pmq.PendingMessageCount);
		}
	}
}
