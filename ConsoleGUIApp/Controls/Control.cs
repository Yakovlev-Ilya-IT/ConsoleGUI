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

        public ConsoleColor BackFocusedColor { get; set; }
        public ConsoleColor BackUnfocusedColor { get; set; }

        public bool IsFocused { get; protected set; }

        public int ZIndex { get; private set; } = 0;

        protected ConsoleColor BackColor => IsFocused ? BackFocusedColor : BackUnfocusedColor;

        public virtual void OnInput(KeyInputEvent inputEvent) { }

        public virtual bool TryGetCellAt(Position position, out Cell cell)
        {
            Position relativePosition = position - Position;

            if (TryGetTextCell(relativePosition, out cell))
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

        private bool TryGetTextCell(Position relativePosition, out Cell cell)
        {
            if (string.IsNullOrEmpty(Text) == false)
            {
                if (IsInOfTextBox(relativePosition))
                {
                    cell = new Cell()
                        .WithContent(Text[relativePosition.X - ((Size.Width - 1) / 2 - (Text.Length - 1) / 2)])
                        .WithBackground(BackColor);
                    return true;
                }
            }

            cell = null;
            return false;
        }

        private bool IsInOfTextBox(Position relativePosition) => IsInOfTextBoxX(relativePosition.X) && IsInOfTextBoxY(relativePosition.Y);

        protected virtual bool IsInOfTextBoxX(int relativePositionX) => relativePositionX >= (Size.Width - 1) / 2 - (Text.Length - 1) / 2 && relativePositionX <= (Size.Width - 1) / 2 + Text.Length / 2;
        protected virtual bool IsInOfTextBoxY(int relativePositionY) => relativePositionY == (Size.Height - 1) / 2;
    }
}
