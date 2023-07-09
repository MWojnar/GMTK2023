using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMTK2023_Desktop
{
	internal class FastInvaderType : InvaderType
	{
		public FastInvaderType(GMTK2023Game game) : base(game.AssetManager.GetSprite("SpriteFastInvader"), 15, typeof(FastInvader))
		{
		}
	}
}
