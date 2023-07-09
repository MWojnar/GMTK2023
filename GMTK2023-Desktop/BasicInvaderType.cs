using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMTK2023_Desktop
{
	internal class BasicInvaderType : InvaderType
	{
		public BasicInvaderType(GMTK2023Game game) : base(game.AssetManager.GetSprite("SpriteBasicInvader"), 5, typeof(BasicInvader))
		{
		}
	}
}
