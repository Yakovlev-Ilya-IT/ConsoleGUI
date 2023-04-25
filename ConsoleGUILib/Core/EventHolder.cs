using ConsoleGUILib.Controls;
using System;

namespace ConsoleGUILib.Core
{
    public static class EventHolder
    {
        public static event Action<Form> FormHasBeenCreated;
        public static event Action<Form> FormHasBeenClosed;

        public static void SendFormHasBeenCreated(Form form) => FormHasBeenCreated?.Invoke(form);
        public static void SendFormHasBeenClosed(Form form) => FormHasBeenClosed?.Invoke(form);
    }
}
