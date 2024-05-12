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
using SharpDX.Direct3D9;
using SharpDX.Direct2D1.Effects;
#endregion

namespace Roguelike_2
{
    public class Experience : SpriteMoving
    {
        public float Lifespan { get; private set; }
        private const float LIFE = 5f;

        public Experience(Texture2D tex, Vector2 position) : base(tex, position)
        {
            Lifespan = LIFE;
        }

        public new void Update()
        {
            base.Update();
            Lifespan -= Global.TotalSeconds;
            Scale = 0.33f + (Lifespan / LIFE * 0.66f);
        }

        public void Collect()
        {
            Lifespan = 0;
        }
    }
}
