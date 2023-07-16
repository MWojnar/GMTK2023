using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMTK2023_Desktop
{
	public class InvaderShot : Entity
	{
		private float speed;

		public InvaderShot(GMTK2023Game game, Vector2 position, GameTime gameTime, float depth = -1) : base(game, position, game.AssetManager.GetSprite("SpriteInvaderShot"), gameTime, depth)
		{
			speed = (float)game.Settings.InvaderShotSpeed;
		}

		public override void Update(GameTime gameTime)
		{
			SetPos(GetPos().X, GetPos().Y + speed);
			if (GetPos().Y > 368)
				game.RemoveEntity(this);
			foreach (Entity entity in game.Entities.Where(e => e is Barrier))
				if (IsCollidingWithEntity(entity))
					if (((Barrier)entity).Hit(this))
					{
						game.RemoveEntity(this);
						return;
					}
			foreach (Entity entity in game.Entities.Where(e => e is Enemy))
				if (IsCollidingWithEntity(entity))
				{
					game.RemoveEntity(this);
					((Enemy)entity).Hit(gameTime);
					return;
				}
			base.Update(gameTime);
		}
	}
}
