using System;
using System.Collections.Generic;
using LogSystem;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using EntityLibrary.Components.Objects;

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

		public void CreateTextureForSprite(string filename, Sprite sprite)
		{
			if (ContainsTextureWithFilename(filename))
			{
				sprite.Texture = GetTextureByName(filename);
			}
			else
			{
				// texture is not already present, ok to add
				sprite.Texture = _contentManager.Load<Texture2D>("./images/" + filename);
				_textures.Add(filename, sprite.Texture);
			}
		}


		public Texture2D GetTextureByName(string filename)
		{
			try
			{
				// otherwise return the texture
				return _textures[filename];
			}
			catch (Exception e)
			{
				Logger.WriteExceptionThenQuit(MessageType.RuntimeException, e);
				// TODO: log specific exception
				throw;
			}
		}

		#endregion
	}
}
