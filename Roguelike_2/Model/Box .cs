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
using SharpDX.MediaFoundation.DirectX;
#endregion

namespace Roguelike_2
{
    public class Box : SpriteMoving
    {
        public bool IsDestroyed { get; private set; }
        private static Random random = new Random();

        public Box(Texture2D texture, Vector2 position) : base(texture, position)
        {
            IsDestroyed = false;
        }

        public void Break()
        {
            IsDestroyed = true;
            DropHeart();
        }

        private void DropHeart()
        {
            HeartController.AddHeart(Position);
        }

        public new void Update()
        {
            base.Update();
        }
    }
}