using Microsoft.Xna.Framework.Graphics;
using SnackGame.Enums;
using System.Collections.Generic;
using SandS.Algorithm.Library.PositionNamespace;

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

        public IDictionary<Direction, Texture2D> Textures { get; private set; }

        public Direction Direction { get; set; }
    }
}