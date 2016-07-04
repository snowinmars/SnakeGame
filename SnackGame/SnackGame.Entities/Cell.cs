using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SnackGame.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
