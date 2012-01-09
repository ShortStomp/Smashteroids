using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using EntityLibrary.Components.Objects;

namespace EntityLibrary.Repositories
{
	internal interface ITextureRepository
	{
		void CreateTextureForSprite(string filename, Sprite sprite);
		bool ContainsTextureWithFilename(string filename);

		Texture2D GetTextureByName(string filename);
	}
}
