using ConsoleGUIApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace ConsoleGUIApp.Controls.CustomForms
{
    public class PCInfoForm: Form
    {
        private ListBox _names;
        private ListBox _parametrs;

        protected override void Initialize()
        {
            base.Initialize();

            int width = 150;
            int height = 50;
            Size = new Size(width, height);

            _names = new ListBox();

            _names = new ListBox();
            _names.Text = "Объекты";
            _names.Position = new Position(1, 2);
            _names.Size = new Size(width / 4 - 2, height - 4);
            _names.BackColor = ConsoleColor.White;
            _names.TextColor = ConsoleColor.Black;
            _names.BorderColor = ConsoleColor.Black;

            _parametrs = new ListBox();
            _parametrs.Text = "Параметры";
            _parametrs.IsCanFocus = false;
            _parametrs.Position = new Position(width / 4, 2);
            _parametrs.Size = new Size(width * 3 / 4 - 2, height - 4);

            _names.SelectedIndexChanged += OnNamesSelectedIndexChanged;

            List<string> items = new List<string>()
            {
                "Процессор",
                "Видеокарта",
                "Чипсет",
                "Диск",
                "Биос",
                "Кэш",
                "USB",
                "Сеть",
                "Пользователи",
            };

            _names.AddRange(items);
            _names.SetSelectedItem(0);

            Add(_names);
            Add(_parametrs);
        }

        private void OnNamesSelectedIndexChanged(int index)
        {
            string item = _names.Items.ElementAt(index);
            string key = "";
            switch (item)
            {
                case "Процессор":
                    key = "Win32_Processor";
                    break;
                case "Видеокарта":
                    key = "Win32_VideoController";
                    break;
                case "Чипсет":
                    key = "Win32_IDEController";
                    break;
                case "Диск":
                    key = "Win32_DiskDrive";
                    break;
                case "Биос":
                    key = "Win32_BIOS";
                    break;
                case "Оперативная память":
                    key = "Win32_PhysicalMemory";
                    break;
                case "USB":
                    key = "Win32_USBController";
                    break;
                case "Кэш":
                    key = "Win32_CacheMemory";
                    break;
                case "Сеть":
                    key = "Win32_NetworkAdapter";
                    break;
                case "Пользователи":
                    key = "Win32_Account";
                    break;

                default:
                    throw new ArgumentException("item");
            }

            SetHardWareInfo(key);
        }

        private void SetHardWareInfo(string key)
        {
            _parametrs.Clear();

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM " + key);

            foreach (ManagementObject obj in searcher.Get())
            {
                try
                {
                    _parametrs.Text = obj["Name"].ToString().Trim();
                }
                catch (Exception ex)
                {
                    _parametrs.Text = obj.ToString();
                }

                foreach(PropertyData data in obj.Properties)
                {
                    string item = data.Name.ToUpper() + " : ";

                    if(data.Value != null && !string.IsNullOrEmpty(data.Value.ToString()))
                    {
                        switch (data.Value.GetType().ToString())
                        {
                            case "System.String[]":
                                string[] stringData = data.Value as string[];
                                string result = string.Empty;
                                foreach (string str in stringData)
                                    result += $"{str} ";
                                item += result;
                                break;

                            case "System.UInt16[]":
                                ushort[] ushortData = data.Value as ushort[];
                                string result1 = string.Empty;
                                foreach (ushort val in ushortData)
                                    result1 += $"{Convert.ToString(val)}";
                                item += result1;
                                break;
                            default:
                                item += data.Value.ToString();
                                break;
                        }

                        _parametrs.AddItem(item);
                    }
                }

            }
        }
    }
}
