using BasicRPGScreen.Screens;
using BasicRPGScreen.StateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BasicRPGScreen
{
    public class BasicRPGScreenGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private readonly ScreenManager _screenManager;
        /*private SpriteBatch _spriteBatch;

        private PlayerKnight _playerKnight;
        private SignSprite[] _signSprites;
        private WoodenDoorSprite _door;
        private SpriteFont _spriteFont;
        private TextBorder _textBorder;*/

        /// <summary>
        /// A game with an RPG feel but is mostly just a title screen
        /// </summary>
        public BasicRPGScreenGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _screenManager = new ScreenManager(this);
            Components.Add(_screenManager);

            AddInitialScreens();
        }

        private void AddInitialScreens()
        {
            _screenManager.AddScreen(new MainMenuScreen(), null);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        /*/// <summary>
        /// Initializes the game
        /// </summary>
        protected override void Initialize()
        {
            //Set the screen size
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            // TODO: Add your initialization logic here
            _playerKnight = new PlayerKnight();
            _signSprites = new SignSprite[]
            {
                new SignSprite(new Vector2(300, 280), "Are you ready to go on a big adventure?"),
                new SignSprite(new Vector2(500, 280), "Well it's not ready yet. There is a cool text box now though!"),
                new SignSprite(new Vector2(700, 280), "So run on over to the door and press Space or A on the GamePad to exit."),
                new SignSprite(new Vector2(900, 280), "Next time it WILL lead to something cool :D")
            };
            _door = new WoodenDoorSprite(new Vector2(1120, 280));
            _textBorder = new TextBorder();

            base.Initialize();
        }*/

        /*/// <summary>
        /// Loads content for the game
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _playerKnight.LoadContent(Content);
            foreach (var sign in _signSprites) sign.LoadContent(Content);
            _door.LoadContent(Content);
            _spriteFont = Content.Load<SpriteFont>("sunnyspells");
            _textBorder.LoadContent(Content);
        }*/

        protected override void LoadContent() { }

        /// <summary>
        /// Updates the game world
        /// </summary>
        /// <param name="gameTime">The game time</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            /*_playerKnight.Update(gameTime);

            //Detect and process collisions
            foreach(var sign in _signSprites)
            {
                if (sign.Bounds.CollidesWith(_playerKnight.Bounds)) sign.ReadSign = true;
                else sign.ReadSign = false;
            }
            if(_door.Bounds.CollidesWith(_playerKnight.Bounds))
                if(Keyboard.GetState().IsKeyDown(Keys.Space) || GamePad.GetState(0).IsButtonDown(Buttons.A))
                    base.Exit();*/

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the game world
        /// </summary>
        /// <param name="gameTime">The game time</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.ForestGreen);
            base.Draw(gameTime);
            /*GraphicsDevice.Clear(Color.ForestGreen);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            foreach (var sign in _signSprites)
            {
                sign.Draw(gameTime, _spriteBatch);
                if(sign.ReadSign)
                {
                    _textBorder.Draw(_spriteBatch);
                    _spriteBatch.DrawString(_spriteFont, sign.Text, new Vector2(275, 520), Color.Gold, 0, new Vector2(150, 0), 0.5f, SpriteEffects.None, 0);
                }
            }
            _door.Draw(gameTime, _spriteBatch);
            _playerKnight.Draw(gameTime, _spriteBatch);
            _spriteBatch.DrawString(_spriteFont, "Simple RPG", new Vector2(480, 50), Color.White, 0, new Vector2(), 2f, SpriteEffects.None, 0);
            _spriteBatch.DrawString(_spriteFont, "EXIT", new Vector2(1132, 260), Color.Red, 0, new Vector2(), 0.65f, SpriteEffects.None, 0);
            _spriteBatch.End();

            base.Draw(gameTime);*/
        }
    }
}