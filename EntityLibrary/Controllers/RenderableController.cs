using System;
using System.Linq;
using EntityLibrary.Components;
using EntityLibrary.Controllers.Base;
using EntityLibrary.Repositories;
using EntityLibrary.Repositories.EntityRepository;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EntityLibrary.Entity;

namespace EntityLibrary.Controllers
{
	internal class RenderableController : Controller, IRenderableController
	{
		#region Fields

		private IEntityRepository _entityRepository;
		private ITextureRepository _textureRepository;

		#endregion

		#region Constructors

		internal RenderableController(IEntityRepository entityRepo, ITextureRepository textureRepo)
		{
			_entityRepository = entityRepo;
			_textureRepository = textureRepo;
		}

		#endregion

		#region Controller overloads

		protected override void Initialize()
		{
			throw new NotImplementedException();
		}

		#endregion

		#region IRenderableController Members

		public Texture2D GetTextureByFilename(string filename)
		{
			return _textureRepository.GetTextureByFilename(filename);
		}

		public void DrawRenderables(SpriteBatch spriteBatch, GameTime gameTime)
		{
			spriteBatch.Begin();

			foreach (IEntity entity in _entityRepository.GetEntitiesWithComponents<SpriteComponent, PositionComponent>())
			{
				var position = entity.GetComponent<PositionComponent>();
				var sprite = entity.GetComponent<SpriteComponent>();
			
				spriteBatch.Draw(
					sprite.Texture,
					position.Position,
					sprite.SourceRect,
					sprite.Color,
					sprite.Rotatation,
					sprite.Origin,
					sprite.Scale,
					sprite.SpriteEffect,
					sprite.DepthLayer);
			}

			spriteBatch.End();
		}

		#endregion
	}
}
