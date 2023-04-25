namespace ConsoleGUILib.Data
{
    public struct Size
    {
        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int Width { get; }
        public int Height { get; }

		public bool Contains(Size size) => size.Width <= Width && size.Height <= Height;

		public bool Contains(Position position) => position.X >= 0 &&
                                                    position.Y >= 0 &&
                                                    position.X < Width &&
                                                    position.Y < Height;

        public static Size Empty => new Size(0, 0);

        public static bool operator ==(in Size first, in Size second) => first.Width == second.Width && first.Height == second.Height;

        public static bool operator !=(in Size first, in Size second) => !(first == second);

        public static bool operator <=(in Size first, in Size second) => first.Width <= second.Width && first.Height <= second.Height;

        public static bool operator >=(in Size first, in Size second) => first.Width >= second.Width && first.Height >= second.Height;

        public override bool Equals(object obj) => obj is Size size && this == size;

        public override int GetHashCode() => HashCode.Combine(Width, Height);
    }
}
