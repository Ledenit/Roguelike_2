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
using SharpDX.Direct3D9;
#endregion

namespace Roguelike_2
{
    public class Player : Sprite2d
    {
        public Weapon Weapon { get; set; }
        private Weapon _shootGun = new ShotGun();
        private Weapon _AutomaticGun = new AutomaticGun();
        public bool Dead { get; private set; }
        public int HP { get; set; }
        public int MaxHP { get; set; }
        public int Experience { get; private set; }

        public Player(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Weapon = _shootGun;
            HP = 3;
            MaxHP = 3;
            Experience = 0;
        }

        public void Reset()
        {
            HP = 3;
            _shootGun = new ShotGun();
            _AutomaticGun = new AutomaticGun();
            Dead = false;
            Weapon = _shootGun;
            Position = new(Global.Bounds.X / 2, Global.Bounds.Y / 2);
            Experience = 0;
        }

        public void Swap()
        {
            Weapon = (Weapon==_shootGun) ? _AutomaticGun : _shootGun;
        }

        private void CheckDeath(List<Enemy> Enemies)
        {
            foreach (var e in Enemies)
            {
                if (e.HP <= 0) continue;
                if (Bounds.Intersects(e.Bounds))
                {
                    HP--;
                    e.ResetHP();
                }
            }

            if (HP == 0) Dead = true;
        }

        public void GetExperience(int experience)
        {
            Experience += experience;
        }

        public void GetHP(int hp)
        {
            HP += hp;
        }

        public void Update(List<Enemy> Enemies, List<Box> Boxes)
        {
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

            Vector2 movementDirection = Vector2.Zero;
            if (Input.Direction != Vector2.Zero)
            {
                movementDirection = Vector2.Normalize(Input.Direction);
            }

            Vector2 movement = movementDirection * Speed * Global.TotalSeconds;

            foreach (var box in Boxes)
            {
                if (Bounds.Intersects(box.Bounds))
                {
                    Vector2 penetrationVector = CalculatePenetrationVector(box);
                    movement -= penetrationVector;
                }
            }

            foreach (var movable in SpriteMoving.AllMovables)
            {
                movable.Position -= movement;
            }
        }

        private Vector2 CalculatePenetrationVector(Box box)
        {
            Rectangle playerBounds = this.Bounds;
            Rectangle boxBounds = box.Bounds;

            float overlapLeft = playerBounds.Right - boxBounds.Left;
            float overlapRight = boxBounds.Right - playerBounds.Left;
            float overlapTop = playerBounds.Bottom - boxBounds.Top;
            float overlapBottom = boxBounds.Bottom - playerBounds.Top;

            float minOverlapX = (overlapLeft < overlapRight) ? overlapLeft : -overlapRight;
            float minOverlapY = (overlapTop < overlapBottom) ? overlapTop : -overlapBottom;

            if (Math.Abs(minOverlapX) < Math.Abs(minOverlapY))
            {
                return new Vector2(minOverlapX, 0);
            }
            else
            {
                return new Vector2(0, minOverlapY);
            }
        }
    }
}
