using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMTK2023_Desktop
{
	internal class KamikazeInvaderType : InvaderType
	{
		public KamikazeInvaderType(GMTK2023Game game) : base(game.AssetManager.GetSprite("SpriteKamikazeInvader"), game.Settings.BombInvaderPrice, typeof(KamikazeInvader))
		{
		}
	}
}
