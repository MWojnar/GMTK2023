using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMTK2023_Desktop
{
	public class BarrierInvader : Invader
	{
		public BarrierInvader(GMTK2023Game game, Vector2 position, GameTime gameTime, Fleet fleet) : base(game, position, game.AssetManager.GetSprite("SpriteBarrierInvader3"), game.AssetManager.GetSprite("SpriteBarrierInvader3"), game.AssetManager.GetSprite("SpriteBarrierInvaderDeath"), gameTime, fleet, (float)game.Settings.BarrierInvaderSpeed, 3, 2, 2, game.Settings.BarrierInvaderShotInterval)
		{
		}

		public override bool Hit(GameTime gameTime, int amount = 1)
		{
			bool returnBool = base.Hit(gameTime, amount);
			if (returnBool && !isDead())
			{
				switch (health)
				{
					case 1: changeMainSprite(game.AssetManager.GetSprite("SpriteBarrierInvader1"), gameTime); break;
					case 2: changeMainSprite(game.AssetManager.GetSprite("SpriteBarrierInvader2"), gameTime); break;
					case 3: changeMainSprite(game.AssetManager.GetSprite("SpriteBarrierInvader3"), gameTime); break;
				}
			}
			return returnBool;
		}

		protected override void die(GameTime gameTime)
		{
			game.CreateEntity(new BarrierDeathSaucer(game, new Vector2(GetPos().X, GetPos().Y), gameTime));
			base.die(gameTime);
		}

		private void changeMainSprite(Sprite sprite, GameTime gameTime)
		{
			baseSprite = sprite;
			leftMoveSprite = sprite;
			SetAnimation(new Animation(sprite, gameTime));
		}
	}
}
