using Configs;
using Microsoft.Xna.Framework;
using SnackGame.Entities.Enums;
using System;
using System.Collections.Generic;

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
            this.SnakeBody = new Queue<Cell>(64);
        }

        public SnackHead SnackHead { get; set; }

        public Cell[,] Cells { get; private set; }

        public Rectangle Rectangle
            => new Rectangle(0, 0, Configuration.WorldSize.X * (Configuration.CellSize.X + Configuration.CellMagrin.X),
                                    Configuration.WorldSize.Y * (Configuration.CellSize.Y + Configuration.CellMagrin.Y));

        public Queue<Cell> SnakeBody { get; set; }

        public void SnakeIncrease(int snakeHor, int snakeVer, int value)
        {
            for (int i = 0; i < value; i++)
            {
                this.SnakeBody.Enqueue(this.Cells[snakeHor, snakeVer]);
            }
        }

        public void SnakeOn(int snakeHor, int snakeVer)
        {
            this.Cells[snakeHor, snakeVer].State = CellState.Snake;
            if (SnakeBody.Count != 0)
            {
                Cell cell = SnakeBody.Dequeue();
                cell.State = CellState.None;
            }

            SnakeBody.Enqueue(this.Cells[snakeHor, snakeVer]);
        }
    }
}