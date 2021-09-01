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
    public class Score : DrawableGameComponent
    {
        private Vector2 position;
        SpriteBatch spriteBatch;
        private SpriteFont highlightFont;
        public int score = 0;
        private int updateCount;
        public bool gameOver = false;
        private bool highScoreSet = false;

        private int[] highScores=new int [5];

        public Score(Game game,
            SpriteBatch spriteBatch,
            SpriteFont highlightFont) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.highlightFont = highlightFont;
            position =  new Vector2(50, 50);          
        }
      
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(highlightFont, "Score:"+score.ToString(),position,Color.Gold);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        
        public override void Update(GameTime gameTime)
        {
            if (gameOver == false)
            {
                updateCount++;
                if (updateCount == 10)
                {
                    score++;
                    updateCount = 0;
                }
            }
            else if (gameOver==true)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (highScoreSet == false && highScores[i] == 0)
                    {
                        highScores[i] = score;
                        highScoreSet = true;
                    }
                    else if (highScoreSet == false && highScores[i]<score)
                    {
                        highScores[i] = score;
                        highScoreSet = true;
                    }
                }
            }                
            base.Update(gameTime);
        }
    }
}
