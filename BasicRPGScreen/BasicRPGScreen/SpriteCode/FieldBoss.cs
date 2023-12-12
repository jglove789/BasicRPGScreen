using BasicRPGScreen.Collisions;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicRPGScreen.SpriteCode
{
    public class FieldBoss : Enemy
    {
        private Texture2D textureIdle;
        private int idleFrames = 1;

        private Texture2D textureAttack;
        private int attackFrames = 6;

        private Texture2D textureDeath;
        private int deathFrames = 6;

        private double animationTimer;

        public short animationFrame = 0;

        private Vector2 position;

        private bool flipped;

        private BoundingRectangle bounds;

        /// <summary>
        /// The color blend with the ghost
        /// </summary>
        public Color Color { get; set; } = Color.White;

        /// <summary>
        /// The bounding volume of the sprite
        /// </summary>
        public BoundingRectangle Bounds => bounds;

        public (Texture2D, int) ActiveTexure { get; set; }

        public FieldBoss(Vector2 location)
        {
            MaxHP = 50;
            CurrentHP = MaxHP;
            Damage = 10;

            position = location;
            bounds = new BoundingRectangle(location, 96, 96);
        }

        /// <summary>
        /// Loads the sprite texture using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            textureIdle = content.Load<Texture2D>("Idle_Boss");
            textureAttack = content.Load<Texture2D>("Attack1_Boss");
            textureDeath = content.Load<Texture2D>("Death_Boss");
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
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, int animation, Vector2 location)
        {
            if (animation == 0) ActiveTexure = (textureIdle, idleFrames);
            else if (animation == 1) ActiveTexure = (textureAttack, attackFrames);
            else ActiveTexure = (textureDeath, deathFrames);

            position = location;
            bounds = new BoundingRectangle(location, 24, 24);
            //Update animation timer
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;

            // Update animation frame
            if (animationTimer > 0.1)
            {
                animationFrame++;
                if (animationFrame > ActiveTexure.Item2) animationFrame = 0;
                animationTimer -= 0.1;
            }
            var source = new Rectangle(animationFrame * 96, 0, 96, 96);
            SpriteEffects spriteEffects = flipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            spriteBatch.Draw(ActiveTexure.Item1, position, source, Color, 0, new Vector2(24, 24), 2f, spriteEffects, 0);
        }
    }
}
