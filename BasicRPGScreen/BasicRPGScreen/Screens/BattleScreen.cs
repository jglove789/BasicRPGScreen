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
using System.Threading;

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
        private PlayerStats _stats;

        private float _pauseAlpha;
        private readonly InputAction _pauseAction;
        private KeyboardState _currentKeyboardState;
        private KeyboardState _previousKeyboardState;

        private List<Enemy> _enemyList = new();

        //Who's turn is it?
        private int _activeEntity;
        private int _turnCount;
        private bool _animationPlaying = false;
        private bool _battleOver = false;
        private bool _battleWonOrLost = true;
        private int _encounter;

        public BattleScreen(List<Enemy> enemies, int encounterScreen)
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            _pauseAction = new InputAction(
                new[] { Buttons.Start, Buttons.Back },
                new[] { Keys.Back, Keys.Escape }, true);

            _playerKnight = new PlayerKnight();
            _enemyList = enemies;
            _textBorder = new TextBorder();
            _turnCount = 1;
            _encounter = encounterScreen;
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
            _stats = this.ScreenManager.GetStats();
            _playerKnight.LoadStats(_stats);
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

            _previousKeyboardState = _currentKeyboardState;
            _currentKeyboardState = Keyboard.GetState();

            if(_activeEntity == 0) //Player turn
            {
                if((_currentKeyboardState.IsKeyDown(Keys.Z) && _previousKeyboardState.IsKeyUp(Keys.Z)) || GamePad.GetState(0).IsButtonDown(Buttons.A))
                {
                    //Attack enemy
                    int damage = _playerKnight.Strength;
                    _enemyList[0].CurrentHP -= damage;
                    if (_enemyList[0].CurrentHP < 0) _enemyList.RemoveAt(0);
                    _activeEntity = 1;
                }
                else if ((_currentKeyboardState.IsKeyDown(Keys.X) && _previousKeyboardState.IsKeyUp(Keys.X)) || GamePad.GetState(0).IsButtonDown(Buttons.X))
                {
                    //Attack enemy with special
                    _activeEntity = 1;
                }
                else if ((_currentKeyboardState.IsKeyDown(Keys.C) && _previousKeyboardState.IsKeyUp(Keys.C)) || GamePad.GetState(0).IsButtonDown(Buttons.B))
                {
                    //Block
                    _activeEntity = 1;
                }
                else if ((_currentKeyboardState.IsKeyDown(Keys.V) && _previousKeyboardState.IsKeyUp(Keys.V)) || GamePad.GetState(0).IsButtonDown(Buttons.Y))
                {
                    //Use healing charge
                    _playerKnight.CurrentHP += _playerKnight.MaxHP / 2;
                    _playerKnight.HealCount -= 1;
                    _activeEntity = 1;
                }
            }
            else //Enemy turn
            {
                //Always attacks player
                _turnCount++;
                _animationPlaying = true;
                
                if(_enemyList.Count > 0)
                {
                    int damage = _enemyList[_activeEntity - 1].Damage;
                    /*if not blocking*/
                    _playerKnight.CurrentHP -= damage;
                }

                if(_enemyList.Count == _activeEntity)
                {
                    _activeEntity = 0;
                }
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
            _spriteBatch.DrawString(_spriteFont, "HP: " + _playerKnight.CurrentHP + "/" + _playerKnight.MaxHP, new Vector2(240, 500), Color.White, 0, new Vector2(), 2f, SpriteEffects.None, 0);
            _spriteBatch.DrawString(_spriteFont, "HP: " + _playerKnight.CurrentHP + "/" + _playerKnight.MaxHP, new Vector2(240, 500), Color.White, 0, new Vector2(), 2f, SpriteEffects.None, 0);

            if(_enemyList.Count > 0)
            {
                foreach (var enemy in _enemyList)
                {
                    enemy.Draw(gameTime, _spriteBatch, 0);
                }
            }
            else
            {
                _spriteBatch.DrawString(_spriteFont, "You Win!", new Vector2(560, 200), Color.Green, 0, new Vector2(), 2f, SpriteEffects.None, 0);
                ExitScreen();
                //Thread.Sleep(2000);
                //if (_encounter == 1) ScreenManager.AddScreen(new FirstEncounterGameplayScreen(false), ControllingPlayer);
            }

            if (_playerKnight.CurrentHP < 0)
            {
                _spriteBatch.DrawString(_spriteFont, "You Win!", new Vector2(560, 200), Color.Green, 0, new Vector2(), 2f, SpriteEffects.None, 0);
                Thread.Sleep(2000);
                LoadingScreen.Load(ScreenManager, false, null, new MainMenuScreen());
            }

            if (_activeEntity == 0)
            {
                //draw menu options
                _spriteBatch.DrawString(_spriteFont, "Attack\nPress Z/A", new Vector2(240, 600), Color.Green, 0, new Vector2(), 0.5f, SpriteEffects.None, 0);
                _spriteBatch.DrawString(_spriteFont, "Special\nPress X/X", new Vector2(320, 600), Color.Blue, 0, new Vector2(), 0.5f, SpriteEffects.None, 0);
                _spriteBatch.DrawString(_spriteFont, "Block\nPress C/B", new Vector2(400, 600), Color.Red, 0, new Vector2(), 0.5f, SpriteEffects.None, 0);
                _spriteBatch.DrawString(_spriteFont, "Heal: " + _playerKnight.HealCount + " Charges Left\nPress V/Y", new Vector2(480, 600), Color.Yellow, 0, new Vector2(), 0.5f, SpriteEffects.None, 0);
            }
            else
            {
                //draw enemy attack
            }

            _spriteBatch.End();

            if (TransitionPosition > 0 || _pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, _pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }
        }
    }
}
