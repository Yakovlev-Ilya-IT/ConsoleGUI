using ConsoleGUIApp.Data;
using ConsoleGUIApp.Input;
using System;

namespace ConsoleGUIApp.Controls
{
    public class Button : Control
    {
        public event Action Click;

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

            throw new InvalidOperationException("can't get cell");
        }

        public override void OnInput(KeyInputEvent inputEvent)
        {
            switch (inputEvent.Key.Key)
            {
                case ConsoleKey.Spacebar:
                    Click?.Invoke();
                    break;

                default:
                    break;
            }
        }
    }
}
