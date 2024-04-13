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
using System.ComponentModel.Design.Serialization;
#endregion

namespace Roguelike_2
{
    public class Player : Sprite2d
    {
        private readonly float _cooldown;
        private float _cooldownLeft;
        private readonly int _maxAmmo;
        public int Ammo { get; private set; }
        private readonly float _reloadTime;
        public bool Reloading { get; private set; }

        public Player(Texture2D texture, Vector2 position, float Cooldown, float CooldownLeft) : base(texture, position)
        {
            _cooldown = Cooldown;
            _cooldownLeft = CooldownLeft;
            _maxAmmo = 30;
            Ammo = _maxAmmo;
            _reloadTime = 2f;
            Reloading = false;
        }

        private void Reload()
        {
            if (Reloading) return;
            _cooldownLeft = _reloadTime;
            Reloading = true;
            Ammo = _maxAmmo;
        }
        
        public void Fire()
        {
            if (_cooldownLeft > 0 || Reloading) return;

            Ammo--;
            if (Ammo > 0)
            {
                _cooldownLeft = _cooldown;
            }
            else Reload();

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
            if (_cooldownLeft > 0)
                _cooldownLeft -= Global.TotalSeconds;
            else if (Reloading)
                Reloading = false;

            if(Input.Direction != Vector2.Zero)
            {
                var directoin =  Vector2.Normalize(Input.Direction);
                Position += directoin * Speed * Global.TotalSeconds;
            }

            var toMouse = Input.MousePosition - Position;
            Rotation = (float)Math.Atan2(toMouse.Y, toMouse.X);

            if (Input.MouseLeftDown)
                Fire();
            if (Input.MouseRightClicked)
                Reload();
        }
    }
}
