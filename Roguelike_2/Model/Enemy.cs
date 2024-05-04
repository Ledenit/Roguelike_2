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
    public class Enemy : Sprite2d
    {
        public int HP { get; private set; }
        private List<Vector2> obstacles;

        public Enemy(Texture2D tex, Vector2 position) : base(tex, position)
        {
            Speed = 100;
            HP = 2;
            obstacles = new List<Vector2>();
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

        public void Update(Player player, List<Enemy> enemies)
        {
            var toPlayer = player.Position - Position;
            Rotation = (float)Math.Atan2(toPlayer.Y, toPlayer.X);

            obstacles.Clear();
            obstacles.Add(player.Position);

            foreach (var enemy in enemies)
            {
                if (enemy.Position != Position)
                    obstacles.Add(enemy.Position);
            }

            AvoidObstacles();

            if (toPlayer.Length() > 50)
            {
                var dir = Vector2.Normalize(toPlayer);
                Position += dir * Speed * Global.TotalSeconds;
            }
        }

        private void AvoidObstacles()
        {
            foreach (var obstacle in obstacles)
            {
                var toObstacle = obstacle - Position;
                var distance = toObstacle.Length();

                if (distance < 50)
                {
                    var avoidanceDir = Vector2.Normalize(Position - obstacle);
                    Position += avoidanceDir * Speed * Global.TotalSeconds;
                }
            }
        }
    }
}
