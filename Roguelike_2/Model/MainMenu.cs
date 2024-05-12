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
    public class MainMenu
    {
        private Texture2D startButtonTexture;
        private Texture2D exitButtonTexture;
        private Rectangle startButtonRectangle;
        private Rectangle exitButtonRectangle;
        public MainMenu()
        {
            startButtonTexture = Global.Content.Load<Texture2D>("Button");
            exitButtonTexture = Global.Content.Load<Texture2D>("Button");

            startButtonRectangle = new Rectangle(
                (Global.Bounds.X - startButtonTexture.Width * 2) / 2,
                (200 + startButtonTexture.Height * 2) / 2,
                startButtonTexture.Width * 2,
                startButtonTexture.Height * 2);

 
            exitButtonRectangle = new Rectangle(
                (Global.Bounds.X - exitButtonTexture.Width * 2) / 2,
                startButtonRectangle.Bottom + 20,
                exitButtonTexture.Width * 2,
                exitButtonTexture.Height * 2);
        }

        public void Update(WorldController _world)
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                //начать игру
                if (startButtonRectangle.Contains(Mouse.GetState().Position))
                {
                    Global.IsGameActive = true;
                    _world.Restart();
                }
                //выйти из игры
                else if (exitButtonRectangle.Contains(Mouse.GetState().Position))
                {
                    Environment.Exit(0); 
                }
            }
        }

        public void Draw()
        {
            Global.SpriteBatch.Draw(startButtonTexture, startButtonRectangle, Color.White);
            Global.SpriteBatch.Draw(exitButtonTexture, exitButtonRectangle, Color.White);
        }
    }
}
