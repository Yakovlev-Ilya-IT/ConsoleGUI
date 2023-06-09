﻿using ConsoleGUILib.Controls;
using ConsoleGUILib.Data;
using System;

namespace ConsoleGUIApp.CustomForms
{
    public class CustomForm: Form
    {
        public CustomForm(): base()
        {
            Size = new Size(100, 13);
            Position = new Position(Console.WindowWidth / 2 - Size.Width / 2, Console.WindowHeight/ 2 - Size.Height / 2);

            Label label = new Label();
            label.Text = "Что нужно открыть?";
            label.Size = new Size(label.Text.Length, 1);
            label.Position = new Position(Size.Width / 2 - label.Size.Width / 2, 2);

            Button firstButton = new Button();
            firstButton.Text = "Информация о ПК";
            firstButton.Size = new Size(Size.Width / 3, 3);
            firstButton.Position = new Position(Size.Width / 2 - firstButton.Size.Width - 5, 4);
            firstButton.Click += OnFirstButtonClick;

            Button secondButton = new Button();
            secondButton.Text = "Сетевые адаптеры";
            secondButton.Size = new Size(Size.Width / 3, 3);
            secondButton.Position = new Position(Size.Width / 2 + 5, 4);
            secondButton.Click += OnSecondButtonClick;

            Button thirdButton = new Button();
            thirdButton.Text = "Проводник";
            thirdButton.Size = new Size(Size.Width - 24, 3);
            thirdButton.Position = new Position(Size.Width / 2 - thirdButton.Size.Width/2, 8);
            thirdButton.Click += OnThirdButtonClick;

            Add(label);
            Add(firstButton);
            Add(secondButton);
            Add(thirdButton);
        }

        private void OnFirstButtonClick()
        {
            PCInfoForm pCInfoForm = new PCInfoForm();
            pCInfoForm.Text = "Информация о ПК";
            pCInfoForm.Position = new Position(10, 5);
        }

        private void OnSecondButtonClick()
        {
            NetworkInfoForm usageInfoForm = new NetworkInfoForm();
            usageInfoForm.Text = "Сетевые адаптеры";
            usageInfoForm.Position = new Position(40, 10);
        }

        private void OnThirdButtonClick()
        {
            DirectoryManager directoryManager = new DirectoryManager();
            directoryManager.Text = "Проводник";
            directoryManager.Position = new Position(40, 10);
        }
    }
}
