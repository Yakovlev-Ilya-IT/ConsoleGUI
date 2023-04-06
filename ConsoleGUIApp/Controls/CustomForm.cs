using ConsoleGUIApp.Data;
using ConsoleGUIApp.Drawing;
using System;


namespace ConsoleGUIApp.Controls
{
    public class CustomForm: Form
    {
        public CustomForm()
        {
            Form form = new Form();

            form.Position = new Position(2, 2);
            form.Size = new Size(4, 2);
            form.BackFocusedColor = ConsoleColor.Green;
            form.BackUnfocusedColor = ConsoleColor.Yellow;
            form.FocusedBorderStyle = BorderStyle.None;
            form.UnfocusedBorderStyle = BorderStyle.None;

            Form form2 = new Form();

            form2.Position = new Position(9, 2);
            form2.Size = new Size(4, 2);
            form2.BackFocusedColor = ConsoleColor.Green;
            form2.BackUnfocusedColor = ConsoleColor.Yellow;
            form2.FocusedBorderStyle = BorderStyle.None;
            form2.UnfocusedBorderStyle = BorderStyle.None;

            Form form3 = new Form();

            form3.Position = new Position(2, 2);
            form3.Size = new Size(11, 2);
            form3.BackFocusedColor = ConsoleColor.Green;
            form3.BackUnfocusedColor = ConsoleColor.Yellow;
            form3.FocusedBorderStyle = BorderStyle.None;
            form3.UnfocusedBorderStyle = BorderStyle.None;

            Button button = new Button();
            button.Position = new Position(5, 5);
            button.Text = "Я кнопка";
            button.Size = new Size(12, 3);
            button.BackFocusedColor = ConsoleColor.Green;
            button.BackUnfocusedColor = ConsoleColor.Black;
            button.Click += () => Controls.RemoveAt(0);

            Controls.Add(form);
            Controls.Add(form2);
            Controls.Add(form3);
            Controls.Add(button);
        }
    }
}
