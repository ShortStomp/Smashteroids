using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityLibrary.Components.Objects;

namespace EntityLibrary.Controllers
{
	public interface IPlayerController
	{
		void AddPlayer(Player player);
		void UpdatePlayer();
	}
}
