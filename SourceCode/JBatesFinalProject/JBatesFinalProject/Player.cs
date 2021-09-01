using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace JBatesFinalProject
{
    class Player : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 playerPosition;
        private Vector2 speed;
        private float rotation = 0f;
        private Rectangle srcRect;
        private Vector2 origin;
        private Vector2 stage;
        private float scale = 1.0f;

        private float scaleChange = 1.0f;
        private const float Max_Scale = 2.0F;
        private const float Min_Scale = 0.5F;
        public Vector2 position;
        private Vector2 velocity;
        private bool hasJumped;

        private float oldValue;

        SoundEffect jumpSound;
        private Vector2 dimension;
        private List<Rectangle> frames;

        private int frameIndex = -1;
        private int delay=2;
        private int delayCounter;

        private int ROW = 1;
        private int COL = 5;

        //Added December 7th
        public bool isDead = false;
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
        public Player(Game game, SpriteBatch spriteBatch, Texture2D tex, Vector2 playerPosition,
             Vector2 stage,SoundEffect jumpSound) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = playerPosition;          
            this.stage = stage;
            this.jumpSound = jumpSound;
            hasJumped = false;
            srcRect = new Rectangle(0, 0, tex.Width, tex.Height);
            origin = new Vector2(tex.Width, tex.Height);
            dimension = new Vector2(tex.Width / COL, tex.Height / ROW);
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
            #region Player Controls
            position += velocity;
            //Game controls will go here
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && hasJumped==false &&isDead==false)
            {           
                jumpSound.Play();
                position.Y -= 10f;
                velocity.Y -= 5f;
                hasJumped = true;
            }
            if (hasJumped==true)
            {
                //if (scale==Max_Scale)
                //{
                //    float i = 1;
                //    velocity.Y += 0.1f * i;
                //}
                //else if (scale==Min_Scale)
                //{
                //    float i = 1;                   
                //    velocity.Y += 0.20f * i;
                //}
                //else
                //{
                //    float i = 1;
                //    velocity.Y += 0.15f * i;
                //}
                float i = 1;
                velocity.Y += 0.15f * i;
            }

            //Scalling Controls
            MouseState ms = Mouse.GetState();
            float currValue = ms.ScrollWheelValue;
            if (currValue != oldValue)
            {
                float scaleValue = (currValue - oldValue) / 120;
                scale += scaleValue * scaleChange;

                if (scale > Max_Scale)
                {
                    scale = Max_Scale;
                }
                if (scale < Min_Scale)
                {
                    scale = Min_Scale;
                }
 
                oldValue = currValue;
            }
            #endregion
            #region Collisions
            //Collision with Platform
            if (position.Y + tex.Height >= stage.Y-128)
            {
                hasJumped = false;
            }
            if (hasJumped == false)
            {
                velocity.Y = 0f;
            }
            #endregion

            base.Update(gameTime);
        }

        //Added December 7th
        public Rectangle getBound()
        {
            return new Rectangle((int)position.X, (int)position.Y,
                55, 55);
        }
    }
}
