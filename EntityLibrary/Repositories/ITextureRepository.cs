using EntityLibrary.Components;
using Microsoft.Xna.Framework.Graphics;

namespace EntityLibrary.Repositories
{
	internal interface ITextureRepository
	{
		void CreateTextureForSprite(string filename, SpriteComponent sprite);
		bool ContainsTextureWithFilename(string filename);

		Texture2D GetTextureByFilename(string filename);
	}
}
