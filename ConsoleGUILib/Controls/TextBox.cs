using ConsoleGUILib.Data;
using ConsoleGUILib.Drawing;
using ConsoleGUILib.Input;
using System.Text;

namespace ConsoleGUILib.Controls
{
    public class TextBox : Control
    {
        private List<string> _text = new List<string>()
        {
            string.Empty,
        };

        private int _caretY = 0;
        private int _caretX = 0;

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

        public override string Text
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in _text)
                    sb.Append(item + "\r\n");
                return sb.ToString();
            }
            set
            {
                if (value == null)
                    value = "";
                _text = new List<string>(value.Split("\r\n"));
                _caretY = _text.Count - 1;
                _caretX = _text[_text.Count - 1].Length;
            }
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

            int shift = 0;

            if (_caretY > Size.Height - 3)
                shift = _caretY - (Size.Height - 3);

            if (_text.Count != 0 && relativePosition.Y - 1 < _text.Count)
            {
                if (relativePosition.Y >= 0)
                {
                    if (relativePosition.X <= _text[relativePosition.Y - 1 + shift].Length)
                    {
                        cell = new Cell()
                            .WithBackground(BackColor)
                            .WithForeground(TextFieldForegroundColor)
                            .WithContent(_text[relativePosition.Y - 1 + shift][relativePosition.X - 1]);

                        if (relativePosition.Y - 1 + shift == _caretY && IsFocused && relativePosition.X - 1 == _caretX)
                            cell = cell.WithBackground(ConsoleColor.Red);

                        return true;
                    }
                }
            }

            if (relativePosition.Y - 1 + shift == _caretY && IsFocused)
            {
                if (relativePosition.X - 1 == _caretX)
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
                    _caretY++;
                    _caretX = 0;
                    _text.Insert(_caretY, string.Empty);
                    break;

                case ConsoleKey.Backspace:
                    if(_text[_caretY] == string.Empty){
                        if (_caretY == 0)
                            return;

                        _text.RemoveAt(_caretY);
                        _caretY--;
                        _caretX = _text[_caretY].Length;
                        return;
                    }

                    _text[_caretY] = _text[_caretY].Remove(_text[_caretY].Length - 1);
                    _caretX--;
                    break;

                case ConsoleKey.UpArrow:
                    if (_caretY == 0)
                        return;
                    _caretY--;

                    if(_caretX > _text[_caretY].Length)
                        _caretX = _text[_caretY].Length;

                    break;

                case ConsoleKey.DownArrow:
                    if (_caretY == _text.Count - 1)
                        return;
                    _caretY++;
                    if (_caretX > _text[_caretY].Length)
                        _caretX = _text[_caretY].Length;
                    break;

                case ConsoleKey.RightArrow:
                    if (_caretX == _text[_caretY].Length)
                        return;

                    _caretX++;
                    break;

                case ConsoleKey.LeftArrow:
                    if (_caretX == 0)
                        return;

                    _caretX--;
                    break;

                default:
                    if(inputEvent.Key.KeyChar.ToString() != "\0")
                    {
                        _text[_caretY] = _text[_caretY].Insert(_caretX, inputEvent.Key.KeyChar.ToString());
                        _caretX++;
                    }
                    break;
            }
        }

        protected override bool TryGetTextCell(Position relativePosition, out Cell cell)
        {
            cell = null;
            return false;
        }
    }
}
