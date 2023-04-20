using ConsoleGUIApp.Controls;
using ConsoleGUIApp.Core;
using ConsoleGUIApp.Data;
using System;
using System.Collections.Generic;

namespace ConsoleGUIApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SafeConsole.UpdateSize(new Size(220, 65));
            SafeConsole.SetUtf8();
            CustomForm form = new CustomForm();
            form.Text = "Форма выбора";

            List<Form> forms = new List<Form>()
            {
                form,
            };

            Application application = new Application(ConsoleColor.Black, forms);
            application.Run();
        }
    }
}
