using ConsoleGUIApp.Data;
using ConsoleGUIApp.Drawing;
using ConsoleGUIApp.Input;
using System;
using System.Collections.Generic;

namespace ConsoleGUIApp.Controls
{
    public class ListBox : Control
    {
        public event Action<int> Click;
        public event Action<int> SelectedIndexChanged;

        private List<string> _items = new List<string>();
        private int _selectedIndex = -1;

        public ListBox()
        {
            Size = new Size(20, 5);
            Text = "TextBox";
            UnfocusedBorderStyle = BorderStyle.Single;
            FocusedBorderStyle = BorderStyle.Double;
            BorderColor = ConsoleColor.White;
            BackColor = ConsoleColor.DarkGray;
            TextColor = ConsoleColor.White;
        }

        public IEnumerable<string> Items => _items;
        public ConsoleColor SelectedItemColor => ConsoleColor.Green;

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

            for (int i = 0; i < _items.Count; i++)
            {
                if(relativePosition.Y >= i && relativePosition.Y <= (i+1))
                {
                    if (string.IsNullOrEmpty(_items[i]) == false)
                    {
                        if (relativePosition.X <= _items[i].Length && relativePosition.X > 0)
                        {
                            cell = new Cell()
                                .WithContent(_items[i][relativePosition.X - 1])
                                .WithForeground(TextColor);
                        }
                        else
                        {
                            cell = new Cell();
                        }

                        if (i == _selectedIndex)
                            cell = cell.WithBackground(SelectedItemColor);
                        else
                            cell = cell.WithBackground(BackColor);

                        return true;
                    }
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
                case ConsoleKey.UpArrow:
                    DecreaseSelectedIndex();
                    break;

                case ConsoleKey.DownArrow:
                    IncreaseSelectedIndex();
                    break;

                case ConsoleKey.Enter:
                    Click?.Invoke(_selectedIndex);
                    break;

                default:
                    break;
            }
        }

        public void SetSelectedItem(int index)
        {
            if(index < -1 || index >= _items.Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            _selectedIndex = index;
            SelectedIndexChanged?.Invoke(_selectedIndex);
        }

        public void AddRange(List<string> items) => _items.AddRange(items); 
        
        public void AddItem(string item) => _items.Add(item);

        public void Clear()
        {
            _items.Clear();
            _selectedIndex = -1;
        }

        public void RemoveItem(string item)
        {
            _items.Remove(item);

            if (_selectedIndex >= _items.Count)
                _selectedIndex = _items.Count - 1;
        }

        protected override bool IsInOfTextBoxY(int relativePositionY) => relativePositionY == 0;

        private void DecreaseSelectedIndex()
        {
            if (_selectedIndex <= 0)
                return;

            _selectedIndex--;
            SelectedIndexChanged?.Invoke(_selectedIndex);
        }

        private void IncreaseSelectedIndex()
        {
            if (_selectedIndex >= _items.Count - 1)
                return;

            _selectedIndex++;
            SelectedIndexChanged?.Invoke(_selectedIndex);
        }
    }
}
