using Configs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SnackGame.Entities;
using SnackGame.Entities.Enums;
using System;

namespace SnackGame.Monogame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class SnackGame : Game
    {
        private readonly GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private World World;

        public SnackGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Add your drawing code here

            this.spriteBatch.Begin();

            SnackGameHelper.Draw(this.spriteBatch, this.World);

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

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Configuration.Init(this.GraphicsDevice);

            Cell[,] cells = GenerateWorld();

            SnackHead snackHead = new SnackHead(cells[cells.GetLength(0) / 2, cells.GetLength(1) / 2].Position.Clone(), Configuration.SnackHeadTextures);

            this.World = new World(snackHead, cells);


            // use this.Content to load your game content here
        }

        private static Cell[,] GenerateWorld()
        {
            int x = Configuration.WorldSize.X ;
            int y = Configuration.WorldSize.Y ;

            Cell[,] cells = new Cell[x, y];

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    cells[i, j] = new Cell(new Position(i * (Configuration.CellSize.X + Configuration.CellMagrin.X),
                                                        j * (Configuration.CellSize.Y + Configuration.CellMagrin.Y)),
                                           Configuration.CellTextures);

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
                Exit();
            }

            // Add your update logic here

            SnackGameHelper.Update(gameTime, this.World);

            base.Update(gameTime);
        }
    }
}