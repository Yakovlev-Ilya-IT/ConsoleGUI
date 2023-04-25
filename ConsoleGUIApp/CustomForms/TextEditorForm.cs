using ConsoleGUILib.Controls;
using ConsoleGUILib.Data;
using System;
using System.IO;

namespace ConsoleGUIApp.CustomForms
{
    public class TextEditorForm : Form
    {
        private FileInfo _fileInfo;
        private TextBox _text;
        private Label _nameLabel;

        public void Initialize(FileInfo fileInfo)
        {
            if (Path.GetExtension(fileInfo.FullName) != ".txt")
                throw new ArgumentException("fileInfo");

            _fileInfo = fileInfo;
            _nameLabel.Text = "Имя файла: " + _fileInfo.Name;

            _text.Text = File.ReadAllText(_fileInfo.FullName);
        }

        protected override void Initialize()
        {
            base.Initialize();

            int width = 130;
            int height = 35;
            Size = new Size(width, height);
            IsModal = true;

            _nameLabel = new Label();
            _nameLabel.Position = new Position(2, 2);
            _nameLabel.Size = new Size(width, 1);

            _text = new TextBox();
            _text.Size = new Size(width - 2, height - 10);
            _text.Position = new Position(1, 4);

            Button saveButton = new Button();
            saveButton.Text = "Сохранить";
            saveButton.Size = new Size(width / 4, 3);
            saveButton.Position = new Position(5, height - 5);

            Button cancelButton = new Button();
            cancelButton.Text = "Отменить";
            cancelButton.Size = new Size(width / 4, 3);
            cancelButton.Position = new Position(width - width / 4 - 5, height - 5);

            saveButton.Click += OnSaveButtonClick;
            cancelButton.Click += OnCancelButtonClick;

            Add(_nameLabel);
            Add(_text);
            Add(saveButton);
            Add(cancelButton);
        }

        private void OnCancelButtonClick()
        {
            Close();
        }

        private void OnSaveButtonClick()
        {
            File.WriteAllText(_fileInfo.FullName, _text.Text);
            Close();
        }
    }
}
