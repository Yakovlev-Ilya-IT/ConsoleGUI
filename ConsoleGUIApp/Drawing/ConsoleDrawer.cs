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

        private IDrawableContentHolder _drawableContentHolder;
        private ConsoleColor _consoleBackColor;

        public ConsoleDrawer(ConsoleColor consoleBackColor, IDrawableContentHolder drawableContentHolder)
        {
            _consoleBackColor = consoleBackColor;
            _drawableContentHolder = drawableContentHolder;

            SafeConsole.UpdateSize(WindowSize);
            _buffer = new ConsoleBuffer(WindowSize);
        }

        private IEnumerable<IDrawable> Content => _drawableContentHolder.Drawables;

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

                    if (TryDrawContent(position))
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

            SafeConsole.Write(position, cell);
        }
    }
}
