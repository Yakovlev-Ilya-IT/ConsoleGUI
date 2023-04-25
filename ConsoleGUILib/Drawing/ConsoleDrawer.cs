using ConsoleGUILib.Data;

namespace ConsoleGUILib.Drawing
{
    public class ConsoleDrawer
    {
        private readonly ConsoleBuffer _buffer;

        private IDrawableContentHolder _drawableContentHolder;
        private ConsoleColor _consoleBackColor;

        public ConsoleDrawer(ConsoleColor consoleBackColor, IDrawableContentHolder drawableContentHolder)
        {
            _consoleBackColor = consoleBackColor;
            _drawableContentHolder = drawableContentHolder;

            _buffer = new ConsoleBuffer(WindowSize);
        }

        private IEnumerable<IDrawable> Content => _drawableContentHolder.Drawables;

        private Size BufferSize => _buffer.Size;
        private Size WindowSize => new Size(Console.WindowWidth, Console.WindowHeight);

        public void AdjustBufferSize()
        {
            if (BufferSize != WindowSize)
                _buffer.UpdateSize(WindowSize);
        }

        public void Draw()
        {
            for (int y = 0; y < BufferSize.Height; y++)
            {
                for (int x = 0; x < BufferSize.Width; x++)
                {
                    if (BufferSize != WindowSize)
                        return;

                    Position position = new Position(x, y);

                    if (TryDrawContent(position))
                        continue;

                    Cell cell = new Cell().WithBackground(_consoleBackColor);
                    DrawCell(position, cell);
                }
            }
        }

        private bool TryDrawContent(Position position)
        {
            var sortByZIndexDrawables = Content.OrderByDescending(drawable => drawable.ZIndex);

            foreach (IDrawable drawable in sortByZIndexDrawables)
            {
                if (drawable.TryGetCellAt(position, out Cell cell))
                {
                    DrawCell(position, cell);
                    return true;
                }
            }

            return false;
        }

        private void DrawCell(Position position, Cell cell)
        {
            if (_buffer.IsContentChanged(position, cell) == false)
                return;

            Write(position, cell);
        }

        private void Write(Position position, Cell cell)
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(position.X, position.Y);
            Console.BackgroundColor = cell.Background;
            Console.ForegroundColor = cell.Foreground;
            Console.Write(cell.Content);
        }
    }
}
