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
using SharpDX.Direct3D9;
#endregion

namespace Roguelike_2
{
    public static class UIController
    {
        private static Texture2D _bullet;
        private static Texture2D _HP;
        private static Texture2D _experience;
        private static Vector2 _textPosition;
        private static Vector2 _expPosition;
        private static string _playerExp;
        private static SpriteFont _font;

        public static void Initialize(Texture2D bullet, Texture2D hp, Texture2D experience)
        {
            _bullet = bullet;
            _HP = hp;
            _experience = experience;
            _font = Global.Content.Load<SpriteFont>("font");
            _expPosition = new(Global.Bounds.X - (2 * _experience.Width), 0);
            _playerExp = "";
        }

        public static void Update(Player player)
        {
            _playerExp = player.Experience.ToString();
            var x = _font.MeasureString(_playerExp).X / 2;
            _textPosition = new(Global.Bounds.X - x - 32, 14);
        }

        public static void Draw(Player player)
        {
            Color colorBullet = player.Weapon.Reloading ? Color.Red : Color.White;

            for (int i=0; i< player.Weapon.Ammo; i++)
            {
                Vector2 position = new(0, i * _bullet.Height * 2);
                Global.SpriteBatch.Draw(
                    _bullet, 
                    position, 
                    null,
                    colorBullet * 0.75f,
                    0,
                    Vector2.Zero, 
                    2,
                    SpriteEffects.None, 
                    1);
            }

            for (int i = 0; i < player.HP; i++)
            {
                Vector2 position = new(player.Position.X - _HP.Width * i, player.Position.Y - 30 * 2 );
                Global.SpriteBatch.Draw(
                    _HP,
                    position,
                    null,
                    Color.White,
                    0,
                    Vector2.Zero,
                    2,
                    SpriteEffects.None,
                    1);
            }

            Global.SpriteBatch.Draw(_experience, _expPosition, null, Color.White * 0.75f, 0f, Vector2.Zero, 2f, SpriteEffects.None, 1f);
            Global.SpriteBatch.DrawString(_font, _playerExp, _textPosition, Color.White);
        }
    }
}
