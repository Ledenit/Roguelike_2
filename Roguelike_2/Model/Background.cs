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
using SharpDX.Direct3D9;
#endregion

namespace Roguelike_2
{
    public class Background : SpriteMoving
    {
        private readonly Point _mapTileSize = new(10, 7);
        private readonly SpriteMoving[,] _tiles;
        private readonly Texture2D _tile = Global.Content.Load<Texture2D>("tile1");

        public Background(Texture2D texture, Vector2 position) : base(texture, position)
        {
            _tiles = new SpriteMoving[_mapTileSize.X, _mapTileSize.Y];

            List<Texture2D> textures = new(5);
            for (int i = 1; i < 6; i++) textures.Add(Global.Content.Load<Texture2D>($"tile{i}"));

            Point TileSize = new(textures[0].Width, textures[0].Height);
            Random random = new();
            for (int y = 0; y < _mapTileSize.Y; y++)
            {
                for (int x = 0; x < _mapTileSize.X; x++)
                {
                    int r = random.Next(0, textures.Count);
                    _tiles[x, y] = new(textures[r], new(x * TileSize.X, y  * TileSize.Y));
                }
            }
        }

        public new void Update()
        {
            for (int y = 0; y < _mapTileSize.Y; y++)
            {
                for (int x = 0; x < _mapTileSize.X; x++)
                {
                    var t = _tiles[x, y];
                    t.Update();

                    if (t.Position.X < (0 - _tile.Width))
                    {
                        t.Position.X += _mapTileSize.X * _tile.Width;
                    }
                    if (t.Position.X > Global.Bounds.X + _tile.Width)
                    {
                        t.Position.X -= _mapTileSize.X * _tile.Width;
                    }

                    if (t.Position.Y < (0 - _tile.Height))
                    {
                        t.Position.Y += _mapTileSize.Y * _tile.Height;
                    }
                    if (t.Position.Y > Global.Bounds.Y + _tile.Height)
                    {
                        t.Position.Y -= _mapTileSize.Y * _tile.Height;
                    }
                }
            }
        }

        public new void Draw()
        {
            for (int y = 0; y < _mapTileSize.Y; y++)
            {
                for (int x = 0; x < _mapTileSize.X; x++) _tiles[x, y].Draw();
            }
        }

        public void Reset()
        {
            Position = new(Global.Bounds.X / 2, Global.Bounds.Y / 2);
            List<Texture2D> textures = new(5);
            for (int i = 1; i < 6; i++) textures.Add(Global.Content.Load<Texture2D>($"tile{i}"));
            Point TileSize = new(textures[0].Width, textures[0].Height);
            Random random = new();
            for (int y = 0; y < _mapTileSize.Y; y++)
            {
                for (int x = 0; x < _mapTileSize.X; x++)
                {
                    int r = random.Next(0, textures.Count);
                    _tiles[x, y] = new(textures[r], new(x * TileSize.X, y * TileSize.Y));
                }
            }
        }
    }
}
