using Microsoft.Xna.Framework.Graphics;
using SnackGame.Enums;
using System.Collections.Generic;

namespace SnackGame.Entities
{
    public class Cell
    {
        public Cell(Position position, IDictionary<CellState, Texture2D> textures)
        {
            this.Position = position;
            this.Textures = textures;
            this.State = CellState.None;
        }

        public Position Position { get; set; }

        public CellState State { get; set; }

        public IDictionary<CellState, Texture2D> Textures { get; set; }
    }
}