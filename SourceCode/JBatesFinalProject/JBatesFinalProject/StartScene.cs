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
    public class StartScene : GameScene
    {
        private MenuComponent menu;
        private SpriteBatch spriteBatch;
        private Texture2D background;
        private Song backgroundMusic;
        private SoundEffect song;
        string[] menuItems = {"Start Game",
        "Help",
        //"High Scores",
        "About",
        "Quit"};        
        public StartScene(Game game) : base(game)
        {
            //Update and Draw ovverides will be inhareted from the parent Class
            Game1 g = (Game1)game;
            this.spriteBatch = g.spriteBatch;
            //Load the resources
            //Both g and Game1 will work as the refrence type
            SpriteFont regularFont = g.Content.Load<SpriteFont>("Fonts/RegularFont");
            SpriteFont highlighFont = game.Content.Load<SpriteFont>("Fonts/HighlightFont");
            background = g.Content.Load<Texture2D>("Images/BGMushroom2");
            menu = new MenuComponent(g, spriteBatch, regularFont, highlighFont, menuItems);
            this.Components.Add(menu);
            //backgroundMusic = g.Content.Load<Song>("Sounds/Music/FunkyChill");
            //BackGroundMusicManager startSceneBgMusic = new BackGroundMusicManager(g, backgroundMusic);
            //this.Components.Add(startSceneBgMusic);
            //startSceneBgMusic.start();            
        }
        internal MenuComponent Menu { get => menu; set => menu = value; }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
