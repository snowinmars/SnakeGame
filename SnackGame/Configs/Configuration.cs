using Algorithms.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SnackGame.Enums;
using System.Collections.Generic;

namespace Configs
{
    public static class Configuration
    {
        public static IDictionary<CellState, Texture2D> CellTextures { get; private set; }
        public static IDictionary<Direction, Texture2D> SnackHeadTextures { get; private set; }

        static Configuration()
        {
            Configuration.WorldSize = new Point(20, 10); // in cells
            Configuration.CellSize = new Point(20, 20);
            Configuration.CellMagrin = new Point(1, 1);
            Configuration.GameSpeed = 300;
            Configuration.SnackHeadStep = new Point(Configuration.CellSize.X + Configuration.CellMagrin.X,
                                                    Configuration.CellSize.Y + Configuration.CellMagrin.Y);
        }

        public static Point CellMagrin { get; private set; }
        public static Point CellSize { get; private set; }
        public static Point WorldSize { get; private set; }
        public static int GameSpeed { get; private set; }
        public static Point SnackHeadStep { get; private set; }

        public static void Init(GraphicsDevice graphicsDevice)
        {
            Configuration.CellTextures = new Dictionary<CellState, Texture2D>
            {
                { CellState.None, graphicsDevice.CreateTexture(Configuration.CellSize.X, Configuration.CellSize.Y, Color.LightBlue) },
                { CellState.Snake, graphicsDevice.CreateTexture(Configuration.CellSize.X, Configuration.CellSize.Y, Color.LightYellow) },
                { CellState.Border, graphicsDevice.CreateTexture(Configuration.CellSize.X, Configuration.CellSize.Y, Color.Gray) },
                { CellState.PositivePrice, graphicsDevice.CreateTexture(Configuration.CellSize.X, Configuration.CellSize.Y, Color.Green) },
            };

            Texture2D texture = graphicsDevice.CreateTexture(Configuration.CellSize.X, Configuration.CellSize.Y, Color.Yellow);

            Configuration.SnackHeadTextures = new Dictionary<Direction, Texture2D>
            {
                { Direction.Up,  texture},
                { Direction.Right,  texture},
                { Direction.Left,  texture},
                { Direction.Down,  texture},
            };
        }
    }
}