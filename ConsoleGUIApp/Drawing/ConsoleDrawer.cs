using ConsoleGUIApp.Core;
using ConsoleGUIApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleGUIApp.Drawing
{
    public class ConsoleDrawer
    {
        private readonly ConsoleBuffer _buffer;

        private List<IDrawable> _content;
        private ConsoleColor _consoleBackColor;

        public ConsoleDrawer(ConsoleColor consoleBackColor, List<IDrawable> content)
        {
            _consoleBackColor = consoleBackColor;
            _content = new List<IDrawable>(content);

            SafeConsole.UpdateSize(WindowSize);
            _buffer = new ConsoleBuffer(WindowSize);
        }

        private Size BufferSize => _buffer.Size;
        private Size WindowSize => SafeConsole.Size;

        public void AdjustBufferSize()
        {
			if (BufferSize != WindowSize)
				Resize(WindowSize);
        }

        public void Draw()
        {
            for (int y = 0; y < BufferSize.Height; y++)
            {
                for (int x = 0; x < BufferSize.Width; x++)
                {
                    Position position = new Position(x, y);

                    if (TryDrawForm(position))
                        continue;

                    Cell cell = new Cell().WithBackground(_consoleBackColor);
                    DrawCell(position, cell);
                }
            }
        }

        private void Resize(Size size)
        {
			SafeConsole.UpdateSize(size);
            _buffer.UpdateSize(size);
        }

        private bool TryDrawForm(Position position)
        {
            var sortByZIndexDrawables = _content.OrderByDescending(drawable => drawable.ZIndex);

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

            SafeConsole.Write(position, cell);
        }
    }
}
