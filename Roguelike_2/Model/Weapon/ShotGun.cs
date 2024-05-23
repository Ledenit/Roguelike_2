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
    public class ShotGun : Weapon
    {
        public override void CreateProjectiles(Player player)
        {
            float angle = (float)(Math.PI / 16);

            ProjectilesInfo pd = new()
            {
                Position = player.Position,
                Rotation = player.Rotation - (3 * angle),
                LifeTime = 0.5f,
                Speed = 950,
                Damage = 2
            };

            for(int i=0; i < 5; i++)
            {
                pd.Rotation += angle;
                ProjectileController.AddProjectile(pd);
            }
        }

        public ShotGun()
        {
            cooldown = 0.75f;
            maxAmmo = 10;
            Ammo = maxAmmo;
            reloadTime = 4f;
        }
    }
}
