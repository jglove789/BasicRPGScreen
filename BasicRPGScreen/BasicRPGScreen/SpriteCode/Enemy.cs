using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicRPGScreen.SpriteCode
{
    public abstract class Enemy
    {
        public int MaxHP;
        public int CurrentHP;
        public int Damage;

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch, int animation, Vector2 location);
    }
}
