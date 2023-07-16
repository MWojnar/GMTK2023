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
        private bool lastPressed, lastRightPressed;
        private Animation gridSelectAnim, gridSelectLargeAnim;
        private Button[] buttons;
        private int selectedButton, money;
        private SpriteFont font;
        private Vector2 basicPos, barrierPos, bombPos, fastPos;
		private Vector2 basicPricePos, barrierPricePos, bombPricePos, fastPricePos;
        private int basicPrice, barrierPrice, bombPrice, fastPrice;

		public FleetCreator(GMTK2023Game game, Vector2 position, GameTime gameTime) : base(game, position, null, gameTime)
        {
            selectedInvader = new BasicInvaderType(game);
            buttons = new Button[4];
            selectedButton = 0;
            Button button = new Button(game, new Vector2(16 * 2, 16 * 14), game.AssetManager.GetSprite("SpriteBasicInvader"), gameTime, () => { selectedInvader = new BasicInvaderType(game); selectedButton = 0; });
            buttons[0] = button;
            button.SetAnimationSpeed(0);
            game.CreateEntity(button);
			button = new Button(game, new Vector2(16 * 5, 16 * 13), game.AssetManager.GetSprite("SpriteBarrierInvader3"), gameTime, () => { selectedInvader = new BarrierInvaderType(game); selectedButton = 1; });
			buttons[1] = button;
			button.SetAnimationSpeed(0);
			game.CreateEntity(button);
			button = new Button(game, new Vector2(16 * 13, 16 * 14), game.AssetManager.GetSprite("SpriteFastInvader"), gameTime, () => { selectedInvader = new FastInvaderType(game); selectedButton = 2; });
			buttons[2] = button;
			button.SetAnimationSpeed(0);
			game.CreateEntity(button);
			button = new Button(game, new Vector2(16 * 9, 16 * 13), game.AssetManager.GetSprite("SpriteKamikazeInvader"), gameTime, () => { selectedInvader = new KamikazeInvaderType(game); selectedButton = 3; });
			buttons[3] = button;
			button.SetAnimationSpeed(0);
			game.CreateEntity(button);
			var buttonFont = game.AssetManager.GetFont("FontDogicaPixelBold");
			button = new Button(game, new Vector2(128 - (buttonFont.MeasureString("Start!").X / 2), 16 * 20 + 4), game.AssetManager.GetSprite("SpriteBarrierInvader3"), gameTime, () => { game.SavedFleet = fleet; game.StartRoom(2, game.GameTime); }, buttonFont, "Start!");
			button.SetAnimationSpeed(0);
			game.CreateEntity(button);
			boundary = new Rectangle(16, 32, 16 * 14, 16 * 10);
            lastPressed = false;
            lastRightPressed = false;
            fleet = new List<KeyValuePair<Vector2, InvaderType>>();
            gridSelectAnim = new Animation(game.AssetManager.GetSprite("SpriteGridSelect"), gameTime);
			gridSelectLargeAnim = new Animation(game.AssetManager.GetSprite("SpriteGridSelectBig"), gameTime);
            font = game.AssetManager.GetFont("FontDogica");
            money = game.Settings.MoneyToSpend;
            basicPos = new Vector2((16 * 2 + 8) - (font.MeasureString("BASIC").X / 2), 16 * 15 + 4);
			barrierPos = new Vector2((16 * 5 + 16) - (font.MeasureString("BARRIER").X / 2), 16 * 15 + 4);
			bombPos = new Vector2((16 * 9 + 16) - (font.MeasureString("BOMB").X / 2), 16 * 15 + 4);
			fastPos = new Vector2((16 * 13 + 8) - (font.MeasureString("FAST").X / 2), 16 * 15 + 4);
            basicPrice = new BasicInvaderType(game).Cost;
            barrierPrice = new BarrierInvaderType(game).Cost;
            bombPrice = new KamikazeInvaderType(game).Cost;
            fastPrice = new FastInvaderType(game).Cost;
			basicPricePos = new Vector2((16 * 2 + 8) - (font.MeasureString($"${basicPrice}").X / 2), 16 * 16);
			barrierPricePos = new Vector2((16 * 5 + 16) - (font.MeasureString($"${barrierPrice}").X / 2), 16 * 16);
			bombPricePos = new Vector2((16 * 9 + 16) - (font.MeasureString($"${bombPrice}").X / 2), 16 * 16);
			fastPricePos = new Vector2((16 * 13 + 8) - (font.MeasureString($"${fastPrice}").X / 2), 16 * 16);
            moneyTextPos = new Vector2(128 - (font.MeasureString($"Money: ${money}").X / 2), 16 * 18 + 4);
		}

        public override void Update(GameTime gameTime)
        {
            Vector2 gridPos = getMouseGridPos();
            bool occupiedPos = isPosOccupied(gridPos) || selectedInvader.Cost > money;
            if (lastPressed && Mouse.GetState().LeftButton == ButtonState.Released && boundary.Contains(game.MousePos) && !occupiedPos)
            {
                fleet.Add(new KeyValuePair<Vector2, InvaderType>(gridPos, selectedInvader));
                money -= selectedInvader.Cost;
            }
            if (lastRightPressed && Mouse.GetState().RightButton == ButtonState.Released && boundary.Contains(game.MousePos) && occupiedPos)
                foreach (var invader in getOccupyingInvaders(gridPos))
                {
                    fleet.Remove(invader);
                    money += invader.Value.Cost;
                }
			lastPressed = Mouse.GetState().LeftButton == ButtonState.Pressed;
            lastRightPressed = Mouse.GetState().RightButton == ButtonState.Pressed;
            base.Update(gameTime);
        }

        private List<KeyValuePair<Vector2, InvaderType>> getOccupyingInvaders(Vector2 gridPos)
        {
            var returnList = new List<KeyValuePair<Vector2, InvaderType>>();
			foreach (var invader in fleet)
			{
				var frameRect1 = invader.Value.Sprite.GetFrameRect(0);
				var rect1 = new Rectangle(frameRect1.X + (int)invader.Key.X, frameRect1.Y + (int)invader.Key.Y, frameRect1.Width, frameRect1.Height);
				var frameRect2 = selectedInvader.Sprite.GetFrameRect(0);
				var rect2 = new Rectangle(frameRect2.X + (int)gridPos.X, frameRect2.Y + (int)gridPos.Y, frameRect2.Width, frameRect2.Height);
                if (rect1.Intersects(rect2))
                    returnList.Add(invader);
			}
            return returnList;
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

        private Vector2 moneyTextPos = new Vector2();

        public override void Draw(ExtendedSpriteBatch spriteBatch, GameTime gameTime)
		{
            /*for (int x = 16; x < 16 + 15 * 16; x += 16)
				spriteBatch.DrawLine(new Vector2(x, 32), new Vector2(x, 32 + 10 * 16), Color.Maroon);
			for (int y = 32; y < 32 + 11 * 16; y += 16)
				spriteBatch.DrawLine(new Vector2(16, y), new Vector2(16 + 14 * 16, y), Color.Maroon);*/
            game.AssetManager.GetSprite("SpriteGrid").Draw(0, spriteBatch, new Vector2(GetPos().X - 16, GetPos().Y - 16));
            Vector2? gridPos = null;
            if (validMouseGridPos())
            {
                gridPos = getMouseGridPos();
            }
			foreach (var invader in fleet)
				invader.Value.Sprite.Draw(0, spriteBatch, invader.Key);
			if (gridPos.HasValue)
            {
                bool occupiedPos = isPosOccupied(gridPos.Value) || selectedInvader.Cost > money;
                selectedInvader.Sprite.Draw(0, spriteBatch, gridPos.Value, occupiedPos ? Color.Red : Color.White);
                (selectedInvader.Sprite.FrameWidth > 16 ? gridSelectLargeAnim : gridSelectAnim).Draw(gameTime, spriteBatch, new Vector2(gridPos.Value.X - 16, gridPos.Value.Y - 16));
            }
            Button button = buttons[selectedButton];
            (button.BaseSprite.FrameWidth > 16 ? gridSelectLargeAnim : gridSelectAnim).Sprite.Draw(0, spriteBatch, new Vector2(button.GetPos().X - 16, button.GetPos().Y - 16));
            spriteBatch.DrawString(font, $"Money: ${money}", moneyTextPos, Color.White);
            spriteBatch.DrawString(font, "BASIC", basicPos, Color.White);
			spriteBatch.DrawString(font, "BARRIER", barrierPos, Color.White);
			spriteBatch.DrawString(font, "BOMB", bombPos, Color.White);
			spriteBatch.DrawString(font, "FAST", fastPos, Color.White);
			spriteBatch.DrawString(font, $"${basicPrice}", basicPricePos, Color.White);
			spriteBatch.DrawString(font, $"${barrierPrice}", barrierPricePos, Color.White);
			spriteBatch.DrawString(font, $"${bombPrice}", bombPricePos, Color.White);
			spriteBatch.DrawString(font, $"${fastPrice}", fastPricePos, Color.White);
		}

        private bool validMouseGridPos()
		{
			Vector2 pos = game.MousePos;
            return !((pos.X < 16) || (pos.X > 16 + 15 * 16 - selectedInvader.Sprite.FrameWidth - 1) || (pos.Y < 32) || (pos.Y > 32 + 11 * 16 - selectedInvader.Sprite.FrameHeight - 1));
		}

        private Vector2 getMouseGridPos()
        {
            Vector2 pos = game.MousePos;
            pos.X = ((int)pos.X / 16) * 16;
            pos.Y = ((int)pos.Y / 16) * 16;
            return pos;
        }
    }
}
