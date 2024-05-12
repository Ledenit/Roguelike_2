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
using SharpDX.MediaFoundation.DirectX;
#endregion

namespace Roguelike_2
{
    public static class Input
    {
        private static MouseState _lastMouseState;
        private static Vector2 _direction;
        private static KeyboardState _lastKeyboardState;

        public static Vector2 Direction => _direction;

        public static Vector2 MousePosition => Mouse.GetState().Position.ToVector2();

        public static bool MouseLeftClicked { get; private set; }

        public static bool MouseRightDown {  get; private set; }

        public static bool SpacePressed { get; private set; }

        public static bool RPressed { get; private set; }

        public static void Update()
        {
            var keyboardState = Keyboard.GetState();
            var mouseState = Mouse.GetState();

            _direction = Vector2.Zero;
            if (keyboardState.IsKeyDown(Keys.A)) _direction.X++;
            if (keyboardState.IsKeyDown(Keys.D)) _direction.X--;
            if (keyboardState.IsKeyDown(Keys.W)) _direction.Y++;
            if (keyboardState.IsKeyDown(Keys.S)) _direction.Y--;

            MouseLeftClicked = Mouse.GetState().LeftButton == ButtonState.Pressed && _lastMouseState.LeftButton == ButtonState.Released;
            MouseRightDown = Mouse.GetState().RightButton == ButtonState.Pressed;
            SpacePressed = _lastKeyboardState.IsKeyUp(Keys.Space) && keyboardState.IsKeyDown(Keys.Space);
            RPressed = _lastKeyboardState.IsKeyUp(Keys.R) && keyboardState.IsKeyDown(Keys.R);

            _lastMouseState = mouseState;
            _lastKeyboardState = keyboardState;
        }
    }
}
