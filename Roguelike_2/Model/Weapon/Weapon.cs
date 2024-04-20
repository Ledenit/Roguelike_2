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
    public abstract class Weapon
    {
        protected float cooldown;
        protected float cooldownLeft;
        protected int maxAmmo;
        public int Ammo { get; protected set; }
        protected float reloadTime;
        public bool Reloading { get; protected set; }

        public Weapon()
        {
            cooldownLeft = 0f;
            Reloading = false;
        }

        public virtual void Reload()
        {
            if (Reloading) return;
            cooldownLeft = reloadTime;
            Reloading = true;
            Ammo = maxAmmo;
        }

        public abstract void CreateProjectiles(Player player);

        public virtual void Fire(Player player)
        {
            if (cooldownLeft > 0 || Reloading) return;

            Ammo--;
            if (Ammo > 0)
            {
                cooldownLeft = cooldown;
            }
            else Reload();

            CreateProjectiles(player);
        }

        public void Update()
        {
            if (cooldownLeft > 0)
                cooldownLeft -= Global.TotalSeconds;
            else if (Reloading)
                Reloading = false;
        }
    }     
}
