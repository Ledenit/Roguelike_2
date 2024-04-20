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
    public class Projectiles : Sprite2d
    {
        public Vector2 Direction {  get; set; }

        public float LifeTime { get; set; }

        public int Damage { get; set; }

        public Projectiles(Texture2D tex, ProjectilesInfo info) : base(tex, info.Position)
        {
            Speed = info.Speed;
            Rotation = info.Rotation;
            Direction = new((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));
            LifeTime = info.LifeTime;
            Damage = info.Damage;
        }

        public void Destroy()
        {
            LifeTime = 0;
        }

        public void Update()
        {
            Position += Direction * Speed * Global.TotalSeconds;
            LifeTime -= Global.TotalSeconds;
        }

    }
}
