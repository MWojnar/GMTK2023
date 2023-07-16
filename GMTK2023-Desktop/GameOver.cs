using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace GMTK2023_Desktop
{
	internal class GameOver : Entity
	{
		private SpriteFont font;
		private double startTime, width, scoreWidth;
		private bool keyDown;

		public GameOver(GMTK2023Game game, Vector2 position, GameTime gameTime, float depth = 0) : base(game, position, null, gameTime, depth)
		{
			font = game.AssetManager.GetFont("FontDogicaPixelBold");
			startTime = gameTime.TotalGameTime.TotalSeconds;
			width = font.MeasureString("Press Any Key").X;
			scoreWidth = font.MeasureString($"Final Score: {game.Points}").X;
			keyDown = false;
		}

		public override void Draw(ExtendedSpriteBatch spriteBatch, GameTime gameTime)
		{
			spriteBatch.DrawString(font, $"Final Score: {game.Points}", new Vector2((float)(128 - (scoreWidth / 2)), 250), Color.White);
			if ((gameTime.TotalGameTime.TotalSeconds - startTime) % 2 < 1)
				spriteBatch.DrawString(font, "Press Any Key", new Vector2((float)(128 - (width / 2)), 300), Color.White);
		}

		public override void Update(GameTime gameTime)
		{
			if (!(anyKeyDown() || Mouse.GetState().LeftButton == ButtonState.Pressed) && keyDown)
				game.StartRoom(0, gameTime);
			if ((anyKeyDown() || Mouse.GetState().LeftButton == ButtonState.Pressed))
				keyDown = true;
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