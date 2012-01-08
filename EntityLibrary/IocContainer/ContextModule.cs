using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using EntityLibrary.Context;

namespace EntityLibrary.IocContainer
{
	internal class ContextModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder
				.Register(c => new EntityContext())
				.As<IEntityContext>()
				.SingleInstance();

			base.Load(builder);
		}
	}
}
