using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GMTK2023_Desktop
{
	internal class Barrier : Entity
	{
		private bool[,] damaged;
		private Rectangle[,] drawRects;
		private Vector2[,] drawPositions;
		private int size;
		private int dim;

		public Barrier(GMTK2023Game game, Vector2 position, GameTime gameTime, float depth = 0) : base(game, position, game.AssetManager.GetSprite("SpriteBarrier"), gameTime, depth)
		{
			size = 4;
			dim = baseSprite.Width / size;
			damaged = new bool[dim, dim];
			drawRects = new Rectangle[dim,dim];
			drawPositions = new Vector2[dim, dim];
			for (int x = 0; x < dim; x++)
				for (int y = 0; y < dim; y++)
				{
					drawRects[x, y] = new Rectangle(x * size, y * size, size, size);
					drawPositions[x, y] = new Vector2(position.X + x * size, position.Y + y * size);
				}
			sourceRect = new Rectangle(0, 0, baseSprite.Width, baseSprite.Height);
		}

		public override void Draw(ExtendedSpriteBatch spriteBatch, GameTime gameTime)
		{
			for (int x = 0; x < dim; x++)
				for (int y = 0; y < dim; y++)
					if (!damaged[x, y])
						baseSprite.Draw(drawRects[x, y], spriteBatch, drawPositions[x, y]);
		}

		internal bool Hit(Entity entity)
		{
			var pos = GetPos();
			var entityPos = entity.GetPos();
			int maxX = 0;
			int maxY = 0;
			int maxVal = int.MinValue;
			for (int x = 0; x < dim; x++)
				for (int y = 0; y < dim; y++)
					if (!damaged[x, y])
					{
						int area = Rectangle.Intersect(drawRects[x, y].OffsetBy(pos), entity.SourceRect.Value.OffsetBy(entityPos)).Area();
						if (area > maxVal)
						{
							maxX = x;
							maxY = y;
							maxVal = area;
						}
					}
			if (maxVal <= 0)
				return false;
			damaged[maxX, maxY] = true;
			game.AssetManager.GetSound("SoundBarrierDamage").Play();
			return true;
		}

		public bool IsGone()
		{
			return (!damaged.Cast<bool>().Any(e => !e));
		}

		internal bool IsHit(Rectangle rect)
		{
			var pos = GetPos();
			int maxX = 0;
			int maxY = 0;
			int maxVal = int.MinValue;
			for (int x = 0; x < dim; x++)
				for (int y = 0; y < dim; y++)
					if (!damaged[x, y])
					{
						int area = Rectangle.Intersect(drawRects[x, y].OffsetBy(pos), rect).Area();
						if (area > maxVal)
						{
							maxX = x;
							maxY = y;
							maxVal = area;
						}
					}
			return maxVal > 0;
		}

		public void Reset()
		{
			for (int x = 0; x < dim; x++)
				for (int y = 0; y < dim; y++)
					damaged[x, y] = false;
			foreach (Entity entity in game.Entities.Where(e => e is InvaderShot))
				if (IsCollidingWithEntity(entity))
					entity.Remove();
		}
	}
}
