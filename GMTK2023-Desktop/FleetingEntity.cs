using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMTK2023_Desktop
{
	internal class FleetingEntity : Entity
	{
		private Action endFunc;

		public FleetingEntity(GMTK2023Game game, Vector2 position, Sprite sprite, GameTime gameTime, Action endFunc = null, float depth = 0) : base(game, position, sprite, gameTime, depth)
		{
			this.endFunc = endFunc;
		}

		public override void Update(GameTime gameTime)
		{
			if (animation.IsOver(gameTime))
			{
				if (endFunc != null)
					endFunc();
				game.RemoveEntity(this);
			}
			base.Update(gameTime);
		}
	}
}
