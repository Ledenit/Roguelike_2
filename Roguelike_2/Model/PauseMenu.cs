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
        private Texture2D restartButtonTexture;
        private Texture2D mainMenuButtonTexture;
        private Texture2D exitButtonTexture;
        private Rectangle resumeButtonRectangle;
        private Rectangle restartButtonRectangle;
        private Rectangle mainMenuButtonRectangle;
        private Rectangle exitButtonRectangle;

        public PauseMenu()
        {
            resumeButtonTexture = Global.Content.Load<Texture2D>("ResumeGameButton");
            restartButtonTexture = Global.Content.Load<Texture2D>("RestartGameButton");
            mainMenuButtonTexture = Global.Content.Load<Texture2D>("ExitMainMenuButton");
            exitButtonTexture = Global.Content.Load<Texture2D>("ExitButton");

            resumeButtonRectangle = new Rectangle(
                (Global.Bounds.X - resumeButtonTexture.Width) / 2,
                (400 + resumeButtonTexture.Height ) / 2,
                resumeButtonTexture.Width,
                resumeButtonTexture.Height);

            restartButtonRectangle = new Rectangle(
                (Global.Bounds.X - restartButtonTexture.Width) / 2,
                resumeButtonRectangle.Bottom + 20,
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
                if (resumeButtonRectangle.Contains(Mouse.GetState().Position))
                {
                    Global.IsGamePaused = false; 
                }
                else if (restartButtonRectangle.Contains(Mouse.GetState().Position))
                {
                    _world.Restart();
                    Global.IsGamePaused = false;
                    Global.IsGameActive = true;
                }
               
                else if (mainMenuButtonRectangle.Contains(Mouse.GetState().Position))
                {
                    Global.IsGameActive = false; 
                    Global.IsGamePaused = false; 
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
            Global.SpriteBatch.Draw(resumeButtonTexture, resumeButtonRectangle, Color.White);
            Global.SpriteBatch.Draw(mainMenuButtonTexture, mainMenuButtonRectangle, Color.White);
            Global.SpriteBatch.Draw(exitButtonTexture, exitButtonRectangle, Color.White);
        }
    }
}