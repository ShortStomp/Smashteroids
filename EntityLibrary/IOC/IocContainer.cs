﻿using Autofac;
using Autofac.Core;
using Autofac.Core.Registration;
using Microsoft.Xna.Framework;

namespace EntityLibrary.IOC
{
	public static class IocContainer
	{
		#region  Fields

		private static IContainer _container;
		private static Game _game;

		#endregion

		/// <summary>
		/// Static constructor
		/// </summary>
		static IocContainer()
		{
			SetupIocContainer();
		}

		public static void SetGameReference(Game gameReference)
		{
			_game = gameReference;
		}


		/// <summary>
		/// Resolve type passed in by T, returning a reference to the resolved class.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T Resolve<T>()
		{
			try
			{
				return _container.Resolve<T>();
			}
			catch (ComponentNotRegisteredException)
			{
				throw;
			}
			catch (DependencyResolutionException)
			{
				throw;
			}
		}


		/// <summary>
		/// Setups the ioc container. Register the modules used by the container.
		/// </summary>
		private static void SetupIocContainer()
		{
			var builder = new ContainerBuilder();

			builder
				.RegisterModule(new FactoryModule());

			builder
				.RegisterModule(new MessagingModule());

			builder
				.RegisterModule(new ContextModule());

			builder
				.RegisterModule(new RepositoryModule());

			builder
				.RegisterModule(new ControllerModule());

			// Register the reference to the game.
			builder
				.Register(c => _game)
				.As<Game>()
				.SingleInstance();

			_container = builder.Build();
		}
	}
}
