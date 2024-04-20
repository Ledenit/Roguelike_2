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
    public class AutomaticGun : Weapon
    {
        public override void CreateProjectiles(Player player)
        {
            ProjectilesInfo pd = new()
            {
                Position = player.Position,
                Rotation = player.Rotation,
                LifeTime = 2,
                Speed = 600,
                Damage = 1
            };

            ProjectileController.AddProjectile(pd);
        }

        public AutomaticGun()
        {
            cooldown = 0.1f;
            maxAmmo = 30;
            Ammo = maxAmmo;
            reloadTime = 2f;
        }
    }
}
