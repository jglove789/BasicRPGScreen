using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicRPGScreen.SpriteCode;
using BasicRPGScreen.StateManagement;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using System.Threading;

namespace BasicRPGScreen.Screens
{
    public class FirstEncounterGameplayScreen : GameScreen
    {
        private ContentManager _content;

        private PlayerKnight _playerKnight;
        private SignSprite[] _signSprites;
        private WoodenDoorSprite _door;
        private SpriteFont _spriteFont;
        private TextBorder _textBorder;
        private Song backgroundMusic;
        private Tilemap _tilemap;
        private Wolf _wolf;
        private List<Enemy> _enemyList = new List<Enemy>();
        private bool _isWolfAlive = true;

        private float _pauseAlpha;
        private readonly InputAction _pauseAction;

        public FirstEncounterGameplayScreen(bool wolfAlive)
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            _pauseAction = new InputAction(
                new[] { Buttons.Start, Buttons.Back },
                new[] { Keys.Back, Keys.Escape }, true);

            _playerKnight = new PlayerKnight();
            _signSprites = new SignSprite[]
            {

            };
            _door = new WoodenDoorSprite(new Vector2(1120, 280));
            _textBorder = new TextBorder();
            _wolf = new Wolf();
            _enemyList.Add(_wolf);
            _isWolfAlive = wolfAlive;
        }

        public override void Activate()
        {
            if (_content == null)
                _content = new ContentManager(ScreenManager.Game.Services, "Content");

            _spriteFont = _content.Load<SpriteFont>("sunnyspells");

            _playerKnight.LoadContent(_content);
            foreach (var sign in _signSprites) sign.LoadContent(_content);
            _door.LoadContent(_content);
            _textBorder.LoadContent(_content);
            backgroundMusic = _content.Load<Song>("LevelMusic2");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.1f;
            MediaPlayer.Play(backgroundMusic);
            _tilemap = _content.Load<Tilemap>("SecondLevel");
            _wolf.LoadContent(_content);

            //Thread.Sleep(500);

            ScreenManager.Game.ResetElapsedTime();
        }

        public override void Deactivate()
        {
            base.Deactivate();
        }

        public override void Unload()
        {
            _content.Unload();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            // Gradually fade in or out depending on whether we are covered by the pause screen.
            if (coveredByOtherScreen)
                _pauseAlpha = Math.Min(_pauseAlpha + 1f / 32, 1);
            else
                _pauseAlpha = Math.Max(_pauseAlpha - 1f / 32, 0);

            if (IsActive)
            {
                _playerKnight.Update(gameTime);

                //Detect and process collisions
                foreach (var sign in _signSprites)
                {
                    if (sign.Bounds.CollidesWith(_playerKnight.Bounds)) sign.ReadSign = true;
                    else sign.ReadSign = false;
                }
                if (_door.Bounds.CollidesWith(_playerKnight.Bounds))
                    if (Keyboard.GetState().IsKeyDown(Keys.Space) || GamePad.GetState(0).IsButtonDown(Buttons.A))
                        ScreenManager.AddScreen(new SecondEncounterGameplayScreen(), ControllingPlayer);
                if(_isWolfAlive) if (_wolf.Bounds.CollidesWith(_playerKnight.Bounds)) ScreenManager.AddScreen(new BattleScreen(_enemyList, 1), ControllingPlayer);
            }
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;

            var keyboardState = input.CurrentKeyboardStates[playerIndex];
            var gamePadState = input.CurrentGamePadStates[playerIndex];

            // The game pauses either if the user presses the pause button, or if
            // they unplug the active gamepad. This requires us to keep track of
            // whether a gamepad was ever plugged in, because we don't want to pause
            // on PC if they are playing with a keyboard and have no gamepad at all!
            bool gamePadDisconnected = !gamePadState.IsConnected && input.GamePadWasConnected[playerIndex];

            PlayerIndex player;
            if (_pauseAction.Occurred(input, ControllingPlayer, out player) || gamePadDisconnected)
            {
                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(Color.ForestGreen);

            var _spriteBatch = ScreenManager.SpriteBatch;

            _spriteBatch.Begin();
            _tilemap.Draw(gameTime, _spriteBatch);
            foreach (var sign in _signSprites)
            {
                sign.Draw(gameTime, _spriteBatch);
                if (sign.ReadSign)
                {
                    _textBorder.Draw(_spriteBatch);
                    _spriteBatch.DrawString(_spriteFont, sign.Text, new Vector2(275, 520), Color.Gold, 0, new Vector2(150, 0), 0.5f, SpriteEffects.None, 0);
                }
            }
            _door.Draw(gameTime, _spriteBatch);
            _playerKnight.Draw(gameTime, _spriteBatch);
            if(_isWolfAlive) _wolf.Draw(gameTime, _spriteBatch, 0);
            _spriteBatch.End();

            if (TransitionPosition > 0 || _pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, _pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }
        }
    }
}
