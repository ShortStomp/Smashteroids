using System;
using EntityLibrary.Message;
using EntityLibrary.Message.PQueue;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EntityLibraryTest.cs
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
				pq.Enqueue(null, null);
			}
			catch(ArgumentNullException)
			{
				Assert.IsTrue(true);
				return;
			}
			Assert.IsTrue(false);	
		}
	}
}
