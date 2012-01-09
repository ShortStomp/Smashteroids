using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace EntityLibrary.Repositories
{
	internal interface ITextureRepository
	{
		void AddTexture(string filename, Texture2D texture);
		bool ContainsTextureWithFilename(string filename);

		Texture2D GetTextureByName(string filename);
	}
}
