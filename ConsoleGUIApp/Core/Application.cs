using ConsoleGUIApp.Controls;
using ConsoleGUIApp.Drawing;
using ConsoleGUIApp.Input;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleGUIApp.Core
{
    public class Application
    {
        private Queue<Form> _forms;
        private Form _activeForm;

        private ConsoleDrawer _drawer;

        private bool _isRunning;

        public Application(ConsoleColor consoleBackColor, List<Form> forms)
        {
            _forms = new Queue<Form>(forms);

            List<IDrawable> drawables = new List<IDrawable>(forms);
            _drawer = new ConsoleDrawer(consoleBackColor, drawables);

            _activeForm = _forms.Dequeue();
            _activeForm.Focus();
        }

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
                    _activeForm.Unfocus();
                    _forms.Enqueue(_activeForm);
                    _activeForm = _forms.Dequeue();
                    _activeForm.Focus();
                }
                else
                {
                    _activeForm.OnInput(new KeyInputEvent(key));
                }
            }
        }
    }
}
