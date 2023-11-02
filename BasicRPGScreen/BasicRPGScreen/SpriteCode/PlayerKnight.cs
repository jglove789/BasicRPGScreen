using Microsoft.Xna.Framework;
using BasicRPGScreen.Collisions;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicRPGScreen.SpriteCode
{
    public class PlayerKnight
    {
        private GamePadState gamePadState;

        private KeyboardState keyboardState;

        private Texture2D textureIdle;

        private Texture2D textureMoving;

        private double animationTimer;

        private short animationFrame = 0;

        private Vector2 position = new Vector2(100, 200);

        private bool flipped;

        private BoundingRectangle bounds = new BoundingRectangle(new Vector2(100 - 10, 200 - 19), 20, 38);

        /// <summary>
        /// The color blend with the ghost
        /// </summary>
        public Color Color { get; set; } = Color.White;

        /// <summary>
        /// The bounding volume of the sprite
        /// </summary>
        public BoundingRectangle Bounds => bounds;

        /// <summary>
        /// Loads the sprite texture using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            textureIdle = content.Load<Texture2D>("_Idle");
            textureMoving = content.Load<Texture2D>("_Run");
        }

        /// <summary>
        /// Updates the sprite's position based on user input
        /// </summary>
        /// <param name="gameTime">The GameTime</param>
        public void Update(GameTime gameTime)
        {
            gamePadState = GamePad.GetState(0);
            keyboardState = Keyboard.GetState();

            // Apply the gamepad movement with inverted Y axis
            position += gamePadState.ThumbSticks.Left * new Vector2(2, -2);
            if (gamePadState.ThumbSticks.Left.X < 0) flipped = true;
            if (gamePadState.ThumbSticks.Left.X > 0) flipped = false;

            // Apply keyboard movement
            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W)) position += new Vector2(0, -2);
            if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S)) position += new Vector2(0, 2);
            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                position += new Vector2(-2, 0);
                flipped = true;
            }
            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                position += new Vector2(2, 0);
                flipped = false;
            }

            // Update the bounds
            bounds.X = position.X;
            bounds.Y = position.Y;
        }

        /// <summary>
        /// Draws the sprite using the supplied SpriteBatch
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The spritebatch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //Update animation timer
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;

            // Update animation frame
            if (animationTimer > 0.1)
            {
                animationFrame++;
                if (animationFrame > 8) animationFrame = 0;
                animationTimer -= 0.1;
            }
            var source = new Rectangle(animationFrame * 120, 0, 120, 80);
            SpriteEffects spriteEffects = flipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A) || gamePadState.ThumbSticks.Left.X > 0)
            {
                spriteBatch.Draw(textureMoving, position, source, Color, 0, new Vector2(64, 64), 2f, spriteEffects, 0);
            }
            else if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D) || gamePadState.ThumbSticks.Left.X < 0)
            {
                spriteBatch.Draw(textureMoving, position, source, Color, 0, new Vector2(64, 64), 2f, spriteEffects, 0);
            }
            else if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
            {
                spriteBatch.Draw(textureMoving, position, source, Color, 0, new Vector2(64, 64), 2f, spriteEffects, 0);
            }
            else
            {
                spriteBatch.Draw(textureIdle, position, source, Color, 0, new Vector2(64, 64), 2f, spriteEffects, 0);
            }
        }
    }
}
