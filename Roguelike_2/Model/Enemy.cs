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
#endregion

namespace Roguelike_2
{
    public class Enemy : Sprite2d
    {
        public int HP { get; private set; }

        public Enemy(Texture2D tex, Vector2 position) : base(tex, position)
        {
            Speed = 100;
            HP = 1;
        }

        public void TakeDamage(int damage)
        {
            HP -= damage;
        }

        public void Update(Player player)
        {
            var toPlayer = player.Position - Position;
            Rotation = (float)Math.Atan2(toPlayer.Y, toPlayer.X);

            if(toPlayer.Length() > 4)
            {
                var dir = Vector2.Normalize(toPlayer);
                Position += dir * Speed * Global.TotalSeconds;
            }
        }
    }
}
