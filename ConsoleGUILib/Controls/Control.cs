using ConsoleGUIApp.Data;
using ConsoleGUIApp.Drawing;
using ConsoleGUIApp.Input;
using System;

namespace ConsoleGUIApp.Controls
{
    public abstract class Control : IDrawable, IKeyInputListener
    {
        private Size _size;

        private Position _position;

        public virtual string Text { get; set; }

        public virtual bool IsCanFocus { get; set; } = true;
        public virtual bool IsTopLevel => false;

        public Size Size
        {
            get => _size;
            set
            {
                if (value <= Size.Empty)
                    throw new ArgumentOutOfRangeException(nameof(value));

                _size = value;
            }
        }

        public Position Position
        {
            get => _position;
            set
            {
                if (value.X < Position.Empty.X || value.Y < Position.Empty.Y)
                    throw new ArgumentOutOfRangeException(nameof(value));

                _position = value;
            }
        }


        public ConsoleColor BackColor { get; set; } = ConsoleColor.White;
        public ConsoleColor TextColor { get; set; } = ConsoleColor.Black;

        public BorderStyle FocusedBorderStyle { get; set; } = BorderStyle.None;
        public BorderStyle UnfocusedBorderStyle { get; set; } = BorderStyle.None;
        public ConsoleColor BorderColor { get; set; } = ConsoleColor.Black;

        public bool IsFocused { get; protected set; }

        public int ZIndex { get; private set; } = 0;

        private BorderStyle BorderStyle => IsFocused ? FocusedBorderStyle : UnfocusedBorderStyle;

        public virtual void OnInput(KeyInputEvent inputEvent) { }

        public virtual bool TryGetCellAt(Position position, out Cell cell)
        {
            Position relativePosition = position - Position;

            if (TryGetTextCell(relativePosition, out cell))
                return true;

            if (TryGetBorderCell(relativePosition, out cell))
                return true;

            return false;
        }

        public virtual void Focus()
        {
            IsFocused = true;
            IncrementZIndex();
        }

        public virtual void Unfocus()
        {
            IsFocused = false;
            DecrimentZIndex();
        }

        public void IncrementZIndex() => ZIndex++;
        public void DecrimentZIndex() => ZIndex--;

        protected bool IsOutOfBox(Position position) => position.X < Position.X || position.X >= Position.X + Size.Width || position.Y < Position.Y || position.Y >= Position.Y + Size.Height;

        protected virtual bool TryGetTextCell(Position relativePosition, out Cell cell)
        {
            if (string.IsNullOrEmpty(Text) == false)
            {
                string text = " " + Text + " ";
                if (IsInOfTextBox(relativePosition))
                {
                    cell = new Cell()
                        .WithContent(text[relativePosition.X - ((Size.Width - 1) / 2 - (text.Length - 1) / 2)])
                        .WithBackground(BackColor)
                        .WithForeground(TextColor);
                    return true;
                }
            }

            cell = null;
            return false;
        }

        protected bool IsInOfTextBox(Position relativePosition) => IsInOfTextBoxX(relativePosition.X) && IsInOfTextBoxY(relativePosition.Y);

        protected virtual bool IsInOfTextBoxX(int relativePositionX) => relativePositionX >= (Size.Width - 1) / 2 - (Text.Length + 1) / 2 && relativePositionX <= (Size.Width - 1) / 2 + (Text.Length + 2) / 2;
        protected virtual bool IsInOfTextBoxY(int relativePositionY) => relativePositionY == (Size.Height - 1) / 2;

        protected virtual bool TryGetBorderCell(Position relativePosition, out Cell cell)
        {
            Border border = BorderFactory.Get(BorderStyle);

            if (border.TryGet(relativePosition, Size, out cell))
            {
                cell = cell
                    .WithBackground(BackColor)
                    .WithForeground(BorderColor);
                return true;
            }

            return false;
        }
    }
}
