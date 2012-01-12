using System;
using EntityLibrary.Message;
using EntityLibrary.Message.PQueue;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EntityLibraryTest
{
	/// <summary>
	/// Summary description for PriorityQueue_Test
	/// </summary>
	[TestClass]
	public class PriorityQueue_Test
	{
		public PriorityQueue_Test()
		{
			//
			// TODO: Add constructor logic here
			//
		}


		[TestMethod]
		public void EnqueueNullKey_ThrowsArgumentNullException()
		{
			/* Arrange */
			var pq = new PriorityQueue<string, IMessage>();

			try
			{
				/* Act */
				pq.Enqueue(null, null);

				/* Assert */
				Assert.IsTrue(false, "Enqueue with null key should result in ArgumentNullException.");
			}
			catch(ArgumentNullException)
			{
			}
		}

		[TestMethod]
		public void EnqueueNullValue_ThrowsArgumentNullException()
		{
			/* Arrange */
			var pq = new PriorityQueue<string, IMessage>();

			try
			{
				/* Act */
				pq.Enqueue("test", null);

				/* Assert */
				Assert.IsTrue(false, "Enqueue with null message should result in ArgumentNullException.");
			}
			catch(ArgumentNullException)
			{
			}
		}

		[TestMethod]
		public void EnqueueContainsKey_IncreasesCountByOne()
		{
			/* Arrange */
			var msg = new Mock<IMessage>();

			var pq = new PriorityQueue<string, IMessage>();
			pq.Enqueue("", msg.Object);

			/* Act */

			// insert a new message with a duplicate key 
			pq.Enqueue("", msg.Object);

			/* Assert */
			Assert.AreEqual(2, pq.Count);
		}

		[TestMethod]
		public void EnqueueDoesNotContainKey_IncreasesCountByOne()
		{
			/* Arrange */
			var pq = new PriorityQueue<string, IMessage>();
			var msg = new Mock<IMessage>();
			pq.Enqueue("", msg.Object);

			/* Act */
			pq.Enqueue("different", msg.Object);

			/* Assert */
			Assert.AreEqual(2, pq.Count);
		}

		[TestMethod]
		public void PeekWhenEmpty_ReturnsDefaultValue()
		{
			/* Arrange */
			var pq = new PriorityQueue<string, IMessage>();
			var msg = new Mock<IMessage>();
			
			/* Act */
			var result = pq.Peek();

			/* Assert */
			Assert.AreEqual(default(IMessage), result);
		}

		[TestMethod]
		public void PeekWhenQueueContainsTwoItemsSameKey_ReturnsFirst()
		{
			/* Arrange */
			var pq = new PriorityQueue<string, IMessage>();
			var msg = new Mock<IMessage>();
			var msg2 = new Mock<IMessage>();

			pq.Enqueue("", msg.Object);
			pq.Enqueue("", msg2.Object);

			/* Act */
			var result = pq.Peek();

			/* Assert */
			Assert.AreSame(result, msg.Object);
		}

		[TestMethod]
		public void PeekWhenQueueContainsTwoItemsDifferentKeys_ReturnsFirst()
		{
			/* Arrange */
			var pq = new PriorityQueue<string, IMessage>();
			var msg = new Mock<IMessage>();
			var msg2 = new Mock<IMessage>();

			pq.Enqueue("", msg.Object);
			pq.Enqueue("different", msg2.Object);

			/* Act */
			var result = pq.Peek();

			/* Assert */
			Assert.AreSame(result, msg.Object);
		}

		[TestMethod]
		public void DequeueWhenEmpty_ReturnsDefaultValue()
		{
			/* Arrange */
			var pq = new PriorityQueue<string, IMessage>();

			/* Act */
			var result = pq.Dequeue();

			/* Assert */
			Assert.AreEqual(default(IMessage), result);
		}

		[TestMethod]
		public void DequeueWhenSingleEntry_ReturnsValueAndIsEmpty()
		{
			/* Arrange */
			var msg = new Mock<IMessage>();
			var pq = new PriorityQueue<string, IMessage>();

			pq.Enqueue("", msg.Object);

			/* Act */
			var result = pq.Dequeue();

			/* Assert */
			Assert.AreSame(result, msg.Object);
			Assert.AreEqual(true, pq.IsEmpty());
		}

		[TestMethod]
		public void DequeuWithTwoEntries_SameKeys_ReturnsFirst()
		{
			/* Arrange */
			var pq = new PriorityQueue<string, IMessage>();
			var msg = new Mock<IMessage>();
			var msg2 = new Mock<IMessage>();

			pq.Enqueue("", msg.Object);
			pq.Enqueue("", msg2.Object);

			/* Act */
			var result = pq.Dequeue();

			/* Assert */
			Assert.AreSame(result, msg.Object);
		}

		[TestMethod]
		public void DequeueWithTwoEntries_DifferentKeys_ReturnsFirst()
		{
			/* Arrange */
			var pq = new PriorityQueue<string, IMessage>();
			var msg = new Mock<IMessage>();
			var msg2 = new Mock<IMessage>();

			pq.Enqueue("", msg.Object);
			pq.Enqueue("diff", msg2.Object);

			/* Act */
			var result = pq.Dequeue();

			/* Assert */
			Assert.AreSame(result, msg.Object);
		}

		[TestMethod]
		public void CountEmpty_ReturnsZero()
		{
			/* Arrange */
			var pq = new PriorityQueue<string, IMessage>();

			/* Act */
			var result = pq.Count;

			/* Assert */
			Assert.AreEqual(0, pq.Count);
		}

		[TestMethod]
		public void CountOneEntry_ReturnsOne()
		{
			/* Arrange */
			var pq = new PriorityQueue<string, IMessage>();
			var msg = new Mock<IMessage>();
			pq.Enqueue("", msg.Object);

			/* Act */
			var result = pq.Count;

			/* Assert */
			Assert.AreEqual(1, result);
		}
	}
}
