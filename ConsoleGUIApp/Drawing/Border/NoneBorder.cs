using ConsoleGUIApp.Data;

namespace ConsoleGUIApp.Drawing
{
    public class NoneBorder : Border
    {
        protected override Cell GetCellFrom(Sides side)
        {
            return CreateCellWith(' ');
        }
    }
}
