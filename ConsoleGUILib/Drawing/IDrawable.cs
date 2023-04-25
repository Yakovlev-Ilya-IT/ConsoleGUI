using ConsoleGUILib.Data;

namespace ConsoleGUILib.Drawing
{
    public interface IDrawable
    {
        public int ZIndex { get; }

        bool TryGetCellAt(Position position, out Cell cell);
    }
}
