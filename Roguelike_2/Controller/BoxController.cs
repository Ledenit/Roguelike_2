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
using SharpDX.MediaFoundation.DirectX;
#endregion

namespace Roguelike_2
{
    public static class BoxController
    {
        private static Texture2D _texture;
        public static List<Box> Boxes { get; } = new List<Box>();

        private static float _spawnCooldown;
        private static float _spawnTime;
        private static Random _random = new Random();

        public static void Initialize(Texture2D texture, float spawnCooldown = 10f)
        {
            _texture = texture;
            _spawnCooldown = spawnCooldown;
            _spawnTime = _spawnCooldown;
        }

        private static Vector2 RandomPosition()
        {
            float width = Global.Bounds.X;
            float height = Global.Bounds.Y;
            Vector2 position = new Vector2((float)(_random.NextDouble() * width), (float)(_random.NextDouble() * height));
            return position;
        }

        public static void AddBox()
        {
            Boxes.Add(new Box(_texture, RandomPosition()));
        }

        public static void Reset()
        {
            Boxes.Clear();
            _spawnTime = _spawnCooldown;
        }

        public static void Update()
        {
            _spawnTime -= Global.TotalSeconds;
            if (_spawnTime <= 0)
            {
                _spawnTime = _spawnCooldown;
                AddBox();
            }

            foreach (var box in Boxes)
            {
                box.Update();
            }

            Boxes.RemoveAll(b => b.IsDestroyed);
        }

        public static void Draw()
        {
            foreach (var box in Boxes)
            {
                box.Draw();
            }
        }
    }
}