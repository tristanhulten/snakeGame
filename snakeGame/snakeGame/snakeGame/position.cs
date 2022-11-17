using System;
using System.Collections.Generic;

namespace snakeGame
{
    public class position
    {
        public int row { get; }
        public int col { get; }

        public position(int row, int col)
        {
            this.row = row;
            this.col = col;
        }

        public position Translate(direction dir)
        {
            return new position(row + dir.rowOffset, col + dir.colOffset );
        }
        // Använder mig av den inbyggda funktionen "override equals and get hashcode" som gör att position klassen kan användas som en nyckel i ett dictionary
        public override bool Equals(object obj)
        {
            return obj is position position &&
                   row == position.row &&
                   col == position.col;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(row, col);
        }

        public static bool operator ==(position left, position right)
        {
            return EqualityComparer<position>.Default.Equals(left, right);
        }

        public static bool operator !=(position left, position right)
        {
            return !(left == right);
        }
    }
}
