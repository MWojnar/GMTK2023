using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMTK2023_Desktop
{
    public class Fleet : Entity
    {
        private List<Invader> invaders;
		private SpriteFont font;

        public Fleet(GMTK2023Game game, Vector2 position, GameTime gameTime, List<KeyValuePair<Vector2, InvaderType>> fleetData = null) : base(game, position, null, gameTime)
        {
			invaders = new List<Invader>();
			/*{
                new FastInvader(game, new Vector2(position.X + 16 * 1, position.Y + 16 * 1), gameTime, this),
				new FastInvader(game, new Vector2(position.X + 16 * 3, position.Y + 16 * 1), gameTime, this),
				new FastInvader(game, new Vector2(position.X + 16 * 5, position.Y + 16 * 1), gameTime, this),
				new FastInvader(game, new Vector2(position.X + 16 * 7, position.Y + 16 * 1), gameTime, this),
				new FastInvader(game, new Vector2(position.X + 16 * 9, position.Y + 16 * 1), gameTime, this),
				new FastInvader(game, new Vector2(position.X + 16 * 11, position.Y + 16 * 1), gameTime, this),
				new FastInvader(game, new Vector2(position.X + 16 * 2, position.Y + 16 * 2), gameTime, this),
				new FastInvader(game, new Vector2(position.X + 16 * 4, position.Y + 16 * 2), gameTime, this),
				new FastInvader(game, new Vector2(position.X + 16 * 6, position.Y + 16 * 2), gameTime, this),
				new FastInvader(game, new Vector2(position.X + 16 * 8, position.Y + 16 * 2), gameTime, this),
				new FastInvader(game, new Vector2(position.X + 16 * 10, position.Y + 16 * 2), gameTime, this),
				new FastInvader(game, new Vector2(position.X + 16 * 12, position.Y + 16 * 2), gameTime, this),
				new BasicInvader(game, new Vector2(position.X + 16 * 1, position.Y + 16 * 4), gameTime, this),
				new BasicInvader(game, new Vector2(position.X + 16 * 3, position.Y + 16 * 4), gameTime, this),
				new BasicInvader(game, new Vector2(position.X + 16 * 5, position.Y + 16 * 4), gameTime, this),
				new BasicInvader(game, new Vector2(position.X + 16 * 7, position.Y + 16 * 4), gameTime, this),
				new BasicInvader(game, new Vector2(position.X + 16 * 9, position.Y + 16 * 4), gameTime, this),
				new BasicInvader(game, new Vector2(position.X + 16 * 11, position.Y + 16 * 4), gameTime, this),
				new BasicInvader(game, new Vector2(position.X + 16 * 2, position.Y + 16 * 5), gameTime, this),
				new BasicInvader(game, new Vector2(position.X + 16 * 4, position.Y + 16 * 5), gameTime, this),
				new BasicInvader(game, new Vector2(position.X + 16 * 6, position.Y + 16 * 5), gameTime, this),
				new BasicInvader(game, new Vector2(position.X + 16 * 8, position.Y + 16 * 5), gameTime, this),
				new BasicInvader(game, new Vector2(position.X + 16 * 10, position.Y + 16 * 5), gameTime, this),
				new BasicInvader(game, new Vector2(position.X + 16 * 12, position.Y + 16 * 5), gameTime, this),
				new BarrierInvader(game, new Vector2(position.X + 16 * 1, position.Y + 16 * 7), gameTime, this),
				new BarrierInvader(game, new Vector2(position.X + 16 * 5, position.Y + 16 * 7), gameTime, this),
				new BarrierInvader(game, new Vector2(position.X + 16 * 9, position.Y + 16 * 7), gameTime, this),
				new BarrierInvader(game, new Vector2(position.X + 16 * 3, position.Y + 16 * 9), gameTime, this),
				new BarrierInvader(game, new Vector2(position.X + 16 * 7, position.Y + 16 * 9), gameTime, this),
				new BarrierInvader(game, new Vector2(position.X + 16 * 11, position.Y + 16 * 9), gameTime, this)
			};*/
			if (fleetData == null)
				fleetData = game.SavedFleet;
			if (fleetData != null)
				foreach (var invaderData in fleetData)
					invaders.Add((Invader)Activator.CreateInstance(invaderData.Value.EntityClass, new object[] { game, invaderData.Key, gameTime, this }));
            foreach (Invader invader in invaders)
                game.CreateEntity(invader);
			font = game.AssetManager.GetFont("FontDogicaPixel");
        }

		public override void Update(GameTime gameTime)
		{
			bool rightDown = Keyboard.GetState().IsKeyDown(Keys.Right);
			bool leftDown = Keyboard.GetState().IsKeyDown(Keys.Left);
			if (rightDown)
				foreach (Invader invader in invaders)
					invader.MoveRight(gameTime);
			if (leftDown)
				foreach (Invader invader in invaders)
					invader.MoveLeft(gameTime);
			if (!rightDown && !leftDown)
				foreach (Invader invader in invaders)
					invader.NotMoving(gameTime);
			base.Update(gameTime);
		}

		Vector2 scoreTextPos = new Vector2();

		public override void Draw(ExtendedSpriteBatch spriteBatch, GameTime gameTime)
		{
			spriteBatch.DrawString(font, $"Score: {game.Points}", scoreTextPos, Color.White);
		}

		public void Remove(Invader invader)
		{
			invaders.Remove(invader);
			if (invaders.Count <= 0)
				gameOver();
		}

		private void gameOver()
		{
			game.GameOver();
		}
	}
}
