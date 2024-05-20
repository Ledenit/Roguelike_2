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
        private Texture2D deadTexture;
        private Rectangle restartButtonRectangle;
        private Rectangle mainMenuButtonRectangle;
        private Rectangle exitButtonRectangle;
        private Rectangle deadRectangle;

        public DeathMenu()
        {
            restartButtonTexture = Global.Content.Load<Texture2D>("RestartGameButton");
            mainMenuButtonTexture = Global.Content.Load<Texture2D>("ExitMainMenuButton");
            exitButtonTexture = Global.Content.Load<Texture2D>("ExitButton");
            deadTexture = Global.Content.Load<Texture2D>("Dead");

            deadRectangle = new Rectangle(
                (Global.Bounds.X - restartButtonTexture.Width) / 2,
                (200 + restartButtonTexture.Height) / 2,
                restartButtonTexture.Width,
                restartButtonTexture.Height);

            restartButtonRectangle = new Rectangle(
                (Global.Bounds.X - restartButtonTexture.Width) / 2,
                (600 + restartButtonTexture.Height) / 2,
                restartButtonTexture.Width,
                restartButtonTexture.Height);

            mainMenuButtonRectangle = new Rectangle(
                (Global.Bounds.X - mainMenuButtonTexture.Width) / 2,
                restartButtonRectangle.Bottom + 20,
                mainMenuButtonTexture.Width,
                mainMenuButtonTexture.Height);

            exitButtonRectangle = new Rectangle(
               (Global.Bounds.X - exitButtonTexture.Width) / 2,
               mainMenuButtonRectangle.Bottom + 20,
               exitButtonTexture.Width,
               exitButtonTexture.Height);
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
            Global.SpriteBatch.Draw(deadTexture, deadRectangle, Color.White);
            Global.SpriteBatch.Draw(restartButtonTexture, restartButtonRectangle, Color.White);
            Global.SpriteBatch.Draw(mainMenuButtonTexture, mainMenuButtonRectangle, Color.White);
            Global.SpriteBatch.Draw(exitButtonTexture, exitButtonRectangle, Color.White);
        }
    }
}