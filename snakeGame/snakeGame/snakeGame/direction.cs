using System;
using System.Collections.Generic;

namespace snakeGame
{
    public class direction
    {

        public readonly static direction Left = new direction(0, -1);
        public readonly static direction Right = new direction(0, 1);
        public readonly static direction Up = new direction(-1, 0);
        public readonly static direction Down = new direction(1, 0);
        public int rowOffset { get; }
        public int colOffset { get; }

        private direction(int rowOffset, int colOffset)
        {
            this.rowOffset = rowOffset;
            this.colOffset = colOffset;
        }

        public direction Opposite()
        {
            return new direction(-rowOffset, -colOffset);
        }

        public override bool Equals(object obj)
        {
            return obj is direction direction &&
                   rowOffset == direction.rowOffset &&
                   colOffset == direction.colOffset;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(rowOffset, colOffset);
        }

        public static bool operator ==(direction left, direction right)
        {
            return EqualityComparer<direction>.Default.Equals(left, right);
        }

        public static bool operator !=(direction left, direction right)
        {
            return !(left == right);
        }
    }
}
