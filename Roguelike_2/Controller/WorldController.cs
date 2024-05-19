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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;
using SharpDX.Direct2D1;
#endregion

namespace Roguelike_2
{
    public class WorldController
    {
        private readonly Player _player;
        private readonly Background _background;
        public WorldController()
        {
            Global.IsPlayerDead = false;
            _background = new(Global.Content.Load<Texture2D>("background"), new(Global.Bounds.X / 2, Global.Bounds.Y / 2));
            _player = new(Global.Content.Load<Texture2D>("hero"), new(Global.Bounds.X / 2, Global.Bounds.Y / 2));
            var bullet = Global.Content.Load<Texture2D>("bullet");
            var hp = Global.Content.Load<Texture2D>("HP");
            var exp = Global.Content.Load<Texture2D>("exp");
            ExpController.Initialize(exp);
            ProjectileController.Initialize(bullet);
            UIController.Initialize(bullet, hp, exp);
            EnemyAI.Initialize(_player.Position);
            EnemyAI.Initialize("mob", 2, 100);
            EnemyAI.Initialize("hero", 1, 150);
            EnemyAI.AddEnemies();
        }

        public void Restart()
        {
            Global.IsPlayerDead = false;
            ProjectileController.Reset();
            ExpController.Reset();
            EnemyAI.Reset();
            _player.Reset();
            _background.Reset();
        }

        public void Update()
        {
            Input.Update();
            _background.Update();
            _player.Update(EnemyAI.Enemies);
            UIController.Update(_player);
            ProjectileController.Update(EnemyAI.Enemies);
            ExpController.Update(_player);
            EnemyAI.Update(_player);

            if (_player.Experience > 10)
            {
                var textureToRemove = Global.Content.Load<Texture2D>("mob");
                EnemyAI.RemoveEnemies(textureToRemove);
            }

            if (_player.Dead) Global.IsPlayerDead=true;
        }

        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            _background.Draw();
            _player.Draw();
            ProjectileController.Draw();
            ExpController.Draw();
            EnemyAI.Draw(spriteBatch);
            UIController.Draw(_player);
        }
    }
}
