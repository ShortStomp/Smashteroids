using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityLibrary.Message
{
	internal class Message : IMessage
	{
		#region Fields

		internal Delegate _method;
		internal DateTime _timeToDeliver;
		internal object[] _args;

		#endregion

		internal Message(Delegate method, DateTime timeToDeliver, params object[] args)
		{
			_method = method;
			_timeToDeliver = timeToDeliver;
			_args = args;
		}

		public void ExecuteMessage()
		{
			// TODO: for now dynamic invoke is fine. But it is being called in a tight loop,
			// using reflection to figure out the parameters. This will definately need to be changed
			// when performance starts to become a problem.
			_method.DynamicInvoke(_args);
		}

		public DateTime TimeToDeliver()
		{
			return _timeToDeliver;
		}
	}
}
