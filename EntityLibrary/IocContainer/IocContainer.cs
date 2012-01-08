using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using EntityLibrary.Repositories.EntityRepository;
using EntityLibrary.Context;

namespace EntityLibrary.IocContainer
{
	public static class IocContainer
	{
		#region  Fields

		private static IContainer _container;

		#endregion
		/// <summary>
		/// Static constructor
		/// </summary>
		static IocContainer()
		{
			SetupIocContainer();
		}


		/// <summary>
		/// Resolve type passed in by T, returning a reference to the resolved class.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T Resolve<T>()
		{
			return _container.Resolve<T>();
		}



		/// <summary>
		/// Setups the ioc container. Register the modules used by the container.
		/// </summary>
		private static void SetupIocContainer()
		{
			var builder = new ContainerBuilder();

			builder
				.RegisterModule(new ContextModule());

			builder
				.RegisterModule(new RepositoryModule());

			builder
				.RegisterModule(new ControllerModule());

			_container = builder.Build();
		}
	}
}
