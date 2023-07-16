using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMTK2023_Desktop
{
	internal class BarrierInvaderType : InvaderType
	{
		public BarrierInvaderType(GMTK2023Game game) : base(game.AssetManager.GetSprite("SpriteBarrierInvader3"), game.Settings.BarrierInvaderPrice, typeof(BarrierInvader))
		{
		}
	}
}
