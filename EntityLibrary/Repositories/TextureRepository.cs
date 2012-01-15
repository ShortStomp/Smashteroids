using System.Collections.Generic;
using EntityLibrary.Components;
using LogSystem;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace EntityLibrary.Repositories
{
	internal class TextureRepository : ITextureRepository
	{
		#region Fields

		private IDictionary<string, Texture2D> _textures;
		private ContentManager _contentManager;

		#endregion

		#region Constructor

		internal TextureRepository(ContentManager manager)
		{
			_contentManager = manager;
			_textures = new Dictionary<string, Texture2D>();
		}

		#endregion

		#region Members

		public bool ContainsTextureWithFilename(string filename)
		{
			return _textures.Keys.Contains(filename);
		}

		public void CreateTextureForSprite(string filename, SpriteComponent sprite)
		{
			if (ContainsTextureWithFilename(filename))
			{
				sprite.Texture = GetTextureByFilename(filename);
			}
			else
			{
				// texture is not already present, ok to add
				sprite.Texture = _contentManager.Load<Texture2D>("./images/" + filename);

				DefaultLogger.WriteLine(MessageType.Information, string.Format("Texture {0} loaded into texture repository. Texture width: {1}, height {2}.", 
					sprite.Texture.Name, sprite.Texture.Width, sprite.Texture.Height));

				_textures.Add(filename, sprite.Texture);
			}
		}


		public Texture2D GetTextureByFilename(string filename)
		{
			if (ContainsTextureWithFilename(filename))
			{
				return _textures[filename];
			}

			try
			{
				// texture is not already present, ok to add
				Texture2D texture = _contentManager.Load<Texture2D>("./images/" + filename);

				DefaultLogger.WriteLine(MessageType.Information, string.Format("Texture {0} loaded into texture repository. Texture width: {1}, height {2}.",
					texture.Name, texture.Width, texture.Height));

				_textures.Add(filename, texture);
				return texture;
			}
			catch (ContentLoadException cle)
			{
				DefaultLogger.WriteExceptionThenQuit(MessageType.RuntimeException, cle);
				return default(Texture2D);
			}
		}

		#endregion
	}
}
