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
    public class ActionScene : GameScene
    {      
        private SpriteBatch spriteBatch;
        private Texture2D BGLevel1;
        private Texture2D playerTex, platformTex, enemySpiderTex;
        private SpriteFont highlightFont;
        Player player;
        Enemy enemySpider;
        public int score=0;
        public int updateEnemy=0;
        private bool isAlive = true;
        private EnemyAnimation spiderAnimation;
        Vector2 spiderEnemyStart = new Vector2(Shared.stage.X, 288);
        public bool gameOver = false;       
        Texture2D enemySpiderAnimationTex;
        // Added December 6th       
        public static Random rnd = new Random();
        public int updateEnemyRandom = 80;
        Game1 g;
        //Added december 7th
        private CollisionManager cm;
        Rectangle enemySpiderSrcRect;
            Vector2 enemySpiderSpeed;
        SoundEffect deathSound;
        Score playerScore;
        Texture2D texRestartKey;
        Texture2D texRestartPicture;
        private Animation restartKey;
        private Animation restartText;
        Texture2D texLevel1, texLevel2, texLevel3;
        private Animation level1, level2, level3;
        public int level = 1;
        public int rndMax = 200;
        Texture2D escapeText;
        Texture2D texEscapeKey;
        private Animation startScreen,escapeKey;
        private Song backgroundMusic;
        BackGroundMusicManager startSceneBgMusic;
        public ActionScene(Game game) : base(game)
        {
            g = (Game1)game;           
            this.spriteBatch = g.spriteBatch;           
            playerTex = g.Content.Load<Texture2D>("SpriteSheet/playerWalk");
            SoundEffect playerJump=g.Content.Load<SoundEffect>("Sounds/SoundEffects/jump");
            //BackGround Parallax
            Texture2D backgroundFGTex = g.Content.Load<Texture2D>("Images/parrallaxFG");
            Rectangle backgroundFGSrcRect = new Rectangle(0, 0, backgroundFGTex.Width, backgroundFGTex.Height);
            Vector2 backgroundFGSpeed = new Vector2(3,0);
            Vector2 backgroundFGPosition = new Vector2(Shared.stage.X- backgroundFGTex.Width, 100);

            Texture2D backgroundBG1Tex = g.Content.Load<Texture2D>("Images/parrallax2");
            Vector2 backgroundBG1Speed = new Vector2(2, 0);
            Vector2 backgroundBG1Position = new Vector2(Shared.stage.X, Shared.stage.Y - backgroundBG1Tex.Height-115);

            Texture2D backgroundMountTex = g.Content.Load<Texture2D>("Images/Mountain");
            Vector2 backgroundMountSpeed = new Vector2(1, 0);
            Vector2 backgroundMountPosition = new Vector2(0, 100);

            ScrollingBackground FG = new ScrollingBackground(g,spriteBatch, backgroundFGTex, backgroundFGSrcRect,backgroundFGPosition, backgroundFGSpeed);
            ScrollingBackground BG1 = new ScrollingBackground(g, spriteBatch, backgroundBG1Tex, backgroundFGSrcRect, backgroundBG1Position, backgroundBG1Speed);
            ScrollingBackground Mount = new ScrollingBackground(g, spriteBatch, backgroundMountTex, backgroundFGSrcRect, backgroundMountPosition, backgroundMountSpeed);
            this.Components.Add(Mount);
            this.Components.Add(BG1);
            this.Components.Add(FG);
            //HighScore
            SpriteFont highlightFont = game.Content.Load<SpriteFont>("Fonts/HighlightFont");
            playerScore = new Score(g, spriteBatch, highlightFont);
            this.Components.Add(playerScore);
            //Platform creation
            Texture2D platformTex = g.Content.Load<Texture2D>("Images/platformTileQuad");
            Rectangle platformSrcRect = new Rectangle(0, 0, platformTex.Width, platformTex.Height);
            Vector2 platformSpeed = new Vector2(4, 0);
            Vector2 platformPostion = new Vector2(0,350);
            Platform platform1 = new Platform(g, spriteBatch, platformTex, platformSrcRect, platformPostion, platformSpeed);
            this.Components.Add(platform1);

            //Enemy Creation

            enemySpiderTex = g.Content.Load<Texture2D>("Images/Enemy1");
            enemySpiderSrcRect = new Rectangle(0, 0, enemySpiderTex.Width, enemySpiderTex.Height);
            enemySpiderSpeed = new Vector2(4,0);
            //Vector2 enemySpiderPosition = new Vector2(Shared.stage.X- enemySpiderTex.Width, 285);
            enemySpider = new Enemy(g, spriteBatch, enemySpiderTex, enemySpiderSrcRect, spiderEnemyStart, enemySpiderSpeed);
            //this.Components.Add(enemySpider);
           // enemySpider.Visible = false;
            //Enemy Animation
            enemySpiderAnimationTex = g.Content.Load<Texture2D>("SpriteSheet/SpiderWalk");
            spiderAnimation = new EnemyAnimation(g, spriteBatch, enemySpiderAnimationTex, spiderEnemyStart, 0);

            //this.Components.Add(spiderAnimation);
            //spiderAnimation.start();
            //Player creation
            Vector2 playerPosition = new Vector2(Shared.stage.X/4, Shared.stage.Y-platformTex.Height-playerTex.Height);
            player = new Player(g, spriteBatch, playerTex, playerPosition, Shared.stage,playerJump);
            this.Components.Add(player);
            player.start();

            //Added December 7th

            deathSound = g.Content.Load<SoundEffect>("Sounds/SoundEffects/death");
            texRestartKey = g.Content.Load<Texture2D>("Images/R-Key");
            restartKey = new Animation(g, spriteBatch, texRestartKey, new Vector2(200, 100), 10);
            this.Components.Add(restartKey);
            restartKey.Visible = false;
            texRestartPicture = g.Content.Load<Texture2D>("Images/restartText");
            restartText = new Animation(g, spriteBatch, texRestartPicture, new Vector2(250, 100), 10);
            this.Components.Add(restartText);
            restartText.Visible = false;
            texLevel1 = g.Content.Load<Texture2D>("SpriteSheet/level1");
            texLevel2 = g.Content.Load<Texture2D>("SpriteSheet/level2");
            texLevel3 = g.Content.Load<Texture2D>("SpriteSheet/level3");
            level1 = new Animation(g, spriteBatch, texLevel1, new Vector2(450, 50), 10);
            level2 = new Animation(g, spriteBatch, texLevel2, new Vector2(450, 50), 10);
            level3 = new Animation(g, spriteBatch, texLevel3, new Vector2(450, 50), 10);
            this.Components.Add(level1);
            this.Components.Add(level2);
            this.Components.Add(level3);
            level1.Visible = true;
            level1.start();
            level2.Visible = false;
            level3.Visible = false;

            escapeText = g.Content.Load<Texture2D>("Images/mainMenu"); ;
            texEscapeKey = g.Content.Load<Texture2D>("Images/Esc-Key");

            startScreen = new Animation(g, spriteBatch, escapeText, new Vector2(60, 10), 10);
            escapeKey = new Animation(g, spriteBatch, texEscapeKey, new Vector2(10, 10), 10);
            this.Components.Add(startScreen);
            this.Components.Add(escapeKey);
            startScreen.start();
            escapeKey.start();

            backgroundMusic = g.Content.Load<Song>("Sounds/Music/FunkyChill");
            startSceneBgMusic = new BackGroundMusicManager(g, backgroundMusic);
            this.Components.Add(startSceneBgMusic);
            startSceneBgMusic.start();

        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            updateEnemy++;
            if (updateEnemy>300)
            {
                updateEnemy = 0;
            }
            if (updateEnemy==updateEnemyRandom && gameOver == false)
            {
                if (playerScore.score>=0 && playerScore.score<200)
                {
                    level = 1;
                    level1.Visible = true;
                    level1.start();
                }
                if (playerScore.score>=200 && playerScore.score < 400)
                {
                    level = 2;
                    level1.Visible = false;
                    level1.stop();
                    level2.Visible = true;
                    level2.start();
                }
                if (playerScore.score >= 400 )
                {
                    level = 3;
                    level3.Visible = true;
                    level3.start();
                    level2.Visible = false;
                    level2.stop();
                }
                spiderAnimation = new EnemyAnimation(g, spriteBatch, enemySpiderAnimationTex, spiderEnemyStart, 0);
                this.Components.Add(spiderAnimation);
                if (level==1)
                {
                    spiderAnimation.speed = new Vector2(4, 0);
                    rndMax = 200;
                }
                if (level == 2)
                {
                    spiderAnimation.speed = new Vector2(6, 0);
                    rndMax = 150;
                }
                if (level == 3)
                {
                    spiderAnimation.speed = new Vector2(7, 0);
                    rndMax = 110;
                }
                spiderAnimation.start();
                updateEnemy = 0;
                updateEnemyRandom = rnd.Next(70, rndMax);
                cm = new CollisionManager(g, player, spiderAnimation, deathSound);
                this.Components.Add(cm);
            }
            //Added December 7th
            if (player.isDead == true && Keyboard.GetState().IsKeyDown(Keys.R))
            {
                player.isDead = false;
                playerScore.score = 0;
                playerScore.gameOver = false;
                this.gameOver = false;
                player.Visible = true;
                restartKey.Visible = false;
                restartKey.stop();
                restartText.Visible = false;
                restartText.stop();
                level = 1;
                level1.Visible = true;
                level1.start();
                startSceneBgMusic.start();
            }
            if (player.isDead==true)
            {
                playerScore.gameOver = true;
                gameOver = true;
                //add death screen here
                restartKey.Visible = true;
                restartKey.start();
                restartText.Visible = true;
                restartText.start();
                level1.Visible = false;
                level2.Visible = false;
                level3.Visible = false;
                startSceneBgMusic.stop();
            }
       
            base.Update(gameTime);
        }
        protected override void LoadContent()
        {
            base.LoadContent();
        }
    }
}
