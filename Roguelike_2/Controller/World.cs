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
            ProjectileController.Initialize();
            _player = new(Global.Content.Load<Texture2D>("hero"), new(50, 50));
        }

        public void Update()
        {
            Input.Update();
            _player.Update();
            ProjectileController.Update();
        }

        public void Draw()
        {
            _player.Draw();
            ProjectileController.Draw();
        }

    }
}
