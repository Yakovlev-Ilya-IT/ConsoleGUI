using ConsoleGUIApp.Data;
using System;
using System.Text;

namespace ConsoleGUIApp.Core
{
    public static class SafeConsole
    {
        public static Size Size => new Size(Console.WindowWidth, Console.WindowHeight);

		public static void UpdateSize(Size size)
        {
			SetCursorPosition(0, 0);
			SetWindowPosition(0, 0);

            if (size <= Size.Empty)
            {
                SetBufferSize(1, 1);
                SetWindowSize(1, 1);
                return;
            }

            SetBufferSize(size.Width, size.Height);
            SetWindowSize(size.Width, size.Height);
            HideCursor();
            //Clear();
        }

        public static void Write(Position position, Cell cell)
		{
			try
			{
				HideCursor();
				SetCursorPosition(position.X, position.Y);
				Console.BackgroundColor = cell.Background;
				Console.ForegroundColor = cell.Foreground;
				Console.Write(cell.Content);
			}
			catch (Exception)
			{
			}
		}
		public static void SetCursorPosition(int left, int top)
		{
			try
			{
				Console.SetCursorPosition(left, top);
			}
			catch (Exception)
			{ 
			}
		}

		public static void SetWindowPosition(int left, int top)
		{
			try
			{
				Console.SetWindowPosition(left, top);
			}
			catch (Exception)
			{ }
		}

		public static void SetWindowSize(int width, int height)
		{
			try
			{
				Console.SetWindowSize(width, height);
			}
			catch (Exception)
			{ }
		}

		public static void SetBufferSize(int width, int height)
		{
			try
			{
				Console.SetBufferSize(width, height);
			}
			catch (Exception)
			{ }
		}

		public static void SetUtf8()
		{
			try
			{
				Console.OutputEncoding = Encoding.UTF8;
			}
			catch (Exception)
			{ }
		}

		public static void HideCursor()
		{
			try
			{
				Console.CursorVisible = false;
			}
			catch (Exception)
			{ }
		}

		public static void Clear()
		{
			try
			{
				Console.Clear();
			}
			catch (Exception)
			{ }
		}
    }
}
