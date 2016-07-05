using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using SnackGame.Entities.Enums;

namespace SnackGame.Entities
{
    public class SnackHead
    {
        public SnackHead(Position position, IDictionary<Direction, Texture2D> textures)
        {
            this.Position = position;

            if (textures == null)
            {
                textures = new Dictionary<Direction, Texture2D>();
            }

            this.Textures = textures;

            this.Direction = Direction.Up;
        }

        public Position Position { get; set; }

        public IDictionary<Direction, Texture2D> Textures { get; set; }

        public Direction Direction { get; set; }
    }
}
