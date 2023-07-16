using Microsoft.Xna.Framework;

namespace GMTK2023_Desktop
{
	public class KamikazeInvader : Invader
	{
		public KamikazeInvader(GMTK2023Game game, Vector2 position, GameTime gameTime, Fleet fleet) : base(game, position, game.AssetManager.GetSprite("SpriteKamikazeInvader"), game.AssetManager.GetSprite("SpriteKamikazeInvaderL"), game.AssetManager.GetSprite("SpriteKamikazeInvaderDeath"), gameTime, fleet, (float)game.Settings.BombInvaderSpeed, 1, 2, 2, game.Settings.BombInvaderShotInterval, game.AssetManager.GetSprite("SpriteKamikazeInvaderR"))
		{
		}

		protected override void die(GameTime gameTime)
		{
			SetAnimation(new Animation(deathSprite, gameTime));
			SetPos(GetPos().X, GetPos().Y);
			game.AssetManager.GetSound("SoundInvaderDeath").Play();
			game.CreateEntity(new Bomb(game, new Vector2(GetPos().X, GetPos().Y), gameTime));
		}
	}
}