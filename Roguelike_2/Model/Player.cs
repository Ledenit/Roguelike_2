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

        public Player(Texture2D texture, Vector2 position, float Cooldown, float CooldownLeft) : base(texture, position)
        {
            Weapon = _shootGun;
        }

        //Переработать под большее количество оружия
        public void Swap()
        {
            Weapon = (Weapon==_shootGun) ? _AutomaticGun : _shootGun;
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

            Weapon.Update();

            if (Input.SpacePressed)
                Swap();

            if (Input.MouseLeftDown)
                Weapon.Fire(this);

            if (Input.MouseRightClicked)
                Weapon.Reload();
                
        }
    }
}
