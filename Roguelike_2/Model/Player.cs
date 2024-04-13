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
    public class Player : Sprite2d
    {
        public Player(Texture2D texture, Vector2 position) : base(texture, position)
        {
        }

        public void Fire()
        {
            ProjectilesInfo pd = new()
            {
                Position = Position,
                Rotation = Rotation,
                LifeTime = 2,
                Speed = 600
            };

            ProjectileController.AddProjectile(pd);
            
        }

        public void Update()
        {
            if(Input.Direction != Vector2.Zero)
            {
                var directoin =  Vector2.Normalize(Input.Direction);
                Position += directoin * Speed * Global.TotalSeconds;
            }

            var toMouse = Input.MousePosition - Position;
            Rotation = (float)Math.Atan2(toMouse.Y, toMouse.X);

            if (Input.MouseClicked)
                Fire();
        }
    }
}
