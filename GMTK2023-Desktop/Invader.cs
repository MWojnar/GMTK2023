using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMTK2023_Desktop
{
    public class Invader : Entity
    {
        protected float movementSpeed;
        protected int health;
        protected int width, height;
        protected Fleet fleet;
        protected Sprite leftMoveSprite;
        protected Sprite rightMoveSprite;
        protected Sprite deathSprite;
        protected double lastShot;
        protected double shootInterval;

        public Invader(GMTK2023Game game, Vector2 position, Sprite sprite, Sprite leftMoveSprite, Sprite deathSprite, GameTime gameTime, Fleet fleet, float movementSpeed = 4, int health = 1, int width = 1, int height = 1, double shootInterval = 3.0f, Sprite rightMoveSprite = null) : base(game, position, sprite, gameTime)
        {
            this.movementSpeed = movementSpeed;
			this.health = health;
			this.width = width;
			this.height = height;
            this.fleet = fleet;
            this.leftMoveSprite = leftMoveSprite;
            this.rightMoveSprite = rightMoveSprite;
            this.deathSprite = deathSprite;
            lastShot = gameTime.TotalGameTime.TotalSeconds;
            this.shootInterval = shootInterval;
        }

        public override void Update(GameTime gameTime)
        {
            if (!isDead())
            {
                double totalSeconds = gameTime.TotalGameTime.TotalSeconds;
                if (shootInterval != 0)
                {
                    double offset = ((((int)GetPos().Y - 32) / 16) % 3);
                    double nextShot = lastShot + (shootInterval - ((lastShot - offset) % shootInterval));
                    if (totalSeconds >= nextShot)
                        Shoot(gameTime);
                }
                lastShot = totalSeconds;
            }
            else if (animation.IsOver(gameTime))
                removeSelf();
            base.Update(gameTime);
        }

        private void removeSelf()
        {
			game.RemoveEntity(this);
            fleet.Remove(this);
		}

        public virtual void Shoot(GameTime gameTime)
        {
            if (!isDead())
            {
                game.CreateEntity(new InvaderShot(game, new Vector2(GetPos().X + (baseSprite.FrameWidth / 2) - 8, GetPos().Y + baseSprite.FrameHeight), gameTime));
				game.AssetManager.GetSound("SoundInvaderShoot").Play();
			}
        }

        public virtual void MoveRight(GameTime gameTime)
        {
            if (!isDead())
            {
                SetPos(GetPos().X + movementSpeed, GetPos().Y);
                wrapAround();
                if (rightMoveSprite == null) {
                    if (animation.Sprite != leftMoveSprite || !isFlipped)
                    {
                        SetAnimation(new Animation(leftMoveSprite, gameTime));
                        isFlipped = true;
                    }
                } else if (animation.Sprite != rightMoveSprite)
                {
                    SetAnimation(new Animation(rightMoveSprite, gameTime));
                    isFlipped = false;
                }
            }
		}

		public virtual void MoveLeft(GameTime gameTime)
		{
            if (!isDead())
            {
                SetPos(GetPos().X - movementSpeed, GetPos().Y);
                wrapAround();
                if (animation.Sprite != leftMoveSprite || isFlipped)
                {
                    SetAnimation(new Animation(leftMoveSprite, gameTime));
                    isFlipped = false;
                }
            }
		}

        protected virtual bool isDead()
        {
            return animation.Sprite == deathSprite;
		}

        public override void Draw(ExtendedSpriteBatch spriteBatch, GameTime gameTime)
        {
			animation.Draw(gameTime, spriteBatch, GetPos(), isFlipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
			animation.Draw(gameTime, spriteBatch, new Vector2(GetPos().X - 256, GetPos().Y), isFlipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
		}

        public virtual bool Hit(GameTime gameTime, int amount = 1)
        {
            if (!isDead())
            {
                health -= amount;
                if (health <= 0)
                    die(gameTime);
                return true;
            }
            return false;
        }

        protected virtual void die(GameTime gameTime)
        {
            SetAnimation(new Animation(deathSprite, gameTime));
            SetPos(GetPos().X - baseSprite.FrameWidth / 2, GetPos().Y - baseSprite.FrameHeight / 2);
			game.AssetManager.GetSound("SoundInvaderDeath").Play();
		}

        private void wrapAround()
        {
            if (GetPos().X < 0)
                SetPos(GetPos().X + 256, GetPos().Y);
            if (GetPos().X >= 256)
                SetPos(GetPos().X - 256, GetPos().Y);
        }

        public void NotMoving(GameTime gameTime)
        {
            if (!isDead())
            {
                if (animation.Sprite != baseSprite)
                {
                    SetAnimation(new Animation(baseSprite, gameTime));
                    isFlipped = false;
                }
            }
		}
    }
}
