using CnackGame.Core;
using Configs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SnackGame.Entities;
using SnackGame.Enums;

namespace SnackGame.Monogame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class SnackGame : Game
    {
        #region Private Fields

        private readonly GraphicsDeviceManager graphics;
        private DrawCore drawCore = new DrawCore();
        private SpriteBatch spriteBatch;

        // TODO to DI
        private UpdateCore updateCore = new UpdateCore();

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

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);

            // Add your drawing code here

            this.spriteBatch.Begin();

            this.drawCore.Draw(this.spriteBatch, this.world);

            this.spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Add your initialization logic here
            // can not init here, due to I need textures for entities in ctors

            this.IsMouseVisible = true;
            //this.graphics.IsFullScreen = true;

            this.ScreenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            this.ScreenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            this.graphics.PreferredBackBufferWidth = this.ScreenWidth;
            this.graphics.PreferredBackBufferHeight = this.ScreenHeight;

            this.Window.Position = Point.Zero;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);

            Configuration.Init(this.GraphicsDevice);

            // TODO to ask
            Cell[,] cells = SnackGame.GenerateWorld();

            SnackHead snackHead = new SnackHead(cells[cells.GetLength(0) / 2, cells.GetLength(1) / 2].Position.Clone(), Configuration.SnackHeadTextures);

            this.world = new World(snackHead, cells);

            // use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            // Add your update logic here

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