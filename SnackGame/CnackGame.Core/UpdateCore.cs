﻿using Configs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SnackGame.Entities;
using SnackGame.Enums;
using System;

namespace CnackGame.Core
{
    public class UpdateCore
    {
        #region Private Fields

        private readonly Keys[] allowedKeys = {
                                                        Keys.Up,
                                                        Keys.Down,
                                                        Keys.Left,
                                                        Keys.Right,
                                                    };

        private KeyboardState previousState;

        private TimeSpan previousTime;

        #endregion Private Fields

        #region Public Constructors

        public UpdateCore()
        {
            this.previousState = Keyboard.GetState();
        }

        #endregion Public Constructors

        #region Public Properties

        public Random Random { get; } = new Random();

        #endregion Public Properties

        #region Public Methods

        public void Update(GameTime gameTime, World world)
        {
            this.CheckHedgehog(world.Cells);

            this.Update(gameTime, world.SnackHead);

            foreach (var cell in world.Cells)
            {
                this.Update(gameTime, cell);
            }

            TimeSpan time = gameTime.TotalGameTime;
            double diffTime = time.TotalMilliseconds - this.previousTime.TotalMilliseconds;

            if (diffTime > Configuration.GameSpeed)
            {
                this.Move(world);
                this.previousTime = time;
            }

            // try to found food

            bool found = false;

            int i;
            int j = 0;
            // TODO optimize
            for (i = 0; i < Configuration.WorldSize.X; i++)
            {
                for (j = 0; j < Configuration.WorldSize.Y; j++)
                {
                    if (world.Cells[i, j].State == CellState.PositivePrice)
                    {
                        found = true;
                        goto breakcycles;
                    }
                }
            }
            breakcycles:

            if (diffTime <= Configuration.GameSpeed)
            {
                return;
            }

            // try to generate food

            if (!found)
            {
                Cell cell = world.Cells[this.Random.Next(1, world.Cells.GetLength(0) - 1), this.Random.Next(1, world.Cells.GetLength(1) - 1)];

                // if cell is occupied
                while (cell.State == CellState.Snake)
                {
                    cell = world.Cells[this.Random.Next(1, world.Cells.GetLength(0) - 1), this.Random.Next(1, world.Cells.GetLength(1) - 1)];
                }

                cell.State = CellState.PositivePrice;
            }
            else
            {
                // TODO to snake prop
                int hor = (int)Math.Floor((float)world.SnackHead.Position.X / (Configuration.CellSize.X + 1));
                int ver = (int)Math.Floor((float)world.SnackHead.Position.Y / (Configuration.CellSize.Y + 1));

                if (hor == i && ver == j)
                {
                    world.SnakeIncrease(hor, ver, 12);
                }
            }
        }

        private void CheckHedgehog(Cell[,] cells)
        {
            if (cells.GetLength(0) < 9 || cells.GetLength(1) < 9)
            {
                return;
            }

            for (int i = 4; i < 7; i++)
            {
                for (int j = 3; j < 7; j++)
                {

                    if (cells[i, j].State != CellState.Snake)
                    {
                        return;
                    }
                }
            }
            cells[1, 6].State = CellState.PositivePrice;

            cells[2, 6].State = CellState.PositivePrice;
            cells[2, 5].State = CellState.PositivePrice;

            cells[3, 6].State = CellState.PositivePrice;
            cells[3, 3].State = CellState.PositivePrice;
            cells[3, 4].State = CellState.PositivePrice;

            cells[4, 6].State = CellState.PositivePrice;
            cells[4, 5].State = CellState.PositivePrice;
            cells[4, 4].State = CellState.PositivePrice;
            cells[4, 3].State = CellState.PositivePrice;

            cells[5, 6].State = CellState.PositivePrice;
            cells[5, 5].State = CellState.PositivePrice;
            cells[5, 4].State = CellState.PositivePrice;
            cells[5, 3].State = CellState.PositivePrice;

            cells[6, 6].State = CellState.PositivePrice;
            cells[6, 5].State = CellState.PositivePrice;
            cells[6, 4].State = CellState.PositivePrice;
            cells[6, 3].State = CellState.PositivePrice;

            cells[7, 6].State = CellState.PositivePrice;
            cells[7, 5].State = CellState.PositivePrice;
            cells[7, 4].State = CellState.PositivePrice;
        }

        public void Update(GameTime gameTime, Cell cell)
        {
        }

        public void Update(GameTime gameTime, SnackHead head)
        {
            this.ReadKey(head);
        }

        #endregion Public Methods

        #region Private Methods

        private void Move(World world)
        {
            // TODO to snake prop
            int hor = (int)Math.Floor((float)world.SnackHead.Position.X / (Configuration.CellSize.X + 1));
            int ver = (int)Math.Floor((float)world.SnackHead.Position.Y / (Configuration.CellSize.Y + 1));

            world.SnakeOn(hor, ver);

            switch (world.SnackHead.Direction)
            {
                case Direction.Up:
                    if (world.Cells[hor, ver - 1].State != CellState.Border)
                    {
                        world.SnackHead.Position.Y -= Configuration.SnackHeadStep.Y;
                    }
                    break;

                case Direction.Right:
                    if (world.Cells[hor + 1, ver].State != CellState.Border)
                    {
                        world.SnackHead.Position.X += Configuration.SnackHeadStep.X;
                    }
                    break;

                case Direction.Down:
                    if (world.Cells[hor, ver + 1].State != CellState.Border)
                    {
                        world.SnackHead.Position.Y += Configuration.SnackHeadStep.Y;
                    }
                    break;

                case Direction.Left:
                    if (world.Cells[hor - 1, ver].State != CellState.Border)
                    {
                        world.SnackHead.Position.X -= Configuration.SnackHeadStep.X;
                    }
                    break;
            }
        }

        private void ReadKey(SnackHead head)
        {
            KeyboardState state = Keyboard.GetState();

            foreach (var key in this.allowedKeys)
            {
                if (state.IsKeyUp(key) && this.previousState.IsKeyDown(key))
                {
                    // ReSharper disable once SwitchStatementMissingSomeCases
                    // due to this are the only keys I'm interested in
                    switch (key)
                    {
                        case Keys.Left:
                            head.Direction = Direction.Left;
                            break;

                        case Keys.Up:
                            head.Direction = Direction.Up;
                            break;

                        case Keys.Right:
                            head.Direction = Direction.Right;
                            break;

                        case Keys.Down:
                            head.Direction = Direction.Down;
                            break;
                    }
                }
            }

            this.previousState = state;
        }

        #endregion Private Methods
    }
}