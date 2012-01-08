using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smashteroids.Extensions
{
	public static class IEnumerableExtensions
	{
		/// <summary>
		/// Apply an action to each item in collection, without making a 
		/// copy of the collection. Allows use of the linq syntax without the list-copy
		/// overhead.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source">The source.</param>
		/// <param name="action">The action.</param>
		public static void ForEachNoCopy<T>(this IEnumerable<T> collection, Action<T> action)
		{
			// Validate parameters for public methods
			if (collection != null && action != null) 
			{
				foreach (var item in collection)
				{
					action(item);
				}
			}
		}
	}
}
