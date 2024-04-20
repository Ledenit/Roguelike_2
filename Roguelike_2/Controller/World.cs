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
    public class World
    {
        private readonly Player _player;
        public World()
        {
            _player = new(Global.Content.Load<Texture2D>("hero"), new(Global.Bounds.X / 2, Global.Bounds.Y / 2), 0.25f, 0f);
            var bullet = Global.Content.Load<Texture2D>("bullet");
            var hp = Global.Content.Load<Texture2D>("HP");
            ProjectileController.Initialize(bullet);
            UIController.Initialize(bullet, hp);
            EnemyAI.Initialize(_player.Position);
            EnemyAI.Initialize("mob");
            EnemyAI.AddEnemies();
            EnemyAI.Initialize("hero");
            EnemyAI.AddEnemies();
        }

        public void Restart()
        {
            ProjectileController.Reset();
            EnemyAI.Reset();
            _player.Reset();
        }

        public void Update()
        {
            Input.Update();
            _player.Update(EnemyAI.Enemies);
            ProjectileController.Update(EnemyAI.Enemies);
            EnemyAI.Update(_player);

            if (_player.Dead) Restart();
        }

        public void Draw()
        {
            _player.Draw();
            ProjectileController.Draw();
            EnemyAI.Draw();
            UIController.Draw(_player);
        }

    }
}
