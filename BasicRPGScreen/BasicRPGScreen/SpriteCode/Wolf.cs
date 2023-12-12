using BasicRPGScreen.Collisions;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace BasicRPGScreen.SpriteCode
{
    public class Wolf : Enemy
    {
        private Texture2D textureIdle;
        private int idleFrames = 1;

        private Texture2D textureAttack;
        private int attackFrames = 6;

        private Texture2D textureDeath;
        private int deathFrames = 6;

        private double animationTimer;

        public short animationFrame = 0;

        private Vector2 position = new Vector2(400, 200);

        private bool flipped;

        private BoundingRectangle bounds = new BoundingRectangle(new Vector2(400, 200), 24, 24);

        /// <summary>
        /// The color blend with the ghost
        /// </summary>
        public Color Color { get; set; } = Color.White;

        /// <summary>
        /// The bounding volume of the sprite
        /// </summary>
        public BoundingRectangle Bounds => bounds;

        public (Texture2D, int) ActiveTexure { get; set; }

        public Wolf()
        {
            MaxHP = 12;
            CurrentHP = MaxHP;
            Damage = 5;
        }

        /// <summary>
        /// Loads the sprite texture using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            textureIdle = content.Load<Texture2D>("S_Death_Wolf");
            textureAttack = content.Load<Texture2D>("S_Attack_Wolf");
            textureDeath = content.Load<Texture2D>("S_Death_Wolf");
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
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, int animation)
        {
            if (animation == 0) ActiveTexure = (textureIdle, idleFrames);
            else if(animation == 1) ActiveTexure = (textureAttack, attackFrames);
            else ActiveTexure = (textureDeath, deathFrames);

            //Update animation timer
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;

            // Update animation frame
            if (animationTimer > 0.1)
            {
                animationFrame++;
                if (animationFrame > ActiveTexure.Item2) animationFrame = 0;
                animationTimer -= 0.1;
            }
            var source = new Rectangle(animationFrame * 48, 0, 48, 48);
            SpriteEffects spriteEffects = flipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            spriteBatch.Draw(ActiveTexure.Item1, position, source, Color, 0, new Vector2(24, 24), 2f, spriteEffects, 0);
        }
    }
}
