using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMTK2023_Desktop
{
	internal class BarrierDeathSaucer : Entity
	{
		private float speed = 2;

		public BarrierDeathSaucer(GMTK2023Game game, Vector2 position, GameTime gameTime, float depth = 0) : base(game, position, game.AssetManager.GetSprite("SpriteBarrierInvaderDeathSaucer"), gameTime, depth)
		{
		}

		public override void Update(GameTime gameTime)
		{
			SetPos(GetPos().X, GetPos().Y - speed);
			if (GetPos().Y < -baseSprite.FrameHeight)
				game.RemoveEntity(this);
			base.Update(gameTime);
		}
	}
}
