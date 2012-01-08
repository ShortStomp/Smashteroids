using Autofac;
using EntityLibrary.Repositories.EntityRepository;
using EntityLibrary.Controllers;
using EntityLibrary.Entity;
using EntityLibrary.Repositories;

namespace EntityLibrary.IocContainer
{
	public class ControllerModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder
				.Register(c => 
					new EntityController(c.Resolve<IEntityRepository>(), new EntityFactory()))
				.As<IEntityController>()
				.SingleInstance();

			builder
				.Register(c => 
					new RenderableController(c.Resolve<ITextureRepository>()))
				.As<IRenderableController>()
				.SingleInstance();

			base.Load(builder);
		}
	}
}
