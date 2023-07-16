using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace GMTK2023_Desktop
{
	public class Bomb : Entity
	{
		private float speed;
		private float amplitude;
		private float period;
		private float startY, startX;
		private Sprite deathSprite;

		public Bomb(GMTK2023Game game, Vector2 position, GameTime gameTime, float depth = 0) : base(game, position, game.AssetManager.GetSprite("SpriteBomb"), gameTime, depth)
		{
			speed = (float)game.Settings.BombSpeed;
			startY = position.Y;
			startX = position.X;
			amplitude = (float)game.Settings.BombAmplitude;
			period = (float)game.Settings.BombPeriod;
			deathSprite = game.AssetManager.GetSprite("SpriteBombExplode");
		}

		public override void Update(GameTime gameTime)
		{
			if (!isDead())
			{
				float yPos = GetPos().Y + speed;
				float variance = yPos - startY;
				float xPos = startX + (float)Math.Sin((variance / period) * 2 * Math.PI) * amplitude;
				SetPos(xPos, yPos);
				if (GetPos().Y > 368)
					game.RemoveEntity(this);
				foreach (Entity entity in game.Entities.Where(e => e is Barrier))
					if (IsCollidingWithEntity(entity))
						if (((Barrier)entity).Hit(this))
						{
							SetAnimation(new Animation(deathSprite, gameTime));
							SetPos(GetPos().X - 8, GetPos().Y - 8);
							destroyAllTouching(gameTime);
							return;
						}
				foreach (Entity entity in game.Entities.Where(e => e is Enemy))
					if (IsCollidingWithEntity(entity))
					{
						SetAnimation(new Animation(deathSprite, gameTime));
						SetPos(GetPos().X - 8, GetPos().Y - 8);
						destroyAllTouching(gameTime);
						((Enemy)entity).Hit(gameTime);
						return;
					}
			} else
			{
				if (animation.IsOver(gameTime))
				{
					game.RemoveEntity(this);
				}
			}
			base.Update(gameTime);
		}

		public override void Draw(ExtendedSpriteBatch spriteBatch, GameTime gameTime)
		{
			animation.Draw(gameTime, spriteBatch, GetPos(), isFlipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
			animation.Draw(gameTime, spriteBatch, new Vector2(GetPos().X - 256, GetPos().Y), isFlipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
		}

		protected virtual bool isDead()
		{
			return animation.Sprite == deathSprite;
		}

		public void destroyAllTouching(GameTime gameTime)
		{
			foreach (Entity entity in game.Entities.Where(e => e is Barrier))
				if (IsCollidingWithEntity(entity))
					((Barrier)entity).DestroyWithinRadius(new Vector2(GetPos().X + animation.Sprite.FrameWidth / 2, GetPos().Y + animation.Sprite.FrameHeight / 2), animation.Sprite.FrameWidth / 2);
			foreach (Entity entity in game.Entities.Where(e => e is Enemy))
				if (IsCollidingWithEntity(entity))
					((Enemy)entity).Hit(gameTime);
		}
	}
}