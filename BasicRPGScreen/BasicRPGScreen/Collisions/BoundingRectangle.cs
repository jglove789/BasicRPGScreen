using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace BasicRPGScreen.Collisions
{
    /// <summary>
    /// A struct representing rectangular bounds
    /// </summary>
    public struct BoundingRectangle
    {
        /// <summary>
        /// The bounding rectangle's x position
        /// </summary>
        public float X;

        /// <summary>
        /// The bounding rectangle's y position
        /// </summary>
        public float Y;

        /// <summary>
        /// The bounding rectangle's width
        /// </summary>
        public float Width;

        /// <summary>
        /// The bounding rectangle's height
        /// </summary>
        public float Height;


        public float Left => X;

        public float Right => X + Width;

        public float Top => Y;

        public float Bottom => Y + Height;

        /// <summary>
        /// Constructs a new BoundingRectangle
        /// </summary>
        /// <param name="x">X position of the rectangle</param>
        /// <param name="y">Y position of the rectangle</param>
        /// <param name="width">Width of the rectangle</param>
        /// <param name="height">Height of the rectangle</param>
        public BoundingRectangle(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Constructs a new Bounding Rectangle
        /// </summary>
        /// <param name="position">The position of the rectangle</param>
        /// <param name="width">Width of the rectangle</param>
        /// <param name="height">Height of the rectangle</param>
        public BoundingRectangle(Vector2 position, float width, float height)
        {
            X = position.X;
            Y = position.Y;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Tests for a collision between this and another bounding rectangle
        /// </summary>
        /// <param name="other">The other bounding rectangle</param>
        /// <returns>true for collision, false otherwise</returns>
        public bool CollidesWith(BoundingRectangle other)
        {
            return CollisionHelper.Collides(this, other);
        }

        /// <summary>
        /// Tests for a collision between this and a bounding circle
        /// </summary>
        /// <param name="other">The bounding circle</param>
        /// <returns>ture for collision, false otherwise</returns>
        public bool CollidesWith(BoundingCircle other)
        {
            return CollisionHelper.Collides(other, this);
        }
    }
}
