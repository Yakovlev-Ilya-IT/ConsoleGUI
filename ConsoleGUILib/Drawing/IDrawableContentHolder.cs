using System.Collections.Generic;

namespace ConsoleGUIApp.Drawing
{
    public interface IDrawableContentHolder
    {
        IEnumerable<IDrawable> Drawables { get; }
    }
}
