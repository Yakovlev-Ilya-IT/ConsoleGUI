using ConsoleGUIApp.Data;

namespace ConsoleGUIApp.Drawing
{
    public interface IDrawable
    {
        public int ZIndex { get; }

        bool TryGetCellAt(Position position, out Cell cell);
    }
}
