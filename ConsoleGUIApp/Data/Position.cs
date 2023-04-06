using System;

namespace ConsoleGUIApp.Data
{
    public struct Position
    {
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public static Position Empty => new Position(0, 0);

        public static bool operator ==(in Position first, in Position second) => first.X == second.X && first.Y == second.Y;
        public static bool operator !=(in Position first, in Position second) => !(first == second);
        public static Position operator -(in Position first, in Position second) => new Position(first.X - second.X, first.Y - second.Y);

        public override bool Equals(object obj) => obj is Position position && this == position;

        public override int GetHashCode() => HashCode.Combine(X, Y);
    }
}
