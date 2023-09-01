using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BasicRPGScreen
{
    public class BasicRPGScreenGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private PlayerKnight _playerKnight;
        private SignSprite[] _signSprites;
        private WoodenDoorSprite _door;
        private SpriteFont _spriteFont;

        /// <summary>
        /// A game with an RPG feel but is mostly just a title screen
        /// </summary>
        public BasicRPGScreenGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Initializes the game
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _playerKnight = new PlayerKnight();
            _signSprites = new SignSprite[]
            {
                new SignSprite(new Vector2(200, 200), "Are you ready to go on a big adventure?"),
                new SignSprite(new Vector2(300, 200), "Well it's not ready yet."),
                new SignSprite(new Vector2(400, 200), "So for now walk to the door."),
                new SignSprite(new Vector2(500, 200), "Then press Space or A on the GamePad to exit.")
            };
            _door = new WoodenDoorSprite(new Vector2(600, 200));

            base.Initialize();
        }

        /// <summary>
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
        }

        /// <summary>
        /// Updates the game world
        /// </summary>
        /// <param name="gameTime">The game time</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            _playerKnight.Update(gameTime);

            //Detect and process collisions
            foreach(var sign in _signSprites)
            {
                if (sign.Bounds.CollidesWith(_playerKnight.Bounds)) sign.ReadSign = true;
                else sign.ReadSign = false;
            }
            if(_door.Bounds.CollidesWith(_playerKnight.Bounds))
                if(Keyboard.GetState().IsKeyDown(Keys.Space) || GamePad.GetState(0).IsButtonDown(Buttons.A))
                    base.Exit();

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the game world
        /// </summary>
        /// <param name="gameTime">The game time</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.ForestGreen);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            foreach (var sign in _signSprites)
            {
                sign.Draw(gameTime, _spriteBatch);
                if(sign.ReadSign) 
                    _spriteBatch.DrawString(_spriteFont, sign.Text, new Vector2(sign.Position.X - 20, sign.Position.Y - 20), Color.Gold, 0, new Vector2(150,0), 0.5f, SpriteEffects.None, 0);
            }
            _door.Draw(gameTime, _spriteBatch);
            _playerKnight.Draw(gameTime, _spriteBatch);
            _spriteBatch.DrawString(_spriteFont, "Simple RPG", new Vector2(320, 50), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}