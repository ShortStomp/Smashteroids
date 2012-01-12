using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityLibrary.Message.PQueue
{
	public interface IPriorityQueue<TKey, TValue>  where TKey : IComparable
	{
		void Enqueue(TKey priority, TValue value);
		bool IsEmpty();
		TValue Peek();
		TValue Dequeue();
	}
}
