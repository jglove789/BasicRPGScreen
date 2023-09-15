using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicRPGScreen.Collisions;
using Microsoft.Xna.Framework.Content;

namespace BasicRPGScreen
{
    public class SignSprite
    {
        private Vector2 _position;

        private Texture2D _texture;

        private string _text;

        private BoundingRectangle _bounds;

        /// <summary>
        /// The position of the sign
        /// </summary>
        public Vector2 Position => _position;

        /// <summary>
        /// The text written on the sign
        /// </summary>
        public string Text => _text;


        public bool ReadSign { get; set; } = false;

        /// <summary>
        /// The bounding voluem of the sprite
        /// </summary>
        public BoundingRectangle Bounds => _bounds;

        /// <summary>
        /// Creates a new sign sprite
        /// </summary>
        /// <param name="position">The position of the sprite in the game</param>
        /// <param name="text">The text within a sign</param>
        public SignSprite(Vector2 position, string text)
        {
            _position = position;
            _bounds = new BoundingRectangle(position.X+16, position.Y+32, 64, 40);
            _text = text;
        }

        /// <summary>
        /// Loads the sprite texture using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("SignSprite");
        }

        /// <summary>
        /// Draws the sprite using the supplied SpriteBatch
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The SpriteBatch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, null, Color.White, 0, new Vector2(), 2f, SpriteEffects.None, 0);
        }
    }
}
