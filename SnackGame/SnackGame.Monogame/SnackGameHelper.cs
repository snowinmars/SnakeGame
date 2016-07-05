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

            if (time.TotalMilliseconds - pr.TotalMilliseconds > Configuration.GameSpeed)
            {
                SnackGameHelper.Mode(world);

                pr = time;
            }
        }

        private static void Mode(World world)
        {
            switch (world.SnackHead.Direction)
            {
            case Direction.Up:
                world.SnackHead.Position.Y -= Configuration.SnackHeadStep.Y;
                break;

            case Direction.Right:
                world.SnackHead.Position.X += Configuration.SnackHeadStep.X;
                break;

            case Direction.Down:
                world.SnackHead.Position.Y += Configuration.SnackHeadStep.Y;
                break;

            case Direction.Left:
                world.SnackHead.Position.X -= Configuration.SnackHeadStep.X;
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