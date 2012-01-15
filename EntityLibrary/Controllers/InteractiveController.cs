using System;
using System.Linq;
using LogSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using EntityLibrary.Components;
using EntityLibrary.Repositories.EntityRepository;

namespace EntityLibrary.Controllers
{
	internal class InteractiveController : IInteractiveController
	{
		#region Fields

		private IEntityRepository _entityRepository;

		#endregion

		#region Constructors

		internal InteractiveController(IEntityRepository er)
		{
			_entityRepository = er;
		}

		#endregion

		#region IPlayerController Members

		//public void AddPlayer(PlayerComponent player)
		//{
		//    //if (player == null)
		//    //{
		//    //    DefaultLogger.WriteExceptionThenQuit(MessageType.RuntimeException, new ArgumentNullException("player"));
		//    //}

		//    //if (_player != null)
		//    //{
		//    //    DefaultLogger.WriteLine(MessageType.Warning, "Adding component with player, when one already exists. Loading two playable entities.", 1, 1);
		//    //}

		//    //_player = player;
		//}

		public void UpdatePlayer(GameTime gameTime)
		{
			const float accelerationRate = 0.001f;
			//const float rotationMultiplier = 1.0f;

			var t = _entityRepository
				.GetEntitiesWithComponents<PlayerComponent, VelocityComponent, PositionComponent>()
				.SingleOrDefault();

			var vc = t.GetComponent<VelocityComponent>();
			var pc = t.GetComponent<PositionComponent>();
			

			var player = _entityRepository.GetComponentsOfType<PlayerComponent>().SingleOrDefault();

			Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys();

			if (Keyboard.GetState().IsKeyUp(Keys.W) && vc.Velocity.Y < 0.0f)
			{
				vc.Velocity = new Vector2(vc.Velocity.X, vc.Velocity.Y + accelerationRate);
			}
			if (pressedKeys.Contains(Keys.W))
			{
				vc.Velocity = new Vector2(vc.Velocity.X, vc.Velocity.Y - accelerationRate);
			}

			if (Keyboard.GetState().IsKeyUp(Keys.S) && vc.Velocity.Y > 0.0f)
			{
				vc.Velocity = new Vector2(vc.Velocity.X , vc.Velocity.Y - accelerationRate);
			}
			if (pressedKeys.Contains(Keys.S))
			{
				vc.Velocity = new Vector2(vc.Velocity.X, vc.Velocity.Y + accelerationRate);
			}

			if (Keyboard.GetState().IsKeyUp(Keys.A) && vc.Velocity.X < 0.0f)
			{
				vc.Velocity = new Vector2(vc.Velocity.X + accelerationRate, vc.Velocity.Y);
			}
			if (pressedKeys.Contains(Keys.A))
			{
				vc.Velocity = new Vector2(vc.Velocity.X -  accelerationRate, vc.Velocity.Y);
			}

			if (Keyboard.GetState().IsKeyUp(Keys.D) && vc.Velocity.X > 0.0f)
			{
				vc.Velocity = new Vector2(vc.Velocity.X - accelerationRate, vc.Velocity.Y);
			}
			if (pressedKeys.Contains(Keys.D))
			{
				vc.Velocity = new Vector2(vc.Velocity.X + accelerationRate, vc.Velocity.Y);
			}

			//if (pressedKeys.Contains(Keys.J))
			//{
			//    _player.Sprite.Rotatation += 0.1f * rotationMultiplier;
			//}
			//if (pressedKeys.Contains(Keys.L))
			//{
			//    _player.Sprite.Rotatation -= 0.1f * rotationMultiplier;
			//}

			pc.Position = new Vector2(
				pc.Position.X + (vc.Velocity.X * gameTime.ElapsedGameTime.Milliseconds),
				pc.Position.Y + (vc.Velocity.Y * gameTime.ElapsedGameTime.Milliseconds));
		}

		#endregion
	}
}
