using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
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
        private Dictionary<string, Song> music;
        private Dictionary<string, SoundEffect> sounds;
        private ContentManager contentManager;

        public AssetManager(ContentManager content)
        {
            sprites = new Dictionary<string, Sprite>();
			music = new Dictionary<string, Song>();
			sounds = new Dictionary<string, SoundEffect>();
            contentManager = content;
        }

        public Sprite GetSprite(string assetName)
        {
            if (!sprites.TryGetValue(assetName, out var sprite))
            {
                sprite = new Sprite(contentManager.Load<Texture2D>(assetName));
                sprites.Add(assetName, sprite);
            }

            return sprite;
        }

		public Song GetMusic(string assetName)
		{
			if (!music.TryGetValue(assetName, out var song))
			{
				song = contentManager.Load<Song>(assetName);
				music.Add(assetName, song);
			}

			return song;
		}

		public SoundEffect GetSound(string assetName)
		{
			if (!sounds.TryGetValue(assetName, out var sound))
			{
				sound = contentManager.Load<SoundEffect>(assetName);
				sounds.Add(assetName, sound);
			}

			return sound;
		}

		public void Load()
        {
            sprites.Clear();
            sprites.Add("SpriteBarrierInvader1", new Sprite(contentManager.Load<Texture2D>("Sprites/spr_barrier_invader_hp1_strip3"), 3, 10));
			sprites.Add("SpriteBarrierInvader2", new Sprite(contentManager.Load<Texture2D>("Sprites/spr_barrier_invader_hp2_strip3"), 3, 10));
			sprites.Add("SpriteBarrierInvader3", new Sprite(contentManager.Load<Texture2D>("Sprites/spr_barrier_invader_hp3_strip3"), 3, 10));
			sprites.Add("SpriteBasicInvaderL", new Sprite(contentManager.Load<Texture2D>("Sprites/spr_basic_invader_moveL_strip4"), 4, 10));
			sprites.Add("SpriteBasicInvader", new Sprite(contentManager.Load<Texture2D>("Sprites/spr_basic_invader_strip4"), 4, 10));
			sprites.Add("SpriteEnemyL", new Sprite(contentManager.Load<Texture2D>("Sprites/spr_enemy_moveL_strip4"), 4, 10));
			sprites.Add("SpriteEnemyShot", new Sprite(contentManager.Load<Texture2D>("Sprites/spr_enemy_shot_strip4"), 4, 10));
			sprites.Add("SpriteEnemy", new Sprite(contentManager.Load<Texture2D>("Sprites/spr_enemy_strip3"), 3, 10));
			sprites.Add("SpriteFastInvaderL", new Sprite(contentManager.Load<Texture2D>("Sprites/spr_fastfire_invader_moveL_strip3"), 3, 10));
			sprites.Add("SpriteFastInvader", new Sprite(contentManager.Load<Texture2D>("Sprites/spr_fastfire_invader_strip3"), 3, 10));
			sprites.Add("SpriteGround", new Sprite(contentManager.Load<Texture2D>("Sprites/spr_ground"), 1, 10));
			sprites.Add("SpriteInvaderShot", new Sprite(contentManager.Load<Texture2D>("Sprites/spr_invader_shot_strip4"), 4, 10));
			sprites.Add("SpriteGrid", new Sprite(contentManager.Load<Texture2D>("Sprites/spr_grid"), 1, 10));
			sprites.Add("SpriteGridSelect", new Sprite(contentManager.Load<Texture2D>("Sprites/spr_grid_select_size1_strip4"), 4, 10));
			sprites.Add("SpriteGridSelectBig", new Sprite(contentManager.Load<Texture2D>("Sprites/spr_grid_select_size2_strip4"), 4, 10));
			sprites.Add("SpriteBarrier", new Sprite(contentManager.Load<Texture2D>("Sprites/spr_barrier"), 1, 10));
			sprites.Add("SpriteBasicInvaderDeath", new Sprite(contentManager.Load<Texture2D>("Sprites/spr_basic_invader_death_strip4"), 4, 10));
			sprites.Add("SpriteFastInvaderDeath", new Sprite(contentManager.Load<Texture2D>("Sprites/spr_fastfire_invader_death_strip4"), 4, 10));
			sprites.Add("SpriteBarrierInvaderDeath", new Sprite(contentManager.Load<Texture2D>("Sprites/spr_barrier_invader_death_strip3"), 3, 10));
			sprites.Add("SpriteBarrierInvaderDeathSaucer", new Sprite(contentManager.Load<Texture2D>("Sprites/spr_barrier_invader_saucer_strip3"), 3, 10));
			sprites.Add("SpriteEnemySpawn", new Sprite(contentManager.Load<Texture2D>("Sprites/spr_enemy_spawn_strip5"), 5, 10));
			sprites.Add("SpriteEnemyDeath", new Sprite(contentManager.Load<Texture2D>("Sprites/spr_enemy_death_strip5"), 5, 10));

			music.Clear();
			music.Add("MusicMain", contentManager.Load<Song>("Music/msc_Invasion_final_mix"));

			sounds.Clear();
			sounds.Add("SoundBarrierDamage", contentManager.Load<SoundEffect>("Sounds/snd_barrier_damage"));
			sounds.Add("SoundExplosion", contentManager.Load<SoundEffect>("Sounds/snd_bomb_explosion"));
			sounds.Add("SoundEnemyDeath", contentManager.Load<SoundEffect>("Sounds/snd_enemy_death"));
			sounds.Add("SoundEnemyShoot", contentManager.Load<SoundEffect>("Sounds/snd_enemy_shot"));
			sounds.Add("SoundInvaderDeath", contentManager.Load<SoundEffect>("Sounds/snd_invader_death"));
			sounds.Add("SoundInvaderShoot", contentManager.Load<SoundEffect>("Sounds/snd_invader_shot"));
		}
    }
}
