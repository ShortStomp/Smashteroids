using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace EntityLibrary.Controllers
{
	public interface IRenderableController
	{
		void AddTexture(string filename, Texture2D texture);
		void DrawRenderables(SpriteBatch spriteBatch, GameTime gameTime);
	}
}
