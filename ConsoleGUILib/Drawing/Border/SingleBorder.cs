using ConsoleGUILib.Data;

namespace ConsoleGUILib.Drawing
{
    public class SingleBorder : Border
    {
        protected override Cell GetCellFrom(Sides side)
        {
            switch (side)
            {
                case Sides.Top:
                    return CreateCellWith('─');

                case Sides.TopRight:
                    return CreateCellWith('┐');

                case Sides.Right:
                    return CreateCellWith('│');

                case Sides.BottomRight:
                    return CreateCellWith('┘');

                case Sides.Bottom:
                    return CreateCellWith('─');

                case Sides.BottomLeft:
                    return CreateCellWith('└');

                case Sides.Left:
                    return CreateCellWith('│');

                case Sides.TopLeft:
                    return CreateCellWith('┌');

                default:
                    throw new ArgumentException(nameof(side));  
            }
        }
    }
}
