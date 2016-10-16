using CnackGame.Core;
using Configs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SandS.Algorithm.Library.PositionNamespace;
using SnackGame.Entities;
using SnackGame.Enums;

namespace SnackGame.Monogame
{
    public class SnackGame : Game
    {
        #region Private Fields

        private readonly GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        // TODO to DI
        private readonly DrawCore drawCore = new DrawCore();
        private readonly UpdateCore updateCore = new UpdateCore();

        private World world;

        #endregion Private Fields

        #region Public Constructors

        public SnackGame()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";
        }

        #endregion Public Constructors

        #region Public Properties

        public int ScreenHeight { get; private set; }

        public int ScreenWidth { get; private set; }

        #endregion Public Properties

        #region Protected Methods

        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);

            this.spriteBatch.Begin();

            this.drawCore.Draw(this.spriteBatch, this.world);

            this.spriteBatch.End();

            base.Draw(gameTime);
        }

        protected override void Initialize()
        {
            this.IsMouseVisible = true;
            //this.graphics.IsFullScreen = true;

            this.ScreenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            this.ScreenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            this.graphics.PreferredBackBufferWidth = this.ScreenWidth;
            this.graphics.PreferredBackBufferHeight = this.ScreenHeight;

            this.Window.Position = Point.Zero;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);

            Configuration.Init(this.GraphicsDevice);

            Cell[,] cells = SnackGame.GenerateWorld();

            Position center = cells[cells.GetLength(0) / 2, cells.GetLength(1) / 2].Position;
            SnackHead snackHead = new SnackHead(center.Clone(), Configuration.SnackHeadTextures);

            this.world = new World(snackHead, cells);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            this.updateCore.Update(gameTime, this.world);

            base.Update(gameTime);
        }

        #endregion Protected Methods

        #region Private Methods

        private static Cell[,] GenerateWorld()
        {
            int x = Configuration.WorldSize.X;
            int y = Configuration.WorldSize.Y;

            Cell[,] cells = new Cell[x, y];

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    cells[i, j] = new Cell(new Position(i * (Configuration.CellSize.X + Configuration.CellMagrin.X),
                                                        j * (Configuration.CellSize.Y + Configuration.CellMagrin.Y)),
                                           Configuration.CellTextures);

                    // if borders
                    if (i == 0 ||
                        j == 0 ||
                        i == x - 1 ||
                        j == y - 1)
                    {
                        cells[i, j].State = CellState.Border;
                    }
                }
            }

            return cells;
        }

        #endregion Private Methods
    }
}