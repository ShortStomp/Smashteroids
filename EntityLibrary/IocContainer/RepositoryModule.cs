using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using EntityLibrary.Context;
using EntityLibrary.Repositories.EntityRepository;
using EntityLibrary.Repositories;

namespace EntityLibrary.IocContainer
{
	internal class RepositoryModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder
				.Register(c => 
					new EntityRepository(c.Resolve<IEntityContext>()))
				.As<IEntityRepository>()
				.SingleInstance();

			builder
				.Register(c =>
					new TextureRepository())
				.As<ITextureRepository>()
				.SingleInstance();

			base.Load(builder);
		}
	}
}
