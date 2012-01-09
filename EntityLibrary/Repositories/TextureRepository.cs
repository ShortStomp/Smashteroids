using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using LogSystem;
using Microsoft.Xna.Framework.Content;
using System.IO;

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

		public void AddTexture(string filename, Texture2D texture)
		{
			if(ContainsTextureWithFilename(filename))
			{
				Logger.WriteExceptionThenQuit(
					MessageType.RuntimeException,
					new InvalidOperationException(string.Format(
						"Adding texture {0} to texture repository failed, texture already in repository."))
					);
			}

			// texture is not already present, ok to add
			_contentManager.Load<Texture2D>("./images/" + filename);
			_textures.Add(filename, texture);
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
