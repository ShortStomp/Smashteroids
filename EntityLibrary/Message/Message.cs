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
		internal float _timeToDeliver;
		internal object[] _args;

		#endregion

		internal Message(Delegate method, float timeToDeliver, params object[] args)
		{
			_method = method;
			_timeToDeliver = timeToDeliver;
			_args = args;
		}

		public void ExecuteMessage()
		{
			_method.DynamicInvoke(_args);
		}

		public float TimeToDeliver()
		{
			return _timeToDeliver;
		}
	}
}
