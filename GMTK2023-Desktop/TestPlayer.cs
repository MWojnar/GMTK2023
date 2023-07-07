using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMTK2023_Desktop
{
    public class TestPlayer : Entity
    {
        public TestPlayer(GMTK2023Game game, Vector2 position) : base(game, position, game._assetManager.GetSprite("test"))
        {
        }

        public TestPlayer(GMTK2023Game game, Vector2 position, Sprite sprite) : base(game, position, sprite)
        {
        }
    }
}
