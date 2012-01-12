using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EntityLibrary.Message.PQueue
{
	public class PriorityQueue<TKey, TValue> : IPriorityQueue<TKey, TValue> where TKey : IComparable
	{
		#region Fields

		// TODO: is list the best unerlying implementation??
		private SortedDictionary<TKey, List<TValue>> _queue;

		#endregion

		#region Constructors

		public PriorityQueue()
		{
			_queue = new SortedDictionary<TKey, List<TValue>>();
		}

		#endregion

		#region IPriorityQueue

		public void Enqueue(TKey priority, TValue value)
		{
			if (priority == null) { throw new ArgumentNullException("priority"); }

			if (_queue.ContainsKey(priority))
			{
				// queue contains key, therefore just add value to existing key's list
				_queue[priority].Add(value);
			}

			// otherwise just add pair
			else
			{
				_queue.Add(priority, new List<TValue>() { value });
			}
		}


		public bool IsEmpty()
		{
			return !_queue.Any();
		}


		public TValue Peek()
		{
			if (IsEmpty())
			{
				// queue is empty, return a default TValue
				return default(TValue);
			}

			return _queue.Values.ElementAt(0).First();
		}


		public TValue Dequeue()
		{
			// check if there is at-least one item in the queue
			if (!_queue.Any())
			{
				// queue is empty, return default TValue
				return default(TValue);
			}

			// at least one item in queue
			IList<TValue> values = _queue.ElementAt(0).Value;
			TValue result = values.First();

			/* if there is more then one item in the values list,
			 * remove only the first item in the values list for the key */
			if (values.Count > 1)
			{
				// remove the value from only the values list
				values.RemoveAt(0);
			}
			else
			{
				// if there's only one item in the values list, remove the whole entry
				_queue.Remove(_queue.ElementAt(0).Key);
			}

			return result;
		}

		#endregion
	}
}
