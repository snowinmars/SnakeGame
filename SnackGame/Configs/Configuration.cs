﻿using Algorithms.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SnackGame.Entities;
using SnackGame.Entities.Enums;
using System.Collections.Generic;

namespace Configs
{
    public static class Configuration
    {
        public static IDictionary<CellState, Texture2D> CellTextures;
        public static IDictionary<Direction, Texture2D> SnackHeadTextures;

        static Configuration()
        {
            Configuration.WorldSize = new Point(800, 600);
            Configuration.CellSize = new Point(20, 20);
            Configuration.CellMagrin = new Point(1, 1);
            Speed = 100;
        }

        public static Point CellMagrin { get; set; }
        public static Point CellSize { get; set; }
        public static Point WorldSize { get; set; }
        public static int Speed { get; set; }

        public static void Init(GraphicsDevice graphicsDevice)
        {
            Configuration.CellTextures = new Dictionary<CellState, Texture2D>
            {
                { CellState.None, graphicsDevice.CreateTexture(Configuration.CellSize.X, Configuration.CellSize.Y, Color.Blue) },
                { CellState.Snake, graphicsDevice.CreateTexture(Configuration.CellSize.X, Configuration.CellSize.Y, Color.LightYellow) },
                { CellState.Border, graphicsDevice.CreateTexture(Configuration.CellSize.X, Configuration.CellSize.Y, Color.Gray) }
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