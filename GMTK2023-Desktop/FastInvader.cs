using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMTK2023_Desktop
{
    public class FastInvader : Invader
    {
        public FastInvader(GMTK2023Game game, Vector2 position, GameTime gameTime, Fleet fleet) : base(game, position, game.AssetManager.GetSprite("SpriteFastInvader"), game.AssetManager.GetSprite("SpriteFastInvaderL"), game.AssetManager.GetSprite("SpriteFastInvaderDeath"), gameTime, fleet, 6, 1, 1, 1, 1.5)
        {
        }
    }
}
