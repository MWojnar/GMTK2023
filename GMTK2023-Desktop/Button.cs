using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMTK2023_Desktop
{
    internal class Button : Entity
    {
        private Action clickFunc;
        private bool lastPressed;

        public Button(GMTK2023Game game, Vector2 position, Sprite sprite, GameTime gameTime, Action clickFunc) : base(game, position, sprite, gameTime)
        {
            this.clickFunc = clickFunc;
            lastPressed = false;
        }

        public override void Update(GameTime gameTime)
        {
            if (!lastPressed)
            {
                if (clickFunc != null && Mouse.GetState().LeftButton == ButtonState.Pressed && IsMouseOver())
                    clickFunc();
            }
            else if (Mouse.GetState().LeftButton == ButtonState.Released)
                lastPressed = false;
            base.Update(gameTime);
        }
    }
}
