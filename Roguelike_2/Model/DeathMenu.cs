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
        private Texture2D restartButtonTexture;
        private Texture2D mainMenuButtonTexture;
        private Texture2D exitButtonTexture;
        private Rectangle restartButtonRectangle;
        private Rectangle mainMenuButtonRectangle;
        private Rectangle exitButtonRectangle;

        public DeathMenu()
        {
            restartButtonTexture = Global.Content.Load<Texture2D>("RestartGameButton");
            mainMenuButtonTexture = Global.Content.Load<Texture2D>("ExitMainMenuButton");
            exitButtonTexture = Global.Content.Load<Texture2D>("ExitButton");

            restartButtonRectangle = new Rectangle(
                (Global.Bounds.X - restartButtonTexture.Width * 2) / 2,
                (600 + restartButtonTexture.Height * 2) / 2,
                restartButtonTexture.Width * 2,
                restartButtonTexture.Height * 2);

            mainMenuButtonRectangle = new Rectangle(
                (Global.Bounds.X - mainMenuButtonTexture.Width * 2) / 2,
                restartButtonRectangle.Bottom + 20,
                mainMenuButtonTexture.Width * 2,
                mainMenuButtonTexture.Height * 2);

            exitButtonRectangle = new Rectangle(
               (Global.Bounds.X - exitButtonTexture.Width * 2) / 2,
               mainMenuButtonRectangle.Bottom + 20,
               exitButtonTexture.Width * 2,
               exitButtonTexture.Height * 2);
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
                else if (exitButtonRectangle.Contains(Mouse.GetState().Position))
                {
                    Environment.Exit(0);
                }
            }
        }

        public void Draw()
        {
            Global.SpriteBatch.Draw(restartButtonTexture, restartButtonRectangle, Color.White);
            Global.SpriteBatch.Draw(mainMenuButtonTexture, mainMenuButtonRectangle, Color.White);
            Global.SpriteBatch.Draw(exitButtonTexture, exitButtonRectangle, Color.White);
        }
    }
}