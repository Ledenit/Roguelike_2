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
#endregion

namespace Roguelike_2
{
    public static class ExpController
    {
        private static Texture2D _texture;
        public static List<Experience> Experience { get; } = new();

        public static void Initialize(Texture2D texture)
        {
            _texture = texture;
        }

        public static void Reset()
        {
            Experience.Clear();
        }

        public static void AddExperience(Vector2 position)
        {
            Experience.Add(new(_texture, position));
        }

        public static void Update(Player player)
        {
            foreach (var e in Experience)
            {
                e.Update();

                if ((e.Position - player.Position).Length() < 50)
                {
                    e.Collect();
                    player.GetExperience(1);
                }
            }

            Experience.RemoveAll((e) => e.Lifespan <= 0);
        }

        public static void Draw()
        {
            foreach (var e in Experience)
            {
                e.Draw();
            }
        }
    }
}
