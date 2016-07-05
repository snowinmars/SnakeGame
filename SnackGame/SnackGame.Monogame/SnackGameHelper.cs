using Configs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SnackGame.Entities;
using SnackGame.Entities.Enums;
using System;

namespace SnackGame.Monogame
{
    internal static class SnackGameHelper
    {
        private static KeyboardState previousState;

        static SnackGameHelper()
        {
            SnackGameHelper.previousState = Keyboard.GetState();
        }

        #region update

        private static Keys[] allowedKeys = new Keys[]
        {
            Keys.Up,
            Keys.Down,
            Keys.Left,
            Keys.Right,
        };

        public static void Update(GameTime gameTime, World world)
        {
            SnackGameHelper.Update(gameTime, world.SnackHead);

            foreach (var cell in world.Cells)
            {
                SnackGameHelper.Update(gameTime, cell);
            }

            TimeSpan time = gameTime.TotalGameTime;
            double diffTime = time.TotalMilliseconds - pr.TotalMilliseconds;

            if (diffTime > Configuration.GameSpeed)
            {
                SnackGameHelper.Move(world);

                pr = time;
            }

            bool found = false;

            int i = 0;
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
        breakcycles:;

            if (diffTime > Configuration.GameSpeed)
            {
                if (!found)
                {
                    world.Cells[Random.Next(1, world.Cells.GetLength(0) - 1), Random.Next(1, world.Cells.GetLength(1) - 1)].State = CellState.PositivePrice;
                }
                else
                {
                    // TODO to snake prop
                    int hor = world.SnackHead.Position.X / Configuration.CellSize.X;
                    int ver = world.SnackHead.Position.Y / Configuration.CellSize.Y;

                    if (hor == i && ver == j)
                    {
                        world.SnakeIncrease(hor, ver, 1);
                    }
                }
            }
        }

        private static void Move(World world)
        {
            // TODO to snake prop
            int hor = world.SnackHead.Position.X / Configuration.CellSize.X;
            int ver = world.SnackHead.Position.Y / Configuration.CellSize.Y;

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

            default:
                break;
            }
        }

        public static Random Random { get; } = new Random();

        public static void Update(GameTime gameTime, Cell cell)
        {
        }

        private static TimeSpan pr;

        public static void Update(GameTime gameTime, SnackHead head)
        {
            ReadKey(head);

            //{
            //    switch (head.Direction)
            //    {
            //    case Direction.Up:
            //        head.Position.Y--;
            //        break;
            //    case Direction.Right:
            //        head.Position.X++;
            //        break;
            //    case Direction.Down:
            //        head.Position.Y++;
            //        break;
            //    case Direction.Left:
            //        head.Position.X--;
            //        break;
            //    default:
            //        break;
            //    }
            //}
        }

        #endregion update

        #region draw

        public static void Draw(SpriteBatch spriteBatch, World world)
        {
            foreach (var cell in world.Cells)
            {
                Draw(spriteBatch, cell);
            }

            Draw(spriteBatch, world.SnackHead);
        }

        public static void Draw(SpriteBatch spriteBatch, Cell cell)
        {
            spriteBatch.Draw(cell.Textures[cell.State], cell.Rectangle, Color.White);
        }

        public static void Draw(SpriteBatch spriteBatch, SnackHead snackHead)
        {
            spriteBatch.Draw(snackHead.Textures[snackHead.Direction], snackHead.Rectangle, Color.White);
        }

        #endregion draw

        private static void ReadKey(SnackHead head)
        {
            KeyboardState state = Keyboard.GetState();

            foreach (var key in SnackGameHelper.allowedKeys)
            {
                if (state.IsKeyUp(key) &&
                    SnackGameHelper.previousState.IsKeyDown(key))
                {
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

                    default:
                        break;
                    }
                }
            }

            SnackGameHelper.previousState = state;
        }
    }
}