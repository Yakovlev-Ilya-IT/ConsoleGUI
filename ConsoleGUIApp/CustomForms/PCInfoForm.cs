using ConsoleGUILib.Controls;
using ConsoleGUILib.Data;
using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleGUIApp.CustomForms
{
    public class PCInfoForm: Form
    {
        private ListBox _names;
        private ListBox _parametrs;

        protected override void Initialize()
        {
            base.Initialize();

            int width = 150;
            int height = 25;
            Size = new Size(width, height);

            _names = new ListBox();

            _names = new ListBox();
            _names.Text = "Объекты";
            _names.Position = new Position(1, 2);
            _names.Size = new Size(width / 6 - 2, height - 4);
            _names.BackColor = ConsoleColor.White;
            _names.TextColor = ConsoleColor.Black;
            _names.BorderColor = ConsoleColor.Black;

            _names.SelectedIndexChanged += OnNamesSelectedIndexChanged;

            List<string> items = new List<string>()
            {
                "Операционная система",
                "Процессор",
                "Логические диски",
                "Стэк вызовов"
            };

            _names.AddRange(items);

            _parametrs = new ListBox();
            _parametrs.Text = "Параметры";
            _parametrs.IsCanFocus = false;
            _parametrs.Position = new Position(width / 6, 2);
            _parametrs.Size = new Size(width * 5 / 6 - 2, height - 4);

            _names.SetSelectedItem(0);
            Add(_names);
            Add(_parametrs);
        }

        private void OnNamesSelectedIndexChanged(int index)
        {
            _parametrs.Clear();

            switch (index)
            {
                case 0:
                    ShowOSInfo();
                    break;

                case 1:
                    ShowProcessorInfo();
                    break;

                case 2:
                    ShowLogicalDrivesInfo();
                    break;

                case 3:
                    ShowStackTrace();
                    break;

                default:
                    break;
            }
        }

        private void ShowOSInfo()
        {
            _parametrs.Text = "Информация об операционной системе";

            List<string> items = new List<string>()
            {
                $"Операционная система:  {Environment.OSVersion}",
                $"Платформа:  {Environment.OSVersion.Platform}",
                $"Версия:  {Environment.OSVersion.Version}",
                string.Format("64 Bit операционная система: {0}", Environment.Is64BitOperatingSystem ? "Да" : "Нет"),
                $"Имя компьютера: {Environment.MachineName}",
                $"Имя пользователя: {Environment.UserName}",
                $"Доменное имя пользователя: {Environment.UserDomainName}",
            };

            _parametrs.AddRange(items);
        }

        private void ShowProcessorInfo()
        {
            _parametrs.Text = "Информация о процессоре";

            List<string> items = new List<string>()
            {
                $"Разрядность процессора: {Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE")}",
                $"Модель процессора: {Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER")}",
                $"Число процессоров: {Environment.ProcessorCount}",
            };

            _parametrs.AddRange(items);
        }

        private void ShowLogicalDrivesInfo()
        {
            _parametrs.Text = "Информация о логических дисках";

            string driveInfo = "";

            foreach (DriveInfo dI in DriveInfo.GetDrives())
            {
                driveInfo += string.Format(
                      "Диск: {0}\r\n" +
                      "Формат диска: {1}\r\n" +
                      "Размер диска (ГБ): {2}\r\n" +
                      "Доступное свободное место (ГБ): {3}\r\n",
                      dI.Name, dI.DriveFormat, (double)dI.TotalSize / 1024 / 1024 / 1024, (double)dI.AvailableFreeSpace / 1024 / 1024 / 1024);
            }

            string[] strings = driveInfo.Split("\r\n");

            List<string> items = new List<string>(strings);

            _parametrs.AddRange(items);
        }

        private void ShowStackTrace()
        {
            _parametrs.Text = "Информация о стэке вызовов";

            string[] strings = Environment.StackTrace.Split("\r\n");

            for (int i = 0; i < strings.Length; i++)
                strings[i] = strings[i].Trim();

            List<string> items = new List<string>(strings);

            _parametrs.AddRange(items);
        }
    }
}
