using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace JBatesFinalProject
{
    class CollisionManager : GameComponent
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //private Bat bat;
        //private Ball ball;
        private Player player;
        private EnemyAnimation spiderEnemy;
        public bool playerDeath;

        private SoundEffect sound;
        public CollisionManager(Game game,
            Player player,
            EnemyAnimation spiderEnemy,
            SoundEffect sound) : base(game)
        {
            this.player = player;
            this.spiderEnemy = spiderEnemy;
            this.sound = sound;

        }

        public override void Update(GameTime gameTime)
        {
            if (player.isDead==false)
            {
                if (player.getBound().Intersects(spiderEnemy.getBound()))
                {

                    player.isDead = true;
                    player.Visible = false;

                    sound.Play();
                    playerDeath = true;

                }
            }
            
            base.Update(gameTime);
        }
    }
}
