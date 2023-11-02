using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicRPGScreen.Collisions;
using Microsoft.Xna.Framework.Content;

namespace BasicRPGScreen.SpriteCode
{
    public class WoodenDoorSprite
    {
        private Vector2 _position;

        private Texture2D _texture;

        private BoundingRectangle _bounds;

        /// <summary>
        /// The bounding voluem of the sprite
        /// </summary>
        public BoundingRectangle Bounds => _bounds;

        /// <summary>
        /// Creates a new wooden door sprite
        /// </summary>
        /// <param name="position">The position of the door in the game</param>
        public WoodenDoorSprite(Vector2 position)
        {
            _position = position;
            _bounds = new BoundingRectangle(position.X, position.Y, 64, 64);
        }

        /// <summary>
        /// Loads the sprite texture using the provided ContentManager
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("WoodenDoor");
        }


        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, null, Color.White, 0, new Vector2(), 2f, SpriteEffects.None, 0);
        }
    }
}
