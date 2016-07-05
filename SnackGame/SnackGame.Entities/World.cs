using System;
using System.Collections.Generic;
using SnackGame.Enums;

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

        public Cell[,] Cells { get; }

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

            if (this.SnakeBody.Count != 0)
            {
                Cell cell = this.SnakeBody.Dequeue();
                cell.State = CellState.None;
            }

            this.SnakeBody.Enqueue(this.Cells[snakeHor, snakeVer]);
        }
    }
}