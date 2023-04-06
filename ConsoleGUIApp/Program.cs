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
            CustomForm form = new CustomForm();

            form.Position = new Position(3, 3);
            form.Size = new Size(31, 15);
            form.BackFocusedColor = ConsoleColor.Red;
            form.BackUnfocusedColor = ConsoleColor.Gray;
            form.Title = "TITLE2";

            CustomForm form2 = new CustomForm();

            form2.Position = new Position(35, 5);
            form2.Size = new Size(43, 20);
            form2.BackFocusedColor = ConsoleColor.Red;
            form2.BackUnfocusedColor = ConsoleColor.Gray;
            form2.Title = "TITLE22";

            Form form3 = new Form();

            form3.Position = new Position(70, 0);
            form3.Size = new Size(20, 10);
            form3.BackFocusedColor = ConsoleColor.Red;
            form3.BackUnfocusedColor = ConsoleColor.Gray;

            List<Form> forms = new List<Form>()
            {
                form,
                form2,
                form3
            };

            Application application = new Application(ConsoleColor.Black, forms);
            application.Run();
        }
    }
}
