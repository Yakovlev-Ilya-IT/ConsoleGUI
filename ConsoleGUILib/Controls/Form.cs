using ConsoleGUIApp.Core;
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
        private List<Control> _focusingContorols = new List<Control>();
        private List<Control> _unfocusingControls = new List<Control>();

        public override bool IsTopLevel => true;

        public Form()
        {
            UnfocusedBorderStyle = BorderStyle.Single;
            FocusedBorderStyle = BorderStyle.Double;

            Text = "Form";
            Size = new Size(100, 25);

            Initialize();

            EventHolder.SendFormHasBeenCreated(this);
        }

        private IEnumerable<Control> AllControls => _focusingContorols.Union(_unfocusingControls);

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

            if (TryGetControlCell(relativePosition, out cell))
                return true;

            cell = new Cell().WithBackground(BackColor);
            return true;
        }

        public void Add(Control control)
        {
            if (control.IsTopLevel)
                throw new ArgumentException("TopLevelControlAdd");

            if(control.IsCanFocus)
                _focusingContorols.Add(control);
            else
                _unfocusingControls.Add(control);
        }

        public override void Focus()
        {
            base.Focus();

            if (_focusingContorols.Count == 0)
                return;

            if (_activeControl == null)
            {
                _activeControl = _focusingContorols[0];
                _focusingContorols.RemoveAt(0);
                _focusingContorols.Add(_activeControl);
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
            base.OnInput(inputEvent);

            switch (inputEvent.Key.Key)
            {
                case ConsoleKey.Tab:
                    SwitchActiveControl();
                    break;

                case ConsoleKey.Escape:
                    Close();
                    break;

                default:
                    _activeControl?.OnInput(inputEvent);
                    break;
            }
        }

        protected override bool IsInOfTextBoxY(int relativePositionY) => relativePositionY == 0;

        protected void Close() => EventHolder.SendFormHasBeenClosed(this);

        protected virtual void Initialize() { }

        private bool TryGetControlCell(Position relativePosition, out Cell cell)
        {
            var sortByZIndexControls = AllControls.OrderByDescending(drawable => drawable.ZIndex);

            foreach (Control control in sortByZIndexControls)
            {
                if (control.TryGetCellAt(relativePosition, out cell))
                    return true;
            }

            cell = null;
            return false;
        }

        private void SwitchActiveControl()
        {
            if(_focusingContorols.Count == 0)
                return;

            Control control = _focusingContorols[0];
            _focusingContorols.RemoveAt(0);
            _focusingContorols.Add(control);

            _activeControl.Unfocus();
            _activeControl = control;
            _activeControl.Focus();
        }
    }
}
