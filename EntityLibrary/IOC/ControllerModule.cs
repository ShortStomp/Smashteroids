using Autofac;
using EntityLibrary.Controllers;
using EntityLibrary.Entity;
using EntityLibrary.Repositories;
using EntityLibrary.Repositories.EntityRepository;

namespace EntityLibrary.IOC
{
	public class ControllerModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder
				.Register(c => 
					new EntityController(c.Resolve<IEntityRepository>(), c.Resolve<IEntityFactory>()))
				.As<IEntityController>()
				.SingleInstance();

			builder
				.Register(c =>
					new RenderableController(c.Resolve<IEntityRepository>(), c.Resolve<ITextureRepository>()))
				.As<IRenderableController>()
				.SingleInstance();

			builder
				.Register(c => new AiController())
				.As<IAiController>()
				.SingleInstance();

			builder
				.Register(c => new CollidableController())
				.As<ICollidableController>()
				.SingleInstance();

			base.Load(builder);
		}
	}
}
