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
        public Weapon Weapon { get; set; }
        private Weapon _shootGun = new ShotGun();
        private Weapon _AutomaticGun = new AutomaticGun();
        public bool Dead { get; private set; }
        public int HP { get; private set; }

        public Player(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Weapon = _shootGun;
            HP = 3;
        }

        public void Reset()
        {
            HP = 3;
            _shootGun = new ShotGun();
            _AutomaticGun = new AutomaticGun();
            Dead = false;
            Weapon = _shootGun;
            Position = new(Global.Bounds.X / 2, Global.Bounds.Y / 2); ;
        }

        //Переработать под большее количество оружия
        public void Swap()
        {
            Weapon = (Weapon==_shootGun) ? _AutomaticGun : _shootGun;
        }

        private void CheckDeath(List<Enemy> Enemies)
        {
            foreach (var e in Enemies)
            {
                if (e.HP <= 0) continue;
                if ((Position - e.Position).Length() < 50)
                {
                    HP--;
                    e.ResetHP();
                }
            }

            if (HP == 0) Dead = true;
        }

        public void Update(List<Enemy> Enemies)
        {
            if(Input.Direction != Vector2.Zero)
            {
                var directoin =  Vector2.Normalize(Input.Direction);
                Position += directoin * Speed * Global.TotalSeconds;
            }

            var toMouse = Input.MousePosition - Position;
            Rotation = (float)Math.Atan2(toMouse.Y, toMouse.X);

            Weapon.Update();

            if (Input.SpacePressed)
                Swap();

            if (Input.MouseRightDown)
                Weapon.Fire(this);

            if (Input.MouseLeftClicked)
                Weapon.Fire(this);

            if (Input.RPressed)
                Weapon.Reload();

            CheckDeath(Enemies);
        }
    }
}
