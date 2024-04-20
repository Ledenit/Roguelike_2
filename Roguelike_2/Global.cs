#region using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;
#endregion

namespace Roguelike_2
{
    public static class Global
    {
        public static float TotalSeconds { get; set; }
        public static ContentManager Content { get; set; }
        public static SpriteBatch SpriteBatch { get; set; }

        public static Point Bounds { get; set; }

        public static void Update(GameTime gameTime)
        {
            TotalSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public static double GetDistance(Vector2 start, Vector2 end)
        {
            var distance = Math.Sqrt(Math.Pow(end.X - start.X,2) + Math.Pow(end.Y - start.Y, 2));
            return distance;
        }
    }
}
