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
    public class Enemy : SpriteMoving
    {
        public int HP { get; private set; }
        private int maxHP;
        private List<Rectangle> obstacles;
        public Texture2D Texture { get; private set; }
        public float EnemySpeed { get; set; }

        public Enemy(Texture2D texture, Vector2 position, int hp, float speed) : base(texture, position)
        {
            EnemySpeed = speed;
            Texture = texture;
            HP = hp;
            maxHP = hp;
            obstacles = new List<Rectangle>();
        }

        public void TakeDamage(int damage)
        {
            HP -= damage;
            if (HP <= 0) 
                ExpController.AddExperience(Position);
        }

        public void ResetHP()
        {
            HP = 0;
            ExpController.AddExperience(Position);
        }

        public virtual void Update(Player player, List<Enemy> enemies)
        {
            Update();

            var toPlayer = player.Position - Position;
            Rotation = (float)Math.Atan2(toPlayer.Y, toPlayer.X);

            obstacles.Clear();
            obstacles.Add(player.Bounds);

            foreach (var enemy in enemies)
            {
                if (enemy != this)
                    obstacles.Add(enemy.Bounds);
            }

            AvoidObstacles();

            if (toPlayer.Length() > 50)
            {
                var dir = Vector2.Normalize(toPlayer);
                Position += dir * EnemySpeed * Global.TotalSeconds;
            }
        }

        private void AvoidObstacles()
        {
            foreach (var obstacle in obstacles)
            {
                if (Bounds.Intersects(obstacle))
                {
                    var obstacleCenter = new Vector2(obstacle.X + obstacle.Width / 2, obstacle.Y + obstacle.Height / 2);
                    var toObstacle = -obstacleCenter + Bounds.Center.ToVector2();
                    var avoidanceDir = Vector2.Normalize(toObstacle);
                    Position += avoidanceDir * EnemySpeed * Global.TotalSeconds;
                }
            }
        }

        public void DrawHealthBar(SpriteBatch spriteBatch)
        {
            if (HP >= maxHP) return;

            int barWidth = 50;
            int barHeight = 5;
            Vector2 barPosition = new Vector2(Position.X - barWidth / 2, Position.Y - Texture.Height / 2 - barHeight - 5);

            float healthPercentage = (float)HP / maxHP;

            Texture2D healthBarBackground = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            healthBarBackground.SetData(new[] { Color.Gray });

            Texture2D healthBarForeground = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            healthBarForeground.SetData(new[] { Color.Red });

            spriteBatch.Draw(healthBarBackground, new Rectangle((int)barPosition.X, (int)barPosition.Y, barWidth, barHeight), Color.White);
            spriteBatch.Draw(healthBarForeground, new Rectangle((int)barPosition.X, (int)barPosition.Y, (int)(barWidth * healthPercentage), barHeight), Color.White);
        }
    }
}
