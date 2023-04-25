namespace ConsoleGUILib.Drawing
{
    public interface IDrawableContentHolder
    {
        IEnumerable<IDrawable> Drawables { get; }
    }
}
