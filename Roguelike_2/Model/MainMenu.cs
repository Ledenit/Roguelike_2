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
        private Texture2D tutorialTexture;
        private Rectangle startButtonRectangle;
        private Rectangle exitButtonRectangle;
        private Rectangle tutorialButtonRectangle;

        public MainMenu()
        {
            startButtonTexture = Global.Content.Load<Texture2D>("StartGameButton");
            exitButtonTexture = Global.Content.Load<Texture2D>("ExitButton");
            tutorialTexture = Global.Content.Load<Texture2D>("Tutorial");

            startButtonRectangle = new Rectangle(
                (Global.Bounds.X - startButtonTexture.Width) / 2,
                (200 + startButtonTexture.Height) / 2,
                startButtonTexture.Width,
                startButtonTexture.Height);

 
            exitButtonRectangle = new Rectangle(
                (Global.Bounds.X - exitButtonTexture.Width) / 2,
                startButtonRectangle.Bottom + 20,
                exitButtonTexture.Width,
                exitButtonTexture.Height);

            tutorialButtonRectangle = new Rectangle(
                (Global.Bounds.X - tutorialTexture.Width) / 2,
                exitButtonRectangle.Bottom + 110,
                tutorialTexture.Width,
                tutorialTexture.Height);
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
            Global.SpriteBatch.Draw(tutorialTexture, tutorialButtonRectangle, Color.White);
        }
    }
}
