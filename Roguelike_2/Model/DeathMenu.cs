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
    public class DeathMenu
    {
        private Texture2D resumeButtonTexture;
        private Texture2D mainMenuButtonTexture;
        private Rectangle restartButtonRectangle;
        private Rectangle mainMenuButtonRectangle;

        public DeathMenu()
        {
            resumeButtonTexture = Global.Content.Load<Texture2D>("Button");
            mainMenuButtonTexture = Global.Content.Load<Texture2D>("Button");

            restartButtonRectangle = new Rectangle(
                (Global.Bounds.X - resumeButtonTexture.Width * 2) / 2,
                (600 + resumeButtonTexture.Height * 2) / 2,
                resumeButtonTexture.Width * 2,
                resumeButtonTexture.Height * 2);

            mainMenuButtonRectangle = new Rectangle(
                (Global.Bounds.X + 200 - mainMenuButtonTexture.Width * 2) / 2,
                restartButtonRectangle.Bottom + 20,
                mainMenuButtonTexture.Width * 2,
                mainMenuButtonTexture.Height * 2);
        }

        public void Update(WorldController _world)
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                //перезапустить игру
                if (restartButtonRectangle.Contains(Mouse.GetState().Position))
                {
                    _world.Restart();
                    Global.IsGameActive = true;       
                }
                //выйти в главное меню
                else if (mainMenuButtonRectangle.Contains(Mouse.GetState().Position))
                {
                    Global.IsGameActive = false;
                    Global.IsPlayerDead = false;
                }
            }
        }

        public void Draw()
        {
            Global.SpriteBatch.Draw(resumeButtonTexture, restartButtonRectangle, Color.White);
            Global.SpriteBatch.Draw(mainMenuButtonTexture, mainMenuButtonRectangle, Color.White);
        }
    }
}