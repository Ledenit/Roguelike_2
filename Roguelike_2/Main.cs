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
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private WorldController _world;
        private MainMenu _mainMenu;
        private PauseMenu _pauseMenu;
        private DeathMenu _deathMenu;

        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Global.IsGameActive = false;
            Global.IsGamePaused = false;
        }

        protected override void Initialize()
        {
            Global.Bounds = new(1200, 900);
            _graphics.PreferredBackBufferWidth = Global.Bounds.X;
            _graphics.PreferredBackBufferHeight = Global.Bounds.Y;
            _graphics.ApplyChanges();

            Global.Content = Content;
            _world = new();
            _mainMenu = new();
            _pauseMenu = new();
            _deathMenu = new();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Global.SpriteBatch = _spriteBatch;
        }

        protected override void Update(GameTime gameTime)
        {
            if (Global.IsPlayerDead)
            {
                _deathMenu.Update(_world);
            }
            else
            {
                if (Global.IsGameActive)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape)) Global.IsGamePaused = true;
                    if (Global.IsGamePaused)
                        _pauseMenu.Update(_world);
                    else
                        _world.Update();
                }
                else
                {
                    _mainMenu.Update(_world);
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                        Exit();
                }
            }

            Global.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(162, 93, 38));

            _spriteBatch.Begin();

            if (Global.IsPlayerDead)
                _deathMenu.Draw();
            else
            {
                if (Global.IsGameActive)
                {
                    if (Global.IsGamePaused)
                        _pauseMenu.Draw();
                    else
                        _world.Draw(_spriteBatch);
                }
                else
                    _mainMenu.Draw();
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}