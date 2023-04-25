using ConsoleGUIApp.Data;
using ConsoleGUIApp.Drawing;
using ConsoleGUIApp.Input;
using System;

namespace ConsoleGUIApp.Controls
{
    public class Button : Control
    {
        public event Action Click;

        public Button()
        {
            Size = new Size(8, 3);
            Text = "button";
            UnfocusedBorderStyle = BorderStyle.Single;
            FocusedBorderStyle = BorderStyle.Double;
            BorderColor = ConsoleColor.White;
            BackColor = ConsoleColor.DarkGray;
            TextColor = ConsoleColor.White;
        }

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

        public override void OnInput(KeyInputEvent inputEvent)
        {
            base.OnInput(inputEvent);

            switch (inputEvent.Key.Key)
            {
                case ConsoleKey.Enter:
                    Click?.Invoke();
                    break;

                default:
                    break;
            }
        }
    }
}
