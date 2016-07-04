using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnackGame.Entities
{
    public class Position : IComparable, IComparable<Position>
    {
        public int X {get;set;}
        public int Y { get; set; }

        public Position() : this(0,0)
        {

        }

        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

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

        public static bool operator == (Position lhs, Position rhs)
            => lhs.CompareTo(rhs) == 0;

        public static bool operator !=(Position lhs, Position rhs)
            => !(lhs == rhs);

        public override int GetHashCode() 
            => this.X ^ this.Y;
        #endregion equals

    }
}
