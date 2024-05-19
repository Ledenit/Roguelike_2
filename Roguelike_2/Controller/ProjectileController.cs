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
    public static class ProjectileController
    {
        private static Texture2D _playerTexture;
        private static Texture2D _enemyTexture;

        public static List<Projectiles> Projectiles { get; } = new();

        public static void Initialize(Texture2D playerTexture, Texture2D enemyTexture)
        {
            _playerTexture = playerTexture;
            _enemyTexture = enemyTexture;
        }

        public static void Reset()
        {
            Projectiles.Clear();
        }

        public static void AddProjectile(ProjectilesInfo info)
        {
            Projectiles.Add(new(_playerTexture, info));
        }

        public static void AddEnemyProjectile(ProjectilesInfo info)
        {
            Projectiles.Add(new(_enemyTexture, info));
        }

        public static void Update(List<Enemy> enemies)
        {
            foreach (var p in Projectiles)
            {
                p.Update();
                foreach (var e in enemies)
                {
                    if (e.HP <= 0) continue;
                    if ((p.Position - e.Position).Length() < 30)
                    {
                        e.TakeDamage(p.Damage);
                        p.Destroy();
                        break;
                    }
                }
            }
            Projectiles.RemoveAll((p) => p.LifeTime <= 0);
        }

        public static void Draw()
        {
            foreach (var p in Projectiles)
            {
                p.Draw();
            }
        }
    }
}
