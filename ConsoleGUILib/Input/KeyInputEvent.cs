using System;

namespace ConsoleGUIApp.Input
{
    public class KeyInputEvent
    {
		public ConsoleKeyInfo Key { get; }

		public KeyInputEvent(ConsoleKeyInfo key)
		{
			Key = key;
		}
	}
}
