using ConsoleGUILib.Controls;
using ConsoleGUILib.Drawing;
using ConsoleGUILib.Input;

namespace ConsoleGUILib.Core
{
    public class Application: IDrawableContentHolder
    {
        private LinkedList<Form> _forms;
        private Form _activeForm;

        private ConsoleDrawer _drawer;

        private bool _isRunning;

        public Application(ConsoleColor consoleBackColor, List<Form> forms)
        {
            if (forms == null || forms.Count == 0)
                throw new ArgumentNullException(nameof(forms));

            _forms = new LinkedList<Form>(forms);

            _drawer = new ConsoleDrawer(consoleBackColor, this);

            EventHolder.FormHasBeenCreated += OnFormHasBeenCreated;
            EventHolder.FormHasBeenClosed += OnFormHasBeenClosed;

            SwitchActiveForm();
        }

        ~Application()
        {
            EventHolder.FormHasBeenCreated -= OnFormHasBeenCreated;
            EventHolder.FormHasBeenClosed -= OnFormHasBeenClosed;
        }

        public IEnumerable<IDrawable> Drawables => _forms;

        public void Run()
        {
            _isRunning = true;

            while (_isRunning)
            {
                Thread.Sleep(10);
                ReadInput();
                _drawer.AdjustBufferSize();
                _drawer.Draw();
            }
        }

        private void ReadInput()
        {
            while (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Modifiers.HasFlag(ConsoleModifiers.Shift) && key.Key == ConsoleKey.Tab)
                {
                    SwitchActiveForm();
                }
                else
                {
                    _activeForm.OnInput(new KeyInputEvent(key));
                }
            }
        }

        private void SwitchActiveForm()
        {
            if(_activeForm != null)
            {
                if (_activeForm.IsModal)
                    if (_forms.Contains(_activeForm))
                        return;

                _activeForm.Unfocus();
            }

            _activeForm = _forms.First.Value;
            _activeForm.Focus();

            _forms.RemoveFirst();
            _forms.AddLast(_activeForm);
        }

        private void OnFormHasBeenCreated(Form form)
        {
            if (_activeForm != null)
                _activeForm.Unfocus();

            _activeForm = form;
            _activeForm.Focus();
            _forms.AddLast(_activeForm);
        }

        private void OnFormHasBeenClosed(Form closedForm)
        {
            _forms.Remove(closedForm);

            if(_forms.Count == 0)
            {
                _isRunning = false;
                return;
            }

            SwitchActiveForm();
        }
    }
}
