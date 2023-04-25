using ConsoleGUIApp.Data;
using ConsoleGUIApp.Drawing;
using ConsoleGUIApp.Input;
using System;
using System.Collections.Generic;

namespace ConsoleGUIApp.Controls
{
    public class TextBox : Control
    {
        private List<string> _text = new List<string>()
        {
            string.Empty,
        };

        private int _caretY = 0;

        public TextBox()
        {
            Size = new Size(20, 5);
            Text = "TextBox";
            UnfocusedBorderStyle = BorderStyle.Single;
            FocusedBorderStyle = BorderStyle.Double;
            BorderColor = ConsoleColor.White;
            BackColor = ConsoleColor.DarkGray;
            TextColor = TextFieldForegroundColor = ConsoleColor.White;
        }

        public ConsoleColor TextFieldForegroundColor { get; set; }

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

            if(relativePosition.Y >= 0 && relativePosition.Y <= _text.Count)
            {
                if(relativePosition.X <= _text[relativePosition.Y-1].Length)
                {
                    cell = new Cell()
                        .WithBackground(BackColor)
                        .WithForeground(TextFieldForegroundColor)
                        .WithContent(_text[relativePosition.Y-1][relativePosition.X-1]);
                    return true;
                }         
            }

            if(relativePosition.Y - 1 == _caretY && IsFocused)
            {
                if (relativePosition.X - 1 == _text[_caretY].Length)
                {
                    cell = new Cell().WithBackground(ConsoleColor.Red);
                    return true;
                }
            }

            cell = new Cell().WithBackground(BackColor);
            return true;
        }

        public override void OnInput(KeyInputEvent inputEvent)
        {
            base.OnInput(inputEvent);

            switch (inputEvent.Key.Key)
            {
                case ConsoleKey.Enter:
                    _text.Add(string.Empty);
                    _caretY++;
                    break;

                case ConsoleKey.Backspace:
                    if(_text[_caretY] == string.Empty){
                        if (_caretY == 0)
                            return;

                        _text.RemoveAt(_caretY);
                        _caretY--;
                        return;
                    }

                    _text[_caretY] = _text[_caretY].Remove(_text[_caretY].Length - 1);
                    break;

                default:
                    _text[_caretY] += inputEvent.Key.KeyChar.ToString();
                    break;
            }
        }

        protected override bool IsInOfTextBoxY(int relativePositionY) => relativePositionY == 0;
    }
}
