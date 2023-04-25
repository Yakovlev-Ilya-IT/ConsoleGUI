using ConsoleGUIApp.Data;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading;

namespace ConsoleGUIApp.Controls.CustomForms
{
    public class NetworkInfoForm: Form
    {
        private ListBox _names;

        private Label _adapterId;
        private Label _adapterName;
        private Label _adapterDecription;
        private Label _adapterType;
        private Label _adapterAdress;
        private Label _adapterStatus;
        private Label _adapterSpeed;

        private Label _adapterReceivedSpeed;
        private ProgressBar _receivedSpeedBar;

        private Label _adapterSentSpeed;
        private ProgressBar _sentSpeedBar;

        private long _previousBytesReceived;
        private long _previousBytesSent;

        private NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
        private NetworkInterface _currentAdapter;

        private static Timer _timer;

        protected override void Initialize()
        {
            base.Initialize();

            int width = 90;
            int height = 30;
            Size = new Size(width, height);

            _names = new ListBox();

            _names = new ListBox();
            _names.Text = "Объекты";
            _names.Position = new Position(1, 2);
            _names.Size = new Size(width / 5 - 2, height - 4);
            _names.BackColor = ConsoleColor.White;
            _names.TextColor = ConsoleColor.Black;
            _names.BorderColor = ConsoleColor.Black;

            _names.SelectedIndexChanged += OnNamesSelectedIndexChanged;

            List<string> items = new List<string>();

            foreach (var adapter in adapters)
            {
                items.Add(adapter.Name);
            }

            _names.AddRange(items);

            int offset = 2;

            _adapterId = new Label();
            _adapterId.Position = new Position(width / 5, 3);

            _adapterName = new Label();
            _adapterName.Position = new Position(width / 5, 3 + offset);

            _adapterDecription = new Label();
            _adapterDecription.Position = new Position(width / 5, 3 + 2*offset);

            _adapterType = new Label();
            _adapterType.Position = new Position(width / 5, 3 + 3*offset);

            _adapterAdress = new Label();
            _adapterAdress.Position = new Position(width / 5, 3 + 4*offset);

            _adapterStatus = new Label();
            _adapterStatus.Position = new Position(width / 5, 3 + 5*offset);

            _adapterSpeed = new Label();
            _adapterSpeed.Position = new Position(width / 5, 3 + 6 * offset);

            _adapterReceivedSpeed = new Label();
            _adapterReceivedSpeed.Position = new Position(width / 5, 3 + 7 * offset);

            _receivedSpeedBar = new ProgressBar();
            _receivedSpeedBar.Size = new Size(30, 2);
            _receivedSpeedBar.Position = new Position(width / 5, 3 + 7 * offset + 2);

            _adapterSentSpeed = new Label();
            _adapterSentSpeed.Position = new Position(width / 5, 3 + 8 * offset + 3);

            _sentSpeedBar = new ProgressBar();
            _sentSpeedBar.Size = new Size(30, 2);
            _sentSpeedBar.Position = new Position(width / 5, 3 + 8 * offset + 5);

            _names.SetSelectedItem(0);
            Add(_names);
            Add(_adapterId);
            Add(_adapterName);
            Add(_adapterDecription);
            Add(_adapterType);
            Add(_adapterAdress);
            Add(_adapterStatus);
            Add(_adapterSpeed);
            Add(_adapterSentSpeed);
            Add(_sentSpeedBar);
            Add(_receivedSpeedBar);
            Add(_adapterReceivedSpeed);

            _timer = new Timer(UpdateAdapterInfo, null, 0, 1000);
        }

        private void OnNamesSelectedIndexChanged(int index)
        {
            _currentAdapter = adapters[index];

            _previousBytesReceived = _currentAdapter.GetIPStatistics().BytesReceived;
            _previousBytesSent = _currentAdapter.GetIPStatistics().BytesSent;

            _adapterId.Text = $"ID устройства: {_currentAdapter.Id}";
            _adapterId.Size = new Size(_adapterId.Text.Length, 1);

            _adapterName.Text = $"Имя устройства: {_currentAdapter.Name}";
            _adapterName.Size = new Size(_adapterName.Text.Length, 1);

            _adapterDecription.Text = $"Описание: {_currentAdapter.Description}";
            _adapterDecription.Size = new Size(_adapterDecription.Text.Length, 1);

            _adapterType.Text = $"Тип интерфейса: {_currentAdapter.NetworkInterfaceType}";
            _adapterType.Size = new Size(_adapterType.Text.Length, 1);

            _adapterAdress.Text = $"Физический адрес: {_currentAdapter.GetPhysicalAddress()}";
            _adapterAdress.Size = new Size(_adapterAdress.Text.Length, 1);

            _adapterStatus.Text = $"Статус: {_currentAdapter.OperationalStatus}";
            _adapterStatus.Size = new Size(_adapterStatus.Text.Length, 1);

            _adapterSpeed.Text = $"Скорость интерфейса (Мбит/с): {_currentAdapter.Speed / 1000 / 1000}";
            _adapterSpeed.Size = new Size(_adapterSpeed.Text.Length, 1);
        }

        public void UpdateAdapterInfo(object obj)
        {
            IPInterfaceStatistics stats = _currentAdapter.GetIPStatistics();

            float maxSpeed = _currentAdapter.Speed / 1000f / 1000;

            float sentSpeed = (stats.BytesSent - _previousBytesSent) * 8f / 1000 / 1000;
            _adapterSentSpeed.Text = $"Скорость отдачи: {sentSpeed} Мбит/с";
            _adapterSentSpeed.Size = new Size(_adapterSentSpeed.Text.Length, 1);

            float sentSpeedInPercantage = sentSpeed * 100f / maxSpeed;

            if (sentSpeedInPercantage > 100)
                sentSpeedInPercantage = 100;

            _sentSpeedBar.ProgressInPercantage = sentSpeedInPercantage;

            _previousBytesSent = stats.BytesSent;

            float receivedSpeed = (stats.BytesReceived - _previousBytesReceived) * 8f / 1000 / 1000;

            _adapterReceivedSpeed.Text = $"Скорость загрузки: {receivedSpeed} Мбит/с";
            _adapterReceivedSpeed.Size = new Size(_adapterReceivedSpeed.Text.Length, 1);
            _previousBytesReceived = stats.BytesReceived;

            float receivedSpeedInPercantage = receivedSpeed * 100f / maxSpeed;

            if (receivedSpeedInPercantage > 100)
                receivedSpeedInPercantage = 100;

            _receivedSpeedBar.ProgressInPercantage = receivedSpeedInPercantage;
        }
    }
}
