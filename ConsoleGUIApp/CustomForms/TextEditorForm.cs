using ConsoleGUILib.Controls;
using ConsoleGUILib.Data;

namespace ConsoleGUIApp.CustomForms
{
    public class TextEditorForm : Form
    {
        protected override void Initialize()
        {
            base.Initialize();

            int width = 150;
            int height = 25;
            Size = new Size(width, height);

            TextBox textBox = new TextBox();
            textBox.Size = new Size(width - 2, height - 2);

            Add(textBox);
        }
    }
}
