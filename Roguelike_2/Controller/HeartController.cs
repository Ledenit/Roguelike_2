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
    public static class HeartController
    {
        private static Texture2D _texture;
        public static List<Heart> Hearts { get; } = new List<Heart>();

        public static void Initialize(Texture2D texture)
        {
            _texture = texture;
        }

        public static void AddHeart(Vector2 position)
        {
            Hearts.Add(new Heart(_texture, position));
        }

        public static void Reset()
        {
            Hearts.Clear();
        }

        public static void Update(Player player)
        {
            foreach (var heart in Hearts)
            {
                heart.Update();
                if ((heart.Position - player.Position).Length() < 50)
                {
                    heart.Collect(player);
                }
            }

            Hearts.RemoveAll(h => (h.Position - player.Position).Length() < 50);
            Hearts.RemoveAll(h => h.Position.X <= -100);
            Hearts.RemoveAll(h => h.Position.X >= Global.Bounds.X + 100);
            Hearts.RemoveAll(h => h.Position.Y <= -100);
            Hearts.RemoveAll(h => h.Position.Y >= Global.Bounds.Y + 100);
        }

        public static void Draw()
        {
            foreach (var heart in Hearts)
            {
                heart.Draw();
            }
        }
    }
}