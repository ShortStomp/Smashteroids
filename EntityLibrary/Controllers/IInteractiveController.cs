using Microsoft.Xna.Framework;
using EntityLibrary.Components;

namespace EntityLibrary.Controllers
{
	public interface IInteractiveController
	{
		//void AddPlayer(PlayerComponent player);
		void UpdatePlayer(GameTime gameTime);
	}
}
