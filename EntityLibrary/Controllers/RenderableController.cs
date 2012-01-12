using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityLibrary.Repositories;
using EntityLibrary.Controllers.Base;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using EntityLibrary.Components;
using EntityLibrary.Repositories.EntityRepository;
using EntityLibrary.Components.Objects;

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

		public void CreateNewTextureForSprite(string filename, Sprite sprite)
		{
			_textureRepository.CreateTextureForSprite(filename, sprite);
		}

		public void DrawRenderables(SpriteBatch spriteBatch, GameTime gameTime)
		{
			spriteBatch.Begin();

			foreach (RenderableComponent rc in _entityRepository.GetComponentsOfType<RenderableComponent>())
			{
				spriteBatch.Draw(rc.Sprite.Texture, rc.Position, Color.White);
			}

			spriteBatch.End();
		}

		#endregion
	}
}
