using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using EntityLibrary.Message;
using EntityLibrary.Message.PQueue;

namespace EntityLibrary.IOC
{
	internal class MessagingModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder
				.Register(c => new PriorityQueue<DateTime, IMessage>())
				.As<IPriorityQueue<DateTime, IMessage>>()
				.SingleInstance();

			builder
				.Register(c => 
					new PriorityMessageQueue(c.Resolve<IPriorityQueue<DateTime, IMessage>>()))
				.As<IPriorityMessageQueue>()
				.SingleInstance();

			base.Load(builder);
		}
	}
}
