using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace GMTK2023_Desktop
{
	internal class TitleScreen : Entity
	{
		private SpriteFont font;
		private double startTime, width;

		public TitleScreen(GMTK2023Game game, Vector2 position, GameTime gameTime, float depth = 0) : base(game, position, null, gameTime, depth)
		{
			font = game.AssetManager.GetFont("FontDogicaPixelBold");
			startTime = gameTime.TotalGameTime.TotalSeconds;
			width = font.MeasureString("Press Any Key").X;
		}

		public override void Draw(ExtendedSpriteBatch spriteBatch, GameTime gameTime)
		{
			if ((gameTime.TotalGameTime.TotalSeconds - startTime) % 2 < 1)
				spriteBatch.DrawString(font, "Press Any Key", new Vector2((float)(128 - (width / 2)), 200), Color.White);
		}

		public override void Update(GameTime gameTime)
		{
			if (anyKeyDown() || Mouse.GetState().LeftButton == ButtonState.Pressed)
				game.StartRoom(1, gameTime);
		}

		private bool anyKeyDown()
		{
			var keyboardState = Keyboard.GetState();
			foreach (Keys key in Enum.GetValues(typeof(Keys)))
			{
				if (keyboardState.IsKeyDown(key))
					return true;
			}
			return false;
		}
	}
}