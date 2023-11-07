using System;
using System.Windows;
using System.IO.Ports;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Controls;
using System.Collections.Specialized;

namespace StepperControlApp
{
    public partial class MainWindow : Window
    {
        SerialPort serialPort;
        ObservableCollection<string> availablePorts = new ObservableCollection<string>();

        private bool isArrowButtonPressed = false;

        public MainWindow()
        {
            InitializeComponent();

            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                availablePorts.Add(port);
            }

            PortComboBox.ItemsSource = availablePorts;
        }

        //funkcja otwierania wybranego portu COM
        private void OpenPortButton_Click(object sender, RoutedEventArgs e)
        {
            if (PortComboBox.SelectedItem != null)
            {
                string selectedPort = PortComboBox.SelectedItem.ToString();
                serialPort = new SerialPort(selectedPort, 9600);

                try
                {
                    serialPort.Open();
                    StatusTextBlock.Text = $"Port {selectedPort} otwarty.";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Nie można otworzyć portu szeregowego: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Wybierz port przed otwarciem.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ArrowButton_Pressed(object sender, RoutedEventArgs e)
        {
            if (serialPort != null && serialPort.IsOpen)
            {
                isArrowButtonPressed = true;
                string command = ((Button)sender).Tag.ToString();
                serialPort.WriteLine(command);
            }
        }

        private void ArrowButton_Released(object sender, RoutedEventArgs e)
        {
            if (serialPort != null && serialPort.IsOpen)
            {
                isArrowButtonPressed = false;
                serialPort.WriteLine("STOP");
            }
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.Close();
            }
            base.OnClosing(e);
        }


    }
}
