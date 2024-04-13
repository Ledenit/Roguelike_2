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
    public static class EnemyAI
    {
        public static List<Enemy> Enemies { get; } = new();

        public static List<Texture2D> _texture { get; } = new();

        private static float _spawnCooldown;
        private static float _spawnTime;
        private static Random _random;
    

        public static void Initialize(string name)
        {
            _texture.Add(Global.Content.Load<Texture2D>(name));
            _spawnCooldown = 1f;
            _spawnTime = _spawnCooldown;
            _random = new();
        }

        private static Vector2 RandomPosition()
        {
            float width = Global.Bounds.X;
            float height = Global.Bounds.Y;
            Vector2 position = new();

            position.X = (int)(_random.NextDouble() * height);
            position.Y = (int)(_random.NextDouble() * width);

            return position;
        }

        public static void AddEnemies()
        {
            foreach(Texture2D enemy in _texture)
                Enemies.Add(new(enemy, RandomPosition()));
        }

        public static void Update(Player player)
        {
            _spawnTime -= Global.TotalSeconds;
            if(_spawnTime <= 0) 
            {
                _spawnTime += _spawnCooldown;
                AddEnemies();
            }
            
            foreach(var e in Enemies)
            {
                e.Update(player);
            }
            Enemies.RemoveAll(z => z.HP <= 0);
        }

        public static void Draw()
        {
            foreach (var e in Enemies)
            {
                e.Draw();
            }
        }

    }
}
