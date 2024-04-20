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
using static System.Net.Mime.MediaTypeNames;
#endregion

namespace Roguelike_2
{
    public class ProjectilesInfo
    {
        public Vector2 Position {  get; set; }
        public float Rotation { get; set; }
        public float LifeTime { get; set; }
        public float Speed { get; set; }
    }
}
