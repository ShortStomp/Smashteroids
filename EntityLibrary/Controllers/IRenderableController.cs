using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using EntityLibrary.Components.Objects;

namespace EntityLibrary.Controllers
{
	public interface IRenderableController
	{
		void CreateNewTextureForSprite(string filename, Sprite sprite);
		void DrawRenderables(SpriteBatch spriteBatch, GameTime gameTime);
	}
}
