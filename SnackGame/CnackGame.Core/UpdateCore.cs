﻿using Configs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SnackGame.Entities;
using SnackGame.Enums;
using System;
using SandS.Algorithm.Library.PositionNamespace;

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
            this.Update(gameTime, world.SnackHead);

            double diffTime = this.MovePlayerDiscretely(gameTime, world);

            // try to found food

            int i = 0;
            int j = 0;
            Position foodPosition = UpdateCore.TryToCreateFood(world, i, j);

            if (diffTime <= Configuration.GameSpeed)
            {
                return;
            }

            // try to generate food

            if (object.ReferenceEquals(foodPosition, null))
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

                if (hor == foodPosition.X && ver == foodPosition.Y)
                {
                    world.SnakeIncrease(hor, ver, 12);
                }
            }
        }

        private static Position TryToCreateFood(World world, int i, int j)
        {
            // TODO optimize
            for (i = 0; i < Configuration.WorldSize.X; i++)
            {
                for (j = 0; j < Configuration.WorldSize.Y; j++)
                {
                    if (world.Cells[i, j].State == CellState.PositivePrice)
                    {
                        return new Position(i,j);
                    }
                }
            }

            return null;
        }

        private double MovePlayerDiscretely(GameTime gameTime, World world)
        {
            TimeSpan time = gameTime.TotalGameTime;
            double diffTime = time.TotalMilliseconds - this.previousTime.TotalMilliseconds;

            if (diffTime > Configuration.GameSpeed)
            {
                this.Move(world);
                this.previousTime = time;
            }

            return diffTime;
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