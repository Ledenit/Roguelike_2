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
using SharpDX.MediaFoundation.DirectX;
#endregion

namespace Roguelike_2
{
    public class SpriteMoving : Sprite2d
    {
        public SpriteMoving(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Speed = 300.0f;
        }

        public void Update()
        {
            if (Input.Direction != Vector2.Zero)
            {
                var directoin = Vector2.Normalize(Input.Direction);
                Position += directoin * Speed * Global.TotalSeconds;
            }
        }
    }
}
