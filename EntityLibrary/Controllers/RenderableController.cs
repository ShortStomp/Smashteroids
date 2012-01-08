using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityLibrary.Repositories;

namespace EntityLibrary.Controllers
{
	internal class RenderableController : IRenderableController
	{
		#region Fields
		
		private ITextureRepository _textureRepository;

		#endregion

		#region Constructors

		internal RenderableController(ITextureRepository textureRepo)
		{
			_textureRepository = textureRepo;
		}

		#endregion


	}
}
