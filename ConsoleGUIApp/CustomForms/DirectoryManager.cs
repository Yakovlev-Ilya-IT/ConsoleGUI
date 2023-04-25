using ConsoleGUILib.Controls;
using ConsoleGUILib.Data;
using ConsoleGUILib.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleGUIApp.CustomForms
{
    public class DirectoryManager : Form
    {
        private Label _currentDirectoryLabel;
        private ListBox _disks;
        private ListBox _directories;
        private string _currentDirectory;

        protected override void Initialize()
        {
            base.Initialize();

            int width = 100;
            int height = 25;
            Size = new Size(width, height);

            _disks = new ListBox();
            _disks.Text = "Диски";
            _disks.Position = new Position(1, 4);
            _disks.Size = new Size(width / 8 - 2, height - 5);

            _currentDirectoryLabel = new Label();
            _currentDirectoryLabel.Position = new Position(1, 2);
            _currentDirectoryLabel.Size = new Size(width - 2, 1);
            _currentDirectoryLabel.BackColor = System.ConsoleColor.DarkYellow;

            _directories = new ListBox();
            _directories.Text = "";
            _directories.Position = new Position(width / 8, 4);
            _directories.Size = new Size(width * 7 / 8 , height - 5);

            _directories.Click += OnDirectoriesClick;

            _currentDirectory = Directory.GetDirectoryRoot(Directory.GetCurrentDirectory());

            _disks.SelectedIndexChanged += OnDisksSelectedIndexChanged;
            SetupDisks();

            Add(_disks);
            Add(_currentDirectoryLabel);
            Add(_directories);
        }

        private void OnDisksSelectedIndexChanged(int index)
        {
            _currentDirectory = _disks.Items.ToList()[index];
            UpadateDirectories();
        }

        private void SetupDisks()
        {
            foreach (DriveInfo dI in DriveInfo.GetDrives())
                _disks.AddItem(dI.Name);

            _disks.SetSelectedItem(0);
        }

        public override void OnInput(KeyInputEvent inputEvent)
        {
            if (inputEvent.Key.Key == ConsoleKey.Backspace)
            {
                if (Directory.GetParent(_currentDirectory) == null)
                    return;

                _currentDirectory = Directory.GetParent(_currentDirectory).FullName;
                UpadateDirectories();
                return;
            }

            base.OnInput(inputEvent);
        }

        private void OnDirectoriesClick(int index)
        {
            if (index < 0)
                return;

            string directorieName = _directories.Items.ToList()[index];

            if (string.IsNullOrEmpty(Path.GetExtension(directorieName)))
            {
                _currentDirectory = Path.Combine(_currentDirectory, directorieName);

                UpadateDirectories();
            }
            else
            {
                if(Path.GetExtension(directorieName) == ".txt")
                {
                    TextEditorForm pCInfoForm = new TextEditorForm();
                    pCInfoForm.Text = "Информация о ПК";
                    pCInfoForm.Position = new Position(15, 10);
                    pCInfoForm.Initialize(new FileInfo(Path.Combine(_currentDirectory, directorieName)));
                }
            }
        }

        private void UpadateDirectories()
        {
            _directories.Clear();

            _currentDirectoryLabel.Text = "Текущая дирректория: " + _currentDirectory;

            DirectoryInfo directoryInfo = new DirectoryInfo(_currentDirectory);

            IEnumerable<DirectoryInfo> dirs = directoryInfo.GetDirectories()
                .Where(s => !s.Attributes.HasFlag(FileAttributes.Hidden));

            foreach (DirectoryInfo dir in dirs)
                _directories.AddItem(dir.Name);

            FileInfo[] files = directoryInfo.GetFiles();

            foreach (FileInfo file in files)
                _directories.AddItem(file.Name);

            if (_directories.Items.Count() > 0)
                _directories.SetSelectedItem(0);
            else
                _directories.SetSelectedItem(-1);
        }
    }
}
