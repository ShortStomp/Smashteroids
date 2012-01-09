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
		internal object[] _methodArguments;

		#endregion

		internal Message(Delegate method, float timeToDeliver, params object[] args)
		{
			_method = method;
			_timeToDeliver = timeToDeliver;
			_methodArguments = args;
		}

		public void ExecuteMessage()
		{
			// TODO: this will be fun to figure out how to abstract...
			_method.DynamicInvoke(_methodArguments.First().ToString(), _methodArguments[1]);
		}

		public float TimeToDeliver()
		{
			return _timeToDeliver;
		}
	}
}
