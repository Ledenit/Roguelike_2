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
    public class ShootingEnemy : Enemy
    {
        private Texture2D bulletTexture;
        private float shootCooldown;
        private float shootTime;

        public ShootingEnemy(Texture2D texture, Vector2 position, int hp, float speed, Texture2D bulletTexture, float shootCooldown)
            : base(texture, position, hp, speed)
        {
            this.bulletTexture = bulletTexture;
            this.shootCooldown = shootCooldown;
            this.shootTime = shootCooldown;
        }

        public override void Update(Player player, List<Enemy> enemies)
        {
            base.Update(player, enemies);

            shootTime -= Global.TotalSeconds;
            if (shootTime <= 0)
            {
                Shoot(player);
                shootTime = shootCooldown;
            }
        }

        private void Shoot(Player player)
        {
            var direction = Vector2.Normalize(player.Position - Position);
            var bulletPosition = Position + direction * (Texture.Width / 2);
            var enemyProjectileInfo = new ProjectilesInfo
            {
                Position = bulletPosition,
                Rotation = (float)Math.Atan2(direction.Y, direction.X),
                LifeTime = 2.0f,
                Speed = 300f,
                Damage = 0
            };

            ProjectileController.AddEnemyProjectile(enemyProjectileInfo);
        }
    }
}