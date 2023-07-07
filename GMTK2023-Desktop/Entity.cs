using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMTK2023_Desktop
{
    public class Entity
    {
        private Vector2 position; // position of the entity
        private Sprite sprite; // animation data for the entity
        private Rectangle sourceRect; // section of the sprite sheet corresponding to the current frame
        private GMTK2023Game game;

        public Entity(GMTK2023Game game, Vector2 position, Sprite sprite)
        {
            this.game = game;
            this.position = position;
            this.sprite = sprite;
            this.sourceRect = sprite.GetFrameRect(0);
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            sprite.Draw(gameTime, spriteBatch, position);
        }

        public virtual void Update(GameTime gameTime)
        {
            //TODO
        }
    }
}
