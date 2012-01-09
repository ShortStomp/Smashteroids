using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityLibrary.Repositories;
using EntityLibrary.Controllers.Base;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using EntityLibrary.Components;

namespace EntityLibrary.Controllers
{
	internal class RenderableController : Controller, IRenderableController
	{
		#region Fields

		private IEntityController _entityController;
		private ITextureRepository _textureRepository;

		#endregion

		#region Constructors

		internal RenderableController(IEntityController controller, ITextureRepository textureRepo)
		{
			_entityController = controller;
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

		public void AddTexture(string filename, Texture2D texture)
		{
			if (!_textureRepository.ContainsTextureWithFilename(filename))
			{
				_textureRepository.AddTexture(filename, texture);
			}
		}

		public void DrawRenderables(SpriteBatch spriteBatch, GameTime gameTime)
		{
			spriteBatch.Begin();

			foreach(RenderableComponent rc in _entityController.RenderableComponents())
			{
				spriteBatch.Draw(rc.Sprite.Texture, rc.Sprite.Position, Color.White);
			}

			spriteBatch.End();
		}

		#endregion
	}
}
