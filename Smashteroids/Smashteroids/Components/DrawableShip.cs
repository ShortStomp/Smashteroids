using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Smashteroids.Components
{
	class DrawableShip : DrawableGameComponent
	{

		#region Private Properties

		private Texture2D PlayerShip { get; set; }

		private SpriteBatch SpriteBatch { get; set; }

		Rectangle RectModel;

		#endregion

		public DrawableShip(Game game)
			: base(game)
		{
			SpriteBatch = (SpriteBatch)game.Services.GetService(typeof(GraphicsResource));

			if (SpriteBatch == null) {
				// TODO: log error
				// TODO: Refactor this into a ServiceNotFoundException() class.
				throw new ArgumentException("SpriteBatch Service was not loaded.");
			}

			RectModel = new Rectangle(0,0,64,64);
		}

		public override void Initialize()
		{
			base.Initialize();
		}

		protected override void LoadContent()
		{

			PlayerShip = Game.Content.Load<Texture2D>("images/BlueShip");

			base.LoadContent();
		}

		public override void Draw(GameTime gameTime)
		{
			SpriteBatch.Draw(PlayerShip, RectModel, Color.DarkRed);

			base.Draw(gameTime);
		}

		public override void Update(GameTime gameTime)
		{
			if (Keyboard.GetState().IsKeyDown(Keys.A)) {
				RectModel.Offset(3, 3);
			}
			base.Update(gameTime);
		}
	}
}
