using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GMTK2023_Desktop
{
    public class GMTK2023Game : Game
    {
        private GraphicsDeviceManager _graphics;
        private ExtendedSpriteBatch _spriteBatch;
        public AssetManager AssetManager;
        private List<Entity> entities;
        public List<Entity> Entities { get { return entities; } }
        private List<Entity> entitiesToRemove;
        private List<Entity> entitiesToAdd;
        private Matrix viewMatrix;
        private Vector2 mousePos;
        private int points;
        private GameTime gameTime;

        public int Points { get { return points; } }
        public GameTime GameTime { get { return gameTime; } }

		public List<KeyValuePair<Vector2, InvaderType>> SavedFleet;

		public Vector2 MousePos {
            get {
                mousePos.X = Mouse.GetState().X / viewMatrix.M11;
				mousePos.Y = Mouse.GetState().Y / viewMatrix.M22;
                return mousePos;
			}
        }

        public GMTK2023Game()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 512;
            _graphics.PreferredBackBufferHeight = 736;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            viewMatrix = Matrix.CreateScale(2, 2, 1);
            mousePos = new Vector2();
            entitiesToAdd = new List<Entity>();
            entitiesToRemove = new List<Entity>();
        }

        protected override void Initialize()
        {
            entities = new List<Entity>();
			SavedFleet = null;
            points = 0;

			base.Initialize();
        }

        public void AddPoints(int amount)
        {
            points += amount;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new ExtendedSpriteBatch(GraphicsDevice);
            AssetManager = new AssetManager(Content);
            AssetManager.Load();
			MediaPlayer.Play(AssetManager.GetMusic("MusicMain"));

			StartRoom(1, new GameTime());
		}

        public void StartRoom(int room, GameTime gameTime)
        {
            ClearEntities();
			for (int x = 0; x < 256; x += 16)
				CreateEntity(new Entity(this, new Vector2(x, 352), AssetManager.GetSprite("SpriteGround"), gameTime));
			if (room == 0)
            {

            } else if (room == 1)
            {
				CreateEntity(new FleetCreator(this, new Vector2(16, 32), gameTime));
			} else if (room == 2)
            {
				CreateEntity(new Fleet(this, new Vector2(16, 32), gameTime));
				for (int i = 16; i < 16 + (64 * 4); i += 64)
					CreateEntity(new Barrier(this, new Vector2(i, 288), gameTime));
                CreateEntity(new FleetingEntity(this, new Vector2(128 - 8 - 16, 336 - 16), AssetManager.GetSprite("SpriteEnemySpawn"), gameTime, () => CreateEntity(new Enemy(this, new Vector2(128 - 8, 336), this.gameTime))));
                points = 0;
			}
        }

        protected override void Update(GameTime gameTime)
		{
			this.gameTime = gameTime;

			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (Entity entity in entities)
                if (!entitiesToRemove.Contains(entity))
                    entity.Update(gameTime);

            removeEntities();
            addEntities();

            base.Update(gameTime);
        }

        private void addEntities()
        {
			foreach (Entity entity in entitiesToAdd)
				insertEntityByDepth(entity);
			entitiesToAdd.Clear();
		}

        private void removeEntities()
        {
			foreach (Entity entity in entitiesToRemove)
				entities.Remove(entity);
			entitiesToRemove.Clear();
		}

        public void CreateEntity(Entity entity)
        {
            if (!entities.Contains(entity) && !entitiesToAdd.Contains(entity))
                entitiesToAdd.Add(entity);
        }

        public void RemoveEntity(Entity entity)
        {
			if (entities.Contains(entity) && !entitiesToRemove.Contains(entity))
				entitiesToRemove.Add(entity);
        }

        public void ClearEntities()
        {
            foreach (Entity entity in entities)
                RemoveEntity(entity);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

			_spriteBatch.Begin(SpriteSortMode.Deferred, samplerState: SamplerState.PointClamp, transformMatrix: viewMatrix);

			foreach (Entity entity in entities)
                entity.Draw(_spriteBatch, gameTime);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void UpdateEntityDepth(Entity entity)
        {
            if (entities.Contains(entity))
            {
                int index = entities.IndexOf(entity);
                if (!(index - 1 < 0 || entities[index - 1].Depth <= entity.Depth) || !(index + 1 >= entities.Count || entities[index + 1].Depth >= entity.Depth))
                {
                    entities.Remove(entity);
                    insertEntityByDepth(entity);
                }
            }
        }

        private void insertEntityByDepth(Entity entity)
        {
            var lesserEntry = entities.LastOrDefault(e => e.Depth <= entity.Depth);
            if (lesserEntry == null)
                entities.Insert(0, entity);
            else
                entities.Insert(entities.IndexOf(lesserEntry) + 1, entity);
        }

        public void GameOver()
        {
            StartRoom(3, new GameTime());
        }
    }
}