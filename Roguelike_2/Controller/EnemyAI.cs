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
using SharpDX.Direct2D1;
#endregion

namespace Roguelike_2
{
    public static class EnemyAI
    {
        public static List<Enemy> Enemies { get; } = new();
        private static List<Texture2D> _textures = new();
        private static List<(int HP, float Speed)> _enemyAttributes = new();
        private static List<Texture2D> _bulletTextures = new();
        private static List<float> _shootCooldowns = new();

        private static float _spawnCooldown;
        private static float _spawnTime;
        private static Random _random;
        private static Vector2 _playerPosition;

        public static void Initialize(Vector2 playerPosition)
        {
            _playerPosition = playerPosition;
            _spawnCooldown = 1.5f;
            _spawnTime = _spawnCooldown;
            _random = new();
        }

        public static void AddEnemyType(string enemyTextureName, int hp, float speed, string bulletTextureName = null, float shootCooldown = 0)
        {
            var texture = Global.Content.Load<Texture2D>(enemyTextureName);
            _textures.Add(texture);
            _enemyAttributes.Add((hp, speed));

            if (!string.IsNullOrEmpty(bulletTextureName))
            {
                var bulletTexture = Global.Content.Load<Texture2D>(bulletTextureName);
                _bulletTextures.Add(bulletTexture);
                _shootCooldowns.Add(shootCooldown);
            }
            else
            {
                _bulletTextures.Add(null);
                _shootCooldowns.Add(0);
            }
        }

        private static Vector2 RandomPosition()
        {
            float width = Global.Bounds.X;
            float height = Global.Bounds.Y;
            Vector2 position = new();

            position.X = (int)(_random.NextDouble() * height);
            position.Y = (int)(_random.NextDouble() * width);

            if (Global.GetDistance(position, _playerPosition) < 100)
            {
                position.X -= 500;
                position.Y -= 500;
            }

            return position;
        }

        public static void AddEnemies()
        {
            for (int i = 0; i < _textures.Count; i++)
            {
                var texture = _textures[i];
                var (hp, speed) = _enemyAttributes[i];
                var bulletTexture = _bulletTextures[i];
                var shootCooldown = _shootCooldowns[i];

                if (bulletTexture != null)
                {
                    Enemies.Add(new ShootingEnemy(texture, RandomPosition(), hp, speed, bulletTexture, shootCooldown));
                }
                else
                {
                    Enemies.Add(new Enemy(texture, RandomPosition(), hp, speed));
                }
            }
        }

        public static void RemoveEnemies(Texture2D texture)
        {
            Enemies.RemoveAll(e => e.Texture == texture);
        }

        public static void Reset()
        {
            Enemies.Clear();
            _spawnTime = _spawnCooldown;
        }

        public static void Update(Player player)
        {
            _spawnTime -= Global.TotalSeconds;
            if (_spawnTime <= 0)
            {
                _spawnTime += _spawnCooldown;
                AddEnemies();
            }

            foreach (var e in Enemies)
            {
                e.Update(player, Enemies);
            }
            Enemies.RemoveAll(e => e.HP <= 0);
            Enemies.RemoveAll(e => e.Position.X <= -100);
            Enemies.RemoveAll(e => e.Position.X >= Global.Bounds.X + 100);
            Enemies.RemoveAll(e => e.Position.Y <= -100);
            Enemies.RemoveAll(e => e.Position.Y >= Global.Bounds.Y + 100);
        }

        public static void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            foreach (var e in Enemies)
            {
                e.Draw();
                e.DrawHealthBar(spriteBatch);
            }
        }
    }
}
