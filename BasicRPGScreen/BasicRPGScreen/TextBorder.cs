using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicRPGScreen
{
    public class TextBorder
    {
        private Vector2 _position = new(160, 480);

        private Texture2D _texture;

        public void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("TextBox");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }
    }
}
