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
    public class PauseMenu
    {
        private Texture2D resumeButtonTexture;
        private Texture2D mainMenuButtonTexture;
        private Rectangle resumeButtonRectangle;
        private Rectangle mainMenuButtonRectangle;

        public PauseMenu()
        {
            resumeButtonTexture = Global.Content.Load<Texture2D>("Button");
            mainMenuButtonTexture = Global.Content.Load<Texture2D>("Button");

            resumeButtonRectangle = new Rectangle(
                (Global.Bounds.X - resumeButtonTexture.Width * 2) / 2,
                (600 + resumeButtonTexture.Height * 2) / 2,
                resumeButtonTexture.Width * 2,
                resumeButtonTexture.Height * 2);

            mainMenuButtonRectangle = new Rectangle(
                (Global.Bounds.X - mainMenuButtonTexture.Width * 2) / 2,
                resumeButtonRectangle.Bottom + 20,
                mainMenuButtonTexture.Width * 2,
                mainMenuButtonTexture.Height * 2);
        }

        public void Update()
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                //продолжить игру
                if (resumeButtonRectangle.Contains(Mouse.GetState().Position))
                {
                    Global.IsGamePaused = false; 
                }
                //выйти в главное меню
                else if (mainMenuButtonRectangle.Contains(Mouse.GetState().Position))
                {
                    Global.IsGameActive = false; // Return to the main menu
                    Global.IsGamePaused = false; // Ensure the game is not paused
                }
            }
        }

        public void Draw()
        {
            Global.SpriteBatch.Draw(resumeButtonTexture, resumeButtonRectangle, Color.White);
            Global.SpriteBatch.Draw(mainMenuButtonTexture, mainMenuButtonRectangle, Color.White);
        }
    }
}