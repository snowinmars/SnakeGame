using Microsoft.Xna.Framework;
using System;

namespace SnackGame.Entities
{
    public class Position : IComparable, IComparable<Position>, ICloneable
    {
        #region clone

        public Position Clone()
        {
            return new Position(this.X, this.Y);
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }

        #endregion clone

        #region Public Constructors

        public Position() : this(0, 0)
        {
        }

        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        #endregion Public Constructors

        #region Public Properties

        public int X { get; set; }
        public int Y { get; set; }

        #endregion Public Properties

        #region convert

        public Point ToPoint()
        {
            return new Point(this.X, this.Y);
        }

        public Vector2 ToVector2()
        {
            return new Vector2(this.X, this.Y);
        }

        #endregion convert

        #region equals

        public static bool operator !=(Position lhs, Position rhs)
            => !(lhs == rhs);

        public static bool operator ==(Position lhs, Position rhs)
        {
            return lhs != null && lhs.CompareTo(rhs) == 0;
        }

        public int CompareTo(object obj)
        {
            Position pos = obj as Position;

            return this.CompareTo(pos);
        }

        public int CompareTo(Position pos)
        {
            if ((object)pos == null)
            {
                return -1;
            }

            return ((this.X == pos.X &&
                this.Y == pos.Y) ? 0 : -1);
        }

        public override bool Equals(object obj)
        {
            Position pos = obj as Position;

            return this.Equals(pos);
        }

        public bool Equals(Position pos)
        {
            if ((object)pos == null)
            {
                return false;
            }

            return this.CompareTo(pos) == 0;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (this.X * 397) ^ this.Y;
            }
        }

        #endregion equals
    }
}