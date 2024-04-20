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
    public static class UIController
    {
        private static Texture2D _bullet;

        public static void Initialize(Texture2D texture)
        {
            _bullet = texture;
        }

        public static void Draw(Player player)
        {
            Color color = player.Weapon.Reloading ? Color.Red : Color.White;

            for (int i=0; i< player.Weapon.Ammo; i++)
            {
                Vector2 position = new(0, i * _bullet.Height * 2);
                Global.SpriteBatch.Draw(
                    _bullet, 
                    position, 
                    null, 
                    color * 0.75f,
                    0,
                    Vector2.Zero, 
                    2,
                    SpriteEffects.None, 
                    1);
            }
        }

    }
}
