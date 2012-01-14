using System;
using System.Linq;
using EntityLibrary.Components.Objects;
using LogSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace EntityLibrary.Controllers
{
	internal class PlayerController : IPlayerController
	{
		#region Fields

		private Player _player;

		#endregion

		#region Constructors

		internal PlayerController()
		{
		}

		#endregion

		#region IPlayerController Members

		public void AddPlayer(Player player)
		{
			if (player == null)
			{
				DefaultLogger.WriteExceptionThenQuit(MessageType.RuntimeException, new ArgumentNullException("player"));
			}

			if (_player != null)
			{
				DefaultLogger.WriteLine(MessageType.Warning, "Adding component with player, when one already exists. Loading two playable entities.", 1, 1);
			}

			_player = player;
		}

		public void UpdatePlayer()
		{
			const int translationDistance = 1;
			const float rotationMultiplier = 1.0f;

			Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys();

			if (pressedKeys.Contains(Keys.W))
			{
				_player._sprite.DestRect = new Rectangle(_player._sprite.DestRect.Location.X, _player._sprite.DestRect.Y - translationDistance,
					_player._sprite.DestRect.Width, _player._sprite.DestRect.Height);
			}
			if (pressedKeys.Contains(Keys.S))
			{
				_player._sprite.DestRect = new Rectangle(_player._sprite.DestRect.Location.X, _player._sprite.DestRect.Y + translationDistance,
					_player._sprite.DestRect.Width, _player._sprite.DestRect.Height);
			}
			if (pressedKeys.Contains(Keys.A))
			{
				_player._sprite.DestRect = new Rectangle(_player._sprite.DestRect.Location.X - translationDistance, _player._sprite.DestRect.Y,
					_player._sprite.DestRect.Width, _player._sprite.DestRect.Height);
			}
			if (pressedKeys.Contains(Keys.D))
			{
				_player._sprite.DestRect = new Rectangle(_player._sprite.DestRect.Location.X + translationDistance, _player._sprite.DestRect.Y,
					_player._sprite.DestRect.Width, _player._sprite.DestRect.Height);
			}
			if (pressedKeys.Contains(Keys.J))
			{
				_player._sprite.Rotatation += 0.1f * rotationMultiplier;
			}
			if (pressedKeys.Contains(Keys.L))
			{
				_player._sprite.Rotatation -= 0.1f * rotationMultiplier;
			}
		}

		#endregion
	}
}
