using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace JBatesFinalProject
{
    public class BackGroundMusicManager : DrawableGameComponent
    {
        public Song backgroundMusic;

        public void start()
        {
            
            MediaPlayer.Play(backgroundMusic);
        }
        public void stop()
        {
            MediaPlayer.Stop();
           
        }

        public BackGroundMusicManager(Game game,Song backgroundMusic) : base(game)
        {
            this.backgroundMusic = backgroundMusic;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
