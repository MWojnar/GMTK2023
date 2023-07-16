using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GMTK2023_Desktop
{
    public class Entity
    {
		private Vector2 position;
        protected Animation animation;
        protected Rectangle? sourceRect;
        public Rectangle? SourceRect { get { return sourceRect; } }
        protected GMTK2023Game game;
		private float depth;
        protected Sprite baseSprite;
        public Sprite BaseSprite { get { return baseSprite; } }
        protected bool isFlipped;

		public float Depth
		{
			get => depth;
			set
			{
                depth = value;
                game.UpdateEntityDepth(this);
			}
		}

		public Entity(GMTK2023Game game, Vector2 position, Sprite sprite, GameTime gameTime, float depth = 0)
        {
            this.game = game;
            this.position = position;
            Depth = depth;
            baseSprite = sprite;
            isFlipped = false;
            if (sprite != null)
                SetAnimation(new Animation(sprite, gameTime));
        }

        public void SetAnimation(Animation animation)
        {
            this.animation = animation;
            this.sourceRect = animation.Sprite.GetFrameRect(0);
        }

        public virtual void Draw(ExtendedSpriteBatch spriteBatch, GameTime gameTime)
        {
            if (animation != null)
                animation.Draw(gameTime, spriteBatch, position, isFlipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
        }

        public virtual void Update(GameTime gameTime)
        {
            //TODO
        }

        public virtual bool IsMouseOver()
        {
            return IsPointColliding(game.MousePos);
        }

        public virtual bool IsMouseOver(Rectangle rect)
        {
            return IsPointColliding(game.MousePos, rect);
        }

        public bool IsPointColliding(Vector2 point)
        {
            return (sourceRect?.Contains(point.X - position.X, point.Y - position.Y)??false);
        }

        public bool IsPointColliding(Vector2 point, Rectangle rect)
        {
			return (rect.Contains(point.X - position.X, point.Y - position.Y));
		}

        public Vector2 GetPos()
        {
            return position;
        }

        public void SetAnimationSpeed(float animationSpeed)
        {
            animation.AnimationRate = animationSpeed;
        }

        public void SetPos(float x, float y)
        {
            position.X = x;
            position.Y = y;
        }

		public bool IsCollidingWithEntity(Entity entity)
		{
            return !sourceRect.HasValue || !entity.sourceRect.HasValue ? false : sourceRect.Value.OffsetBy(GetPos()).Intersects(entity.sourceRect.Value.OffsetBy(entity.GetPos()));
		}

		public bool IsCollidingWithRect(Rectangle rect)
		{
			return !sourceRect.HasValue ? false : sourceRect.Value.OffsetBy(GetPos()).Intersects(rect);
		}

        public void Remove()
        {
            game.RemoveEntity(this);
        }
    }
}
