﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace BasicRPGScreen.Collisions
{
    /// <summary>
    /// A struct representing circular bounds
    /// </summary>
    public class BoundingCircle
    {
        /// <summary>
        /// The center of the BoundingCircle
        /// </summary>
        public Vector2 Center;

        /// <summary>
        /// The radius of the BoundingCircle
        /// </summary>
        public float Radius;

        /// <summary>
        /// Constructs a new BoundingCircle
        /// </summary>
        /// <param name="center">The center</param>
        /// <param name="radius">The radius-</param>
        public BoundingCircle(Vector2 center, float radius)
        {
            Center = center;
            Radius = radius;
        }

        /// <summary>
        /// Tests for a collision between this and another bounding circle
        /// </summary>
        /// <param name="other">The other bounding circle</param>
        /// <returns>true for collision, false otherwise</returns>
        public bool CollidesWith(BoundingCircle other)
        {
            return CollisionHelper.Collides(this, other);
        }

        /// <summary>
        /// Tests for a collision between this and a bounding rectangle
        /// </summary>
        /// <param name="other">The bounding rectangle</param>
        /// <returns>true for collision, false otherwise</returns>
        public bool CollidesWith(BoundingRectangle other)
        {
            return CollisionHelper.Collides(this, other);
        }
    }
}