using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using EntityLibrary.Message;

namespace EntityLibrary.IOC
{
	internal class MessagingModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder
				.Register(c => new PriorityMessageQueue())
				.As<IPriorityMessageQueue>()
				.SingleInstance();

			base.Load(builder);
		}
	}
}
