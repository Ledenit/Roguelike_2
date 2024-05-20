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
        private bool _monster2Spawned = false;
        private bool _monster3Spawned = false;
        private bool _monster4Spawned = false;
        public WorldController()
        {
            Global.IsPlayerDead = false;
            _background = new(Global.Content.Load<Texture2D>("background"), new(Global.Bounds.X / 2, Global.Bounds.Y / 2));
            _player = new(Global.Content.Load<Texture2D>("hero-pixel"), new(Global.Bounds.X / 2, Global.Bounds.Y / 2));
            var bullet = Global.Content.Load<Texture2D>("bullet");
            var box = Global.Content.Load<Texture2D>("box");
            var hp = Global.Content.Load<Texture2D>("HP");
            var exp = Global.Content.Load<Texture2D>("exp");
            ExpController.Initialize(exp);
            BoxController.Initialize(box);
            HeartController.Initialize(hp);
            ProjectileController.Initialize(bullet);
            UIController.Initialize(bullet, hp, exp);
            EnemyAI.Initialize(_player.Position);
            EnemyAI.Initialize("mob1", 2, 100, 2f);
            EnemyAI.AddEnemies();
        }

        public void Restart()
        {
            Global.IsPlayerDead = false;
            ProjectileController.Reset();
            BoxController.Reset();
            HeartController.Reset();
            ExpController.Reset();
            EnemyAI.Reset();
            _player.Reset();
            _background.Reset();
        }

        public void Update()
        {
            Input.Update();
            _background.Update();
            _player.Update(EnemyAI.Enemies, BoxController.Boxes);
            UIController.Update(_player);
            ProjectileController.Update(EnemyAI.Enemies, BoxController.Boxes);
            BoxController.Update();
            HeartController.Update(_player);
            ExpController.Update(_player);
            EnemyAI.Update(_player);

            //if (_player.Experience > 10)
            //{
            //    var textureToRemove = Global.Content.Load<Texture2D>("mob");
            //    EnemyAI.RemoveEnemies(textureToRemove);
            //}

            if (_player.Experience >= 20 && !_monster2Spawned)
            {
                EnemyAI.Initialize("mob2", 1, 220, 2.5f);
                EnemyAI.AddEnemies();

                _player.MaxHP += 1;
                _player.HP = _player.MaxHP;
                _monster2Spawned = true; 
            }

            if (_player.Experience >= 30 && !_monster3Spawned)
            {
                EnemyAI.Initialize("mob4", 3, 190, 3f);
                EnemyAI.AddEnemies();

                _player.MaxHP += 1;
                _player.HP = _player.MaxHP;
                _monster3Spawned = true;
            }

            if (_player.Experience >= 40 && !_monster4Spawned)
            {
                
                EnemyAI.Initialize("mob3", 5, 60, 4f);
                EnemyAI.AddEnemies();

                _player.MaxHP += 1;
                _player.HP = _player.MaxHP;
                _monster4Spawned = true;
            }


            if (_player.Dead) Global.IsPlayerDead=true;
        }

        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            _background.Draw();
            _player.Draw();
            ProjectileController.Draw();
            BoxController.Draw();
            HeartController.Draw();
            ExpController.Draw();
            EnemyAI.Draw(spriteBatch);
            UIController.Draw(_player);
        }
    }
}
