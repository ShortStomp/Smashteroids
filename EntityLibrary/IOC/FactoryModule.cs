using Autofac;
using EntityLibrary.Components.Factory;
using EntityLibrary.Entity;
using EntityLibrary.Message;
using EntityLibrary.Controllers;

namespace EntityLibrary.IOC
{
	internal class FactoryModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder
				.Register(c =>
					new MessageFactory(
						c.Resolve<IPriorityMessageQueue>(),
						c.Resolve<IRenderableController>(),
						c.Resolve<IAiController>(),
						c.Resolve<ICollidableController>()))
				.As<IMessageFactory>()
				.SingleInstance();

			builder
				.Register(c => 
					new ComponentFactory(
						c.Resolve<IMessageFactory>(),
						c.Resolve<IRenderableController>(),
						c.Resolve<IPlayerController>()))
				.As<IComponentFactory>()
				.SingleInstance();

			builder
				.Register(c => new EntityFactory())
				.As<IEntityFactory>()
				.SingleInstance();

			base.Load(builder);
		}
	}
}
