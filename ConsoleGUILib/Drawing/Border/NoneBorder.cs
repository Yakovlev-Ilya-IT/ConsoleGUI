using ConsoleGUILib.Data;

namespace ConsoleGUILib.Drawing
{
    public class NoneBorder : Border
    {
        protected override Cell GetCellFrom(Sides side)
        {
            return CreateCellWith(' ');
        }
    }
}
