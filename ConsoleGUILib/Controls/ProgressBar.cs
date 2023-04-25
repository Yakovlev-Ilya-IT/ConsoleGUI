using ConsoleGUILib.Data;

namespace ConsoleGUILib.Controls
{
    public class ProgressBar : Control
    {
        private float _progress = 0;

        public ProgressBar()
        {
            BackColor = ConsoleColor.DarkGray;
        }

        public override bool IsCanFocus => false;

        public float ProgressInPercantage
        {
            get => _progress;
            set
            {
                if(value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException("value");

                _progress = value;
            }
        }

        public ConsoleColor FillColor { get; set; } = ConsoleColor.Green;

        public override bool TryGetCellAt(Position position, out Cell cell)
        {
            if (IsOutOfBox(position))
            {
                cell = null;
                return false;
            }

            if (base.TryGetCellAt(position, out cell))
                return true;

            Position relativePosition = position - Position;

            float percentageConverter = 1 / 100f;

            int fillingValue = (int)(ProgressInPercantage * Size.Width * percentageConverter);

            if (fillingValue > Size.Width)
                fillingValue = Size.Width;

            if(relativePosition.X < fillingValue)
                cell = new Cell().WithBackground(FillColor);
            else
                cell = new Cell().WithBackground(BackColor);

            return true;
        }

        protected override bool TryGetTextCell(Position relativePosition, out Cell cell)
        {
            cell = null;
            return false;
        }

        protected override bool TryGetBorderCell(Position relativePosition, out Cell cell)
        {
            cell = null;
            return false;
        }
    }
}
