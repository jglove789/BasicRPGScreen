using BasicRPGScreen.Collisions;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicRPGScreen.SpriteCode
{
    public class Goblin
    {
        private Texture2D textureIdle;

        private Texture2D textureAttack;

        private Texture2D textureDeath;

        private double animationTimer;

        public short animationFrame = 0;

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
            textureIdle = content.Load<Texture2D>("S_Death_Goblin");
            textureAttack = content.Load<Texture2D>("S_Attack_Goblin");
            textureDeath = content.Load<Texture2D>("S_Death_Goblin");
        }

        /// <summary>
        /// Updates the sprite's position based on user input
        /// </summary>
        /// <param name="gameTime">The GameTime</param>
        public void Update(GameTime gameTime)
        {
            
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
            spriteBatch.Draw(textureIdle, position, source, Color, 0, new Vector2(64, 64), 2f, spriteEffects, 0);
        }
    }
}
