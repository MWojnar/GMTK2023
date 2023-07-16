using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMTK2023_Desktop
{
    public class BasicInvader : Invader
    {
        public BasicInvader(GMTK2023Game game, Vector2 position, GameTime gameTime, Fleet fleet) : base(game, position, game.AssetManager.GetSprite("SpriteBasicInvader"), game.AssetManager.GetSprite("SpriteBasicInvaderL"), game.AssetManager.GetSprite("SpriteBasicInvaderDeath"), gameTime, fleet, (float)game.Settings.BasicInvaderSpeed, 1, 1, 1, game.Settings.BasicInvaderShotInterval)
        {
        }
    }
}
