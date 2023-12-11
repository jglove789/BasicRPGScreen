using BasicRPGScreen.SpriteCode;
using BasicRPGScreen.StateManagement;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.TimeZoneInfo;

namespace BasicRPGScreen.Screens
{
    public class BattleScreen : GameScreen
    {
        private ContentManager _content;

        private PlayerKnight _playerKnight;
        private SpriteFont _spriteFont;
        private TextBorder _textBorder;
        private Song backgroundMusic;
        private Tilemap _tilemap;

        private float _pauseAlpha;
        private readonly InputAction _pauseAction;

        //Who's turn is it?
        private int _activeEntity;
        private int _turnCount;
        private bool _animationPlaying = false;
        private bool _battleOver = false;
        private bool _battleWonOrLost = true;

        public BattleScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            _pauseAction = new InputAction(
                new[] { Buttons.Start, Buttons.Back },
                new[] { Keys.Back, Keys.Escape }, true);

            _playerKnight = new PlayerKnight();
            _textBorder = new TextBorder();
            _turnCount = 1;
        }

        public override void Activate()
        {
            if (_content == null)
                _content = new ContentManager(ScreenManager.Game.Services, "Content");

            _spriteFont = _content.Load<SpriteFont>("sunnyspells");

            _playerKnight.LoadContent(_content);
            _textBorder.LoadContent(_content);
            backgroundMusic = _content.Load<Song>("LevelMusic2");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.1f;
            MediaPlayer.Play(backgroundMusic);
            _tilemap = _content.Load<Tilemap>("BattleScreen");

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

            if(_activeEntity == 0) //Player turn
            {
                if(Keyboard.GetState().IsKeyDown(Keys.A) || GamePad.GetState(0).IsButtonDown(Buttons.A))
                {
                    //Attack enemy
                    _activeEntity = 1;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.A) || GamePad.GetState(0).IsButtonDown(Buttons.A))
                {
                    //Attack enemy with special
                    _activeEntity = 1;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.A) || GamePad.GetState(0).IsButtonDown(Buttons.A))
                {
                    //Block
                    _activeEntity = 1;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.A) || GamePad.GetState(0).IsButtonDown(Buttons.A))
                {
                    //Use healing charge
                    _activeEntity = 1;
                }
            }
            else //Enemy turn
            {
                //Always attacks player
                _activeEntity = 0;
                _turnCount++;
                _animationPlaying = true;
            }

            //Not sure if this statement will be necessary during combat
            /*if (IsActive)
            {
                _playerKnight.Update(gameTime);
            }*/
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

            /*PlayerIndex player;
            if (_pauseAction.Occurred(input, ControllingPlayer, out player) || gamePadDisconnected)
            {
                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
            }*/
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(Color.ForestGreen);

            var _spriteBatch = ScreenManager.SpriteBatch;

            _spriteBatch.Begin();
            _tilemap.Draw(gameTime, _spriteBatch);
            _playerKnight.Draw(gameTime, _spriteBatch);
            _spriteBatch.End();

            if (TransitionPosition > 0 || _pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, _pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }
        }
    }
}
