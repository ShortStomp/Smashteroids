using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityLibrary.Repositories;
using EntityLibrary.Controllers.Base;

namespace EntityLibrary.Controllers
{
	internal class RenderableController : Controller, IRenderableController
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

		#region Controller overloads

		protected override void Initialize()
		{
			throw new NotImplementedException();
		}

		#endregion


	}
}
