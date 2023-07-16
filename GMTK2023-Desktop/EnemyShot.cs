using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMTK2023_Desktop
{
	internal class EnemyShot : Entity
	{
		private float speed = 4;

		public EnemyShot(GMTK2023Game game, Vector2 position, GameTime gameTime, float depth = -1) : base(game, position, game.AssetManager.GetSprite("SpriteEnemyShot"), gameTime, depth)
		{
		}

		public override void Update(GameTime gameTime)
		{
			SetPos(GetPos().X, GetPos().Y - speed);
			if (GetPos().Y < -baseSprite.FrameHeight)
				game.RemoveEntity(this);
			foreach (Entity entity in game.Entities.Where(e => e is Invader))
				if (IsCollidingWithEntity(entity)) {
					((Invader)entity).Hit(gameTime);
					game.RemoveEntity(this);
					break;
				}
			base.Update(gameTime);
		}
	}
}
