using Configs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SnackGame.Entities;

namespace CnackGame.Core
{
    public class DrawCore
    {
        #region Public Methods

        public void Draw(SpriteBatch spriteBatch, World world)
        {
            foreach (var cell in world.Cells)
            {
                Draw(spriteBatch, cell);
            }

            Draw(spriteBatch, world.SnackHead);
        }

        public void Draw(SpriteBatch spriteBatch, Cell cell)
        {
            spriteBatch.Draw(cell.Textures[cell.State], new Rectangle(cell.Position.X, cell.Position.Y, Configuration.CellSize.X, Configuration.CellSize.Y), Color.White);
        }

        public void Draw(SpriteBatch spriteBatch, SnackHead snackHead)
        {
            spriteBatch.Draw(snackHead.Textures[snackHead.Direction], new Rectangle(snackHead.Position.X, snackHead.Position.Y, Configuration.CellSize.X, Configuration.CellSize.Y), Color.White);
        }

        #endregion Public Methods
    }
}