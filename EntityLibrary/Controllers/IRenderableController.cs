using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EntityLibrary.Controllers
{
	public interface IRenderableController
	{
		Texture2D GetTextureByFilename(string filename);
		void DrawRenderables(SpriteBatch spriteBatch, GameTime gameTime);
	}
}
