using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using LogSystem;

namespace EntityLibrary.Repositories
{
	internal class TextureRepository : ITextureRepository
	{
		#region Fields

		private IDictionary<string, Texture2D> _textures;

		#endregion

		#region Constructor

		internal TextureRepository()
		{
			_textures = new Dictionary<string, Texture2D>();
		}

		#endregion

		#region Members

		public void AddTexture(string filename, Texture2D texture)
		{
			if(_textures.Keys.Contains(filename))
			{
				Logger.WriteExceptionThenQuit(
					MessageType.RuntimeException,
					new InvalidOperationException(string.Format(
						"Adding texture {0} to texture repository failed, texture already in repository."))
					);
			}

			// texture is not already present, ok to add.
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
