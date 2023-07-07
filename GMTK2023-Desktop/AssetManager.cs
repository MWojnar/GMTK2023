using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMTK2023_Desktop
{
    public class AssetManager
    {
        private Dictionary<string, Sprite> sprites;
        private ContentManager contentManager;

        public AssetManager(ContentManager content)
        {
            sprites = new Dictionary<string, Sprite>();
            contentManager = content;
        }

        public Sprite GetSprite(string assetName)
        {
            // check if the texture is already loaded
            if (!sprites.TryGetValue(assetName, out var sprite))
            {
                // if not, load it and store it in the dictionary
                sprite = new Sprite(contentManager.Load<Texture2D>(assetName));
                sprites.Add(assetName, sprite);
            }

            // return the texture
            return sprite;
        }

        public void Load()
        {
            sprites.Clear();
            sprites.Add("test", new Sprite(contentManager.Load<Texture2D>("Sprites\\test")));
        }
    }
}
