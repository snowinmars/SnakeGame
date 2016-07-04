using System;
using Microsoft.Xna.Framework;

namespace SnackGame.Entities
{
    public class World
    {
        public World(SnackHead snackHead, Cell[,] cells)
        {
            if (cells == null)
            {
                throw new ArgumentException("Cells can not be null");
            }

            if (snackHead == null)
            {
                throw new ArgumentException("Snack head can not be null");
            }

            this.SnackHead = snackHead;
            this.Cells = cells;
        }

        public SnackHead SnackHead { get; set; }

        public Cell[,] Cells { get; private set; }
    }
}