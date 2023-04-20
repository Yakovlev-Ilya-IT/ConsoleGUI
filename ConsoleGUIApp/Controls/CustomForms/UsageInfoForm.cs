using ConsoleGUIApp.Data;
using ConsoleGUIApp.Utils;
using OpenHardwareMonitor.Hardware;
using System;
using System.Linq;
using System.Threading;

namespace ConsoleGUIApp.Controls.CustomForms
{
    public class UsageInfoForm: Form
    {
        private static Timer _gpuTempTimer;
        private static Timer _gpuLoadTimer;
        private static Timer _cpuLoadTimer;

        protected override void Initialize()
        {
            base.Initialize();

            int width = 40;
            int height = 50;
            Size = new Size(width, height);

            Label gpuTempLabel = new Label();
            gpuTempLabel.Text = "Средняя температура GPU: ";
            gpuTempLabel.Position = new Position(5, 3);
            gpuTempLabel.Size = new Size(gpuTempLabel.Text.Length + 10, 1);

            ProgressBar gpuTempBar = new ProgressBar();
            gpuTempBar.Size = new Size(30, 2);
            gpuTempBar.Position = new Position(5, 5);

            ProgressBarInfo gpuTempInfo = new ProgressBarInfo(HardwareType.GpuNvidia, SensorType.Temperature, gpuTempLabel.Text, gpuTempLabel, gpuTempBar);

            Label gpuLoadLabel = new Label();
            gpuLoadLabel.Text = "Средняя загрузка GPU: ";
            gpuLoadLabel.Position = new Position(5, 8);
            gpuLoadLabel.Size = new Size(gpuLoadLabel.Text.Length + 10, 1);

            ProgressBar gpuLoadBar = new ProgressBar();
            gpuLoadBar.Size = new Size(30, 2);
            gpuLoadBar.Position = new Position(5, 10);

            ProgressBarInfo gpuLoadInfo = new ProgressBarInfo(HardwareType.GpuNvidia, SensorType.Load, gpuLoadLabel.Text, gpuLoadLabel, gpuLoadBar);

            Label cpuLoadLabel = new Label();
            cpuLoadLabel.Text = "Средняя загрузка CPU: ";
            cpuLoadLabel.Position = new Position(5, 13);
            cpuLoadLabel.Size = new Size(cpuLoadLabel.Text.Length + 10, 1);

            ProgressBar cpuLoadBar = new ProgressBar();
            cpuLoadBar.Size = new Size(30, 2);
            cpuLoadBar.Position = new Position(5, 15);

            ProgressBarInfo cpuLoadInfo = new ProgressBarInfo(HardwareType.CPU, SensorType.Load, cpuLoadLabel.Text, cpuLoadLabel, cpuLoadBar);

            Add(gpuTempLabel);
            Add(gpuTempBar);
            Add(gpuLoadLabel);
            Add(gpuLoadBar);
            Add(cpuLoadLabel);
            Add(cpuLoadBar);

            _gpuTempTimer = new Timer(SetSensorValue, gpuTempInfo, 0, 1000);
            _gpuLoadTimer = new Timer(SetSensorValue, gpuLoadInfo, 0, 1000);
            _cpuLoadTimer = new Timer(SetSensorValue, cpuLoadInfo, 0, 1000);
        }

        private void SetSensorValue(object obj)
        {
            ProgressBarInfo info = (ProgressBarInfo)obj;
            float value = 0;

            Visitor visitor = new Visitor();

            Computer computer = new Computer();
            computer.Open();
            switch (info.HardwareType)
            {
                case HardwareType.CPU:
                    computer.CPUEnabled = true;
                    break;

                case HardwareType.GpuNvidia:
                    computer.GPUEnabled = true;
                    break;

                default:
                    throw new InvalidOperationException();
            }
            computer.Accept(visitor);

            IHardware[] hardwares = computer.Hardware.Where(hardware => hardware.HardwareType == info.HardwareType).ToArray();

            foreach (var hardware in hardwares)
            {
                ISensor[] sensors = hardware.Sensors.Where(sensor => sensor.SensorType == info.SensorType).ToArray();

                foreach (var sensor in sensors)
                {
                    value += (float)sensor.Value;
                }

                value /= sensors.Length;
            }

            info.Label.Text = info.Text + string.Format("{0:F2}", value);
            info.ProgressBar.ProgressInPercantage = value;
        }

        private class ProgressBarInfo
        {
            public HardwareType HardwareType;
            public SensorType SensorType;
            public string Text;
            public Label Label;
            public ProgressBar ProgressBar;

            public ProgressBarInfo(HardwareType hardwareType, SensorType sensorType, string text, Label label, ProgressBar progressBar)
            {
                HardwareType = hardwareType;
                SensorType = sensorType;
                Text = text;
                Label = label;
                ProgressBar = progressBar;
            }
        }
    }
}
