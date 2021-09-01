using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace JBatesFinalProject
{
    class Enemy : DrawableGameComponent
    {
        public SpriteBatch spriteBatch;
        public  Texture2D tex;
        public Rectangle srcRect;
        public Vector2 position;
        public Vector2 speed;


        public Enemy(Game game, SpriteBatch spriteBatch, Texture2D tex, Rectangle srcRect, Vector2 position, Vector2 speed) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.srcRect = srcRect;
            this.position = position;
            this.speed = speed;
           
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, srcRect, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            position -= speed;        
            base.Update(gameTime);
        }
        public Rectangle getBound()
        {
            return new Rectangle((int)position.X, (int)position.Y,
                64, tex.Height);
        }
    }
}
