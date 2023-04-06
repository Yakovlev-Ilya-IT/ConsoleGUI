using ConsoleGUIApp.Data;

namespace ConsoleGUIApp.Drawing
{
    public class ConsoleBuffer
    {
        private Cell[,] _buffer;

        public Size Size => new Size(_buffer.GetLength(1), _buffer.GetLength(0));

        public ConsoleBuffer(Size size) => _buffer = new Cell[size.Height, size.Width];

        public void Clear()
        {
            for (int i = 0; i < _buffer.GetLength(0); i++)
                for (int j = 0; j < _buffer.GetLength(1); j++)
                    _buffer[i, j] = null;
        }

        public void UpdateSize(Size size) => _buffer = new Cell[size.Height, size.Width];

        public bool IsContentChanged(Position position, Cell newCell)
        {
            ref Cell cell = ref _buffer[position.Y, position.X];

            if (cell == null)
            {
                cell = newCell;
                return true;
            }

            if (cell == newCell)
                return false;

            cell = newCell;
            return true;
        }
    }
}
