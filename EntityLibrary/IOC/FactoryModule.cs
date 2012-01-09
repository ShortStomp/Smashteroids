using Autofac;
using EntityLibrary.Components.Factory;
using EntityLibrary.Entity;

namespace EntityLibrary.IOC
{
	internal class FactoryModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder
				.Register(c => new ComponentFactory())
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
