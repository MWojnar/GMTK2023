using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMTK2023_Desktop
{
    public class Sprite
    {
        private Texture2D texture;
        private int frameWidth;
        private int frameHeight;
        private float animationRate;

        public Sprite(Texture2D texture)
        {
            this.texture = texture;
            this.frameWidth = texture.Width;
            this.frameHeight = texture.Height;
            this.animationRate = 60;
        }

        public Sprite(Texture2D texture, int frameWidth, int frameHeight, float animationRate)
        {
            this.texture = texture;
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
            this.animationRate = animationRate;
        }

        public int FrameWidth { get { return frameWidth; } }
        public int FrameHeight { get { return frameHeight; } }
        public float AnimationRate { get { return animationRate; } }

        public Rectangle GetFrameRect(int frame)
        {
            int frameX = frame * frameWidth;
            return new Rectangle(frameX, 0, frameWidth, frameHeight);
        }

        public int GetCurrentFrame(GameTime gameTime)
        {
            float animationStartTime = (float)gameTime.TotalGameTime.TotalSeconds;
            float animationInterval = 1 / animationRate;
            float timeSinceStart = (float)gameTime.TotalGameTime.TotalSeconds - animationStartTime;
            return (int)(timeSinceStart / animationInterval) % (texture.Width / frameWidth);
        }

        internal void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position)
        {
            int currentFrame = GetCurrentFrame(gameTime);
            var sourceRect = GetFrameRect(currentFrame);
            spriteBatch.Draw(Texture, position, sourceRect, Color.White);
        }

        public Texture2D Texture { get { return texture; } }
    }
}
