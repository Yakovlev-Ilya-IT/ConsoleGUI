using ConsoleGUIApp.Data;
using ConsoleGUIApp.Drawing;
using ConsoleGUIApp.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleGUIApp.Controls
{
    public class Form: Control
    {
        private Control _activeControl;

        public string Title { get; set; }

        public BorderStyle FocusedBorderStyle { get; set; } = BorderStyle.Double;
        public BorderStyle UnfocusedBorderStyle { get; set; } = BorderStyle.Single;

        protected List<Control> Controls { get; } = new List<Control>();

        private BorderStyle BorderStyle => IsFocused ? FocusedBorderStyle : UnfocusedBorderStyle;

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

            if (TryGetBorderCell(relativePosition, out cell))
                return true;

            if (TryGetControlCell(relativePosition, out cell))
                return true;

            cell = new Cell().WithBackground(BackColor);
            return true;

            throw new InvalidOperationException("can't get cell");
        }

        public override void Focus()
        {
            base.Focus();

            if (Controls.Count == 0)
                return;

            if (_activeControl == null)
            {
                _activeControl = Controls[0];
                Controls.RemoveAt(0);
                Controls.Add(_activeControl);
            }

            _activeControl.Focus();
        }

        public override void Unfocus()
        {
            base.Unfocus();

            _activeControl?.Unfocus();
        }

        public override void OnInput(KeyInputEvent inputEvent)
        {
            switch (inputEvent.Key.Key)
            {
                case ConsoleKey.Tab:
                    SwitchActiveControl();
                    break;

                default:
                    _activeControl.OnInput(inputEvent);
                    break;
            }
        }

        private bool TryGetBorderCell(Position relativePosition, out Cell cell)
        {
            Border border = BorderFactory.Get(BorderStyle);

            if (border.TryGet(relativePosition, Size, out cell))
            {
                cell = cell.WithBackground(BackColor);
                return true;
            }

            return false;
        }

        private bool TryGetControlCell(Position relativePosition, out Cell cell)
        {
            var sortByZIndexControls = Controls.OrderByDescending(drawable => drawable.ZIndex);

            foreach (Control control in sortByZIndexControls)
            {
                if (control.TryGetCellAt(relativePosition, out cell))
                    return true;
            }

            cell = null;
            return false;
        }

        protected override bool IsInOfTextBoxY(int relativePositionY) => relativePositionY == 0;

        private void SwitchActiveControl()
        {
            if(Controls.Count == 0)
                return;

            Control control = Controls[0];
            Controls.RemoveAt(0);
            Controls.Add(control);

            _activeControl.Unfocus();
            _activeControl = control;
            _activeControl.Focus();
        }
    }
}
