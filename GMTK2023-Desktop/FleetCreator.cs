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
    public class FleetCreator : Entity
    {
        private List<KeyValuePair<Vector2, InvaderType>> fleet;
        private InvaderType selectedInvader;
        private Rectangle boundary;
        private bool lastPressed;
        private Animation gridSelectAnim, gridSelectLargeAnim;

        public FleetCreator(GMTK2023Game game, Vector2 position, GameTime gameTime) : base(game, position, null, gameTime)
        {
            selectedInvader = new BasicInvaderType(game);
            Button button = new Button(game, new Vector2(256 / 4 - 8, 230), game.AssetManager.GetSprite("SpriteBasicInvader"), gameTime, () => selectedInvader = new BasicInvaderType(game));
            button.SetAnimationSpeed(0);
            game.CreateEntity(button);
			button = new Button(game, new Vector2(256 * 2 / 4 - 16, 230), game.AssetManager.GetSprite("SpriteBarrierInvader3"), gameTime, () => selectedInvader = new BarrierInvaderType(game));
			button.SetAnimationSpeed(0);
			game.CreateEntity(button);
			button = new Button(game, new Vector2(256 * 3 / 4, 230), game.AssetManager.GetSprite("SpriteFastInvader"), gameTime, () => selectedInvader = new FastInvaderType(game));
			button.SetAnimationSpeed(0);
			game.CreateEntity(button);
			button = new Button(game, new Vector2(256 * 1 / 4, 260), game.AssetManager.GetSprite("SpriteBarrierInvader3"), gameTime, () => { game.SavedFleet = fleet; game.StartRoom(2, game.GameTime); });
			button.SetAnimationSpeed(0);
			game.CreateEntity(button);
			boundary = new Rectangle(16, 32, 16 * 14, 16 * 10);
            lastPressed = false;
            fleet = new List<KeyValuePair<Vector2, InvaderType>>();
            gridSelectAnim = new Animation(game.AssetManager.GetSprite("SpriteGridSelect"), gameTime);
			gridSelectLargeAnim = new Animation(game.AssetManager.GetSprite("SpriteGridSelectBig"), gameTime);
		}

        public override void Update(GameTime gameTime)
        {
            Vector2 gridPos = getMouseGridPos();
            bool occupiedPos = isPosOccupied(gridPos);
            if (lastPressed && Mouse.GetState().LeftButton == ButtonState.Released && boundary.Contains(game.MousePos) && !occupiedPos)
                fleet.Add(new KeyValuePair<Vector2, InvaderType>(gridPos, selectedInvader));
            lastPressed = Mouse.GetState().LeftButton == ButtonState.Pressed;
            base.Update(gameTime);
        }

        private bool isPosOccupied(Vector2 gridPos)
        {
            foreach (var invader in fleet)
            {
                var frameRect1 = invader.Value.Sprite.GetFrameRect(0);
                var rect1 = new Rectangle(frameRect1.X + (int)invader.Key.X, frameRect1.Y + (int)invader.Key.Y, frameRect1.Width, frameRect1.Height);
				var frameRect2 = selectedInvader.Sprite.GetFrameRect(0);
				var rect2 = new Rectangle(frameRect2.X + (int)gridPos.X, frameRect2.Y + (int)gridPos.Y, frameRect2.Width, frameRect2.Height);
                if (rect1.Intersects(rect2))
                    return true;
            }
            return false;
        }

        public override void Draw(ExtendedSpriteBatch spriteBatch, GameTime gameTime)
		{
            /*for (int x = 16; x < 16 + 15 * 16; x += 16)
				spriteBatch.DrawLine(new Vector2(x, 32), new Vector2(x, 32 + 10 * 16), Color.Maroon);
			for (int y = 32; y < 32 + 11 * 16; y += 16)
				spriteBatch.DrawLine(new Vector2(16, y), new Vector2(16 + 14 * 16, y), Color.Maroon);*/
            game.AssetManager.GetSprite("SpriteGrid").Draw(0, spriteBatch, new Vector2(GetPos().X - 16, GetPos().Y - 16));
			Vector2 gridPos = getMouseGridPos();
			bool occupiedPos = isPosOccupied(gridPos);
			foreach (var invader in fleet)
				invader.Value.Sprite.Draw(0, spriteBatch, invader.Key);
			selectedInvader.Sprite.Draw(0, spriteBatch, gridPos, occupiedPos ? Color.Red : Color.White);
            (selectedInvader.Sprite.FrameWidth > 16 ? gridSelectLargeAnim : gridSelectAnim).Draw(gameTime, spriteBatch, new Vector2(gridPos.X - 16, gridPos.Y - 16));
		}

        private Vector2 getMouseGridPos()
        {
            Vector2 pos = game.MousePos;
            if (pos.X < 16)
                pos.X = 16;
            if (pos.X > 16 + 14 * 16 - selectedInvader.Sprite.FrameWidth)
                pos.X = 16 + 14 * 16 - selectedInvader.Sprite.FrameWidth;
            if (pos.Y < 32)
                pos.Y = 32;
            if (pos.Y > 32 + 10 * 16 - selectedInvader.Sprite.FrameHeight)
                pos.Y = 32 + 10 * 16 - selectedInvader.Sprite.FrameHeight;
            pos.X = ((int)pos.X / 16) * 16;
            pos.Y = ((int)pos.Y / 16) * 16;
            return pos;
        }
    }
}
