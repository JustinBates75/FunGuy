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
    public class EnemyAnimation : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;
        private Vector2 dimension;

        private List<Rectangle> frames;

        private int frameIndex = -1;
        private int delay;
        private int delayCounter;

        public int ROW = 9;
        public int COL = 1;
        public Vector2 speed= new Vector2(4, 0);
        public Vector2 Position { get => position; set => position = value; }
        public void start()
        {
            this.Enabled = true;
            this.Visible = true;
        }
        public void stop()
        {
            this.Enabled = false;
            this.Visible = false;
        }
        public EnemyAnimation(Game game,
           SpriteBatch spritebatch,
           Texture2D tex,
           Vector2 position,
           int delay) : base(game)
        {
            this.spriteBatch = spritebatch;
            this.tex = tex;
            this.position = position;
            this.delay = delay;

            dimension = new Vector2(tex.Width / COL, tex.Height / ROW);
            //stop();

            //CreateFrames
            CreateFrames();
        }
        private void CreateFrames()
        {
            frames = new List<Rectangle>();
            for (int i = 0; i < ROW; i++)
            {
                for (int j = 0; j < COL; j++)
                {
                    int x = j * (int)dimension.X;
                    int y = i * (int)dimension.Y;
                    Rectangle r = new Rectangle(x, y, (int)dimension.X, (int)dimension.Y);
                    frames.Add(r);
                }
            }
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if (frameIndex >= 0)
            {
                //Version 4
                spriteBatch.Draw(tex, position, frames[frameIndex], Color.White);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            delayCounter++;
            position -=speed;
            if (delayCounter > delay)
            {
                frameIndex++;
                if (frameIndex > ROW * COL - 1)
                {
                    frameIndex = 0;
                    //stop();
                }
                
                delayCounter = 0;
                //start();
            }
            //position -= speed;
            base.Update(gameTime);
        }
        //Aded December 7th

        public Rectangle getBound()
        {
            return new Rectangle((int)position.X, (int)position.Y,
                55, 55);
        }

    }
}
