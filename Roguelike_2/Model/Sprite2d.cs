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
using static System.Net.Mime.MediaTypeNames;
#endregion

namespace Roguelike_2
{
    public class Sprite2d
    {
        protected readonly Texture2D texture;
        protected readonly Vector2 origin;

        public Vector2 Position { get; set; }
        public float Speed { get; set; }
        public float Rotation { get; set; }

        public float Scale { get; set; }

        public Sprite2d(Texture2D tex, Vector2 position)
        {
            texture = tex;
            Position = position;
            Speed = 300.0f;
            Scale = 1f;
            origin = new(tex.Width / 2, tex.Height / 2);
        }

        public virtual void Draw()
        {
            Global.SpriteBatch.Draw(texture,
                Position,
                null,
                Color.White,
                Rotation,
                origin,
                Scale,
                SpriteEffects.None,
                1);
        }
    }
}
