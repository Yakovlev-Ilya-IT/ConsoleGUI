using ConsoleGUIApp.Data;

namespace ConsoleGUIApp.Drawing
{
    public abstract class Border
    {
		public bool TryGet(Position position, Size size, out Cell borderCell)
        {
			if (position.X == 0 && position.Y == 0)
            {
				borderCell = GetCellFrom(Sides.TopLeft);
				return true;
			}

			if (position.X == size.Width - 1 && position.Y == 0)
            {
				borderCell = GetCellFrom(Sides.TopRight);
				return true;
			}

			if (position.X == 0 && position.Y == size.Height - 1)
			{
				borderCell = GetCellFrom(Sides.BottomLeft);
				return true;
			}

			if (position.X == size.Width - 1 && position.Y == size.Height - 1)
			{
				borderCell = GetCellFrom(Sides.BottomRight);
				return true;
			}

			if (position.X == 0)
			{
				borderCell = GetCellFrom(Sides.Left);
				return true;
			}

			if (position.X == size.Width - 1)
			{
				borderCell = GetCellFrom(Sides.Right);
				return true;
			}

			if (position.Y == 0)
			{
				borderCell = GetCellFrom(Sides.Top);
				return true;
			}

			if (position.Y == size.Height - 1)
			{
				borderCell = GetCellFrom(Sides.Bottom);
				return true;
			}

			borderCell = null;
			return false;
		}

		protected abstract Cell GetCellFrom(Sides side);

		protected Cell CreateCellWith(char content) => new Cell().WithContent(content);
	}
}
