﻿using ConsoleGUIApp.CustomForms;
using ConsoleGUILib.Controls;
using ConsoleGUILib.Core;
using System;
using System.Collections.Generic;

namespace ConsoleGUIApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CustomForm form = new CustomForm();
            form.Text = "Форма выбора";

            List<Form> forms = new List<Form>()
            {
                form,
            };

            Application application = new Application(ConsoleColor.Black, forms);
            application.Run();

            Console.SetCursorPosition(0, 0);
            Console.CursorVisible = false;
        }
    }
}
