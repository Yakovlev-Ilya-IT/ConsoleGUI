using ConsoleGUILib.Data;

namespace ConsoleGUILib.Controls
{
    public class Label: Control
    {
        public Label()
        {
            Text = "Label";
            BackColor = ConsoleColor.White;
            Size = new Size(5, 1);
        }

        public override bool IsCanFocus => false;

        public override bool TryGetCellAt(Position position, out Cell cell)
        {
            if (IsOutOfBox(position))
            {
                cell = null;
                return false;
            }

            if (base.TryGetCellAt(position, out cell))
                return true;

            cell = new Cell().WithBackground(BackColor);
            return true;
        }

        protected override bool TryGetTextCell(Position relativePosition, out Cell cell)
        {
            if (string.IsNullOrEmpty(Text) == false)
            {
                if (IsInOfTextBox(relativePosition))
                {
                    cell = new Cell()
                        .WithContent(Text[relativePosition.X])
                        .WithBackground(BackColor)
                        .WithForeground(TextColor);
                    return true;
                }
            }

            cell = null;
            return false;
        }

        protected override bool IsInOfTextBoxX(int relativePositionX) => relativePositionX < Text.Length && relativePositionX >= 0;
        protected override bool IsInOfTextBoxY(int relativePositionY) => relativePositionY == 0;
    }
}
