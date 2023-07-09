using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMTK2023_Desktop
{
	public class Enemy : Entity
	{
		private Sprite leftSprite;
		private Strategy strategy;
		private double stratTimeout;
		private double standardStratLength;
		private double speed;
		private double shootSpeed;
		private double nextShot;
		private Barrier hideBarrier;
		private int? movePointX;
		private bool dead;

		private enum Strategy
		{
			Hide,
			Fight
		}

		public Enemy(GMTK2023Game game, Vector2 position, GameTime gameTime, float depth = 0) : base(game, position, game.AssetManager.GetSprite("SpriteEnemy"), gameTime, depth)
		{
			leftSprite = game.AssetManager.GetSprite("SpriteEnemyL");
			strategy = Strategy.Hide;
			speed = 2;
			shootSpeed = 1;
			nextShot = gameTime.TotalGameTime.TotalSeconds + shootSpeed;
			standardStratLength = 3;
			stratTimeout = gameTime.TotalGameTime.TotalSeconds + standardStratLength;
			movePointX = null;
			dead = false;
			respawnBarriers();
		}

		private void respawnBarriers()
		{
			foreach (Barrier barrier in game.Entities.Where(e => e is Barrier))
				barrier.Reset();
		}

		public override void Update(GameTime gameTime)
		{
			double movementX = 0;
			Vector2 pos = GetPos();
			if (strategy == Strategy.Hide)
			{
				if (hideBarrier == null)
					hideBarrier = (Barrier)game.Entities.Where(e => e is Barrier && !((Barrier)e).IsGone()).RandomElement();
				if (hideBarrier == null)
					setStrategy(Strategy.Fight, gameTime);
				else
				{
					float targetX = hideBarrier.GetPos().X + (hideBarrier.SourceRect?.Width ?? 0) / 2.0f - baseSprite.FrameWidth / 2.0f;
					if (targetX < pos.X)
						movementX = (pos.X - targetX < speed) ? -(pos.X - targetX) : -speed;
					else if (targetX > pos.X)
						movementX = (targetX - pos.X < speed) ? (targetX - pos.X) : speed;
				}
			} else
			{
				if (movePointX == null)
					movePointX = (int)(rand.NextDouble() * 256.0 - (sourceRect?.Width ?? 0));
				float targetX = movePointX ?? 0;
				if (targetX < pos.X)
					movementX = (pos.X - targetX < speed) ? -(pos.X - targetX) : -speed;
				else if (targetX > pos.X)
					movementX = (targetX - pos.X < speed) ? (targetX - pos.X) : speed;
				else
					movePointX = (int)(rand.NextDouble() * 256.0 - (sourceRect?.Width ?? 0));
				if (nextShot < gameTime.TotalGameTime.TotalSeconds && canShoot() && rand.NextDouble() > .75)
					shoot(gameTime);
			}
			if (gameTime.TotalGameTime.TotalSeconds > stratTimeout)
				setRandomStrategy(gameTime);
			if (movementX > 0)
			{
				if (animation.Sprite != leftSprite || !isFlipped)
				{
					SetAnimation(new Animation(leftSprite, gameTime));
					isFlipped = true;
				}
			}
			else if (movementX < 0)
			{
				if (animation.Sprite != leftSprite || isFlipped)
				{
					SetAnimation(new Animation(leftSprite, gameTime));
					isFlipped = false;
				}
			}
			else if (animation.Sprite != baseSprite || isFlipped)
			{
				SetAnimation(new Animation(baseSprite, gameTime));
				isFlipped = false;
			}
			SetPos(pos.X + (float)movementX, pos.Y);
			base.Update(gameTime);
		}

		private void shoot(GameTime gameTime)
		{
			game.CreateEntity(new EnemyShot(game, new Vector2(GetPos().X + (baseSprite.FrameWidth / 2) - 8, GetPos().Y), gameTime));
			game.AssetManager.GetSound("SoundEnemyShoot").Play();
			nextShot = gameTime.TotalGameTime.TotalSeconds + shootSpeed;
		}

		private bool canShoot()
		{
			Rectangle beam = new Rectangle((int)GetPos().X + 4, 0, 8, 368);
			return !game.Entities.Where(e => e is Barrier && e.IsCollidingWithRect(beam)).Any(e => ((Barrier)e).IsHit(beam));
		}

		private Random rand = new Random();

		private void setRandomStrategy(GameTime gameTime)
		{
			setStrategy(rand.NextDouble() > .5 ? Strategy.Fight : Strategy.Hide, gameTime);
		}

		private void setStrategy(Strategy strategy, GameTime gameTime, double timeout = 5)
		{
			this.strategy = strategy;
			stratTimeout = gameTime.TotalGameTime.TotalSeconds + standardStratLength;
			hideBarrier = null;
			movePointX = null;
		}

		public void Hit(GameTime gameTime)
		{
			die(gameTime);
		}

		private void die(GameTime gameTime)
		{
			if (!dead)
			{
				game.RemoveEntity(this);
				game.CreateEntity(new FleetingEntity(game, new Vector2(GetPos().X - 16, GetPos().Y - 16), game.AssetManager.GetSprite("SpriteEnemyDeath"), gameTime, () => game.CreateEntity(new FleetingEntity(game, new Vector2(128 - 8 - 16, 336 - 16), game.AssetManager.GetSprite("SpriteEnemySpawn"), gameTime, () => game.CreateEntity(new Enemy(game, new Vector2(128 - 8, 336), game.GameTime))))));
				dead = true;
				game.AssetManager.GetSound("SoundEnemyDeath").Play();
			}
		}
	}
}
