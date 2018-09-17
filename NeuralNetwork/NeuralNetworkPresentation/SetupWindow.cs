using System;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using NeuralNetwork.ProjectParameters;
using static System.Int32;
using static System.String;
using static NeuralNetwork.ProjectParameters.ArloParameters;
using SerialPort = System.IO.Ports.SerialPort;

namespace NeuralNetworkPresentation
{
    public sealed partial class SetupWindow : Form
    {
        public SetupWindow()
        {
            InitializeComponent();
            Size = new Size(500, 400);
            Text = @"Setup";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MinimizeBox = false;
            MaximizeBox = false;
        }

        private void SetupWindow_Load(object sender, EventArgs e)
        {
            xPositionTextBox.Text = SimulationParameters.DefaultStartPositionX.ToString();
            yPositionTextBox.Text = SimulationParameters.DefaultStartPositionY.ToString();
            numberOfExploringStepsTextBox.Text = SimulationParameters.DefaultNumberOfExploringSteps.ToString();
            numberOfTestingStepsTextBox.Text = SimulationParameters.DefaultNumberOfTestingSteps.ToString();
            numberOfEpochsTextBox.Text = SimulationParameters.DefaultNumberOfEpochs.ToString();
            numberOfExpedicionsTextBox.Text = SimulationParameters.DefaultNumberOfExpedicions.ToString();
            batteryMaxCapacityTextBox.Text = SimulationParameters.DefaultBatteryMaxCapacity.ToString();

            if (UseArlo) PrepareSerialPort();
        }

        private void StartSimulationButton_Click(object sender, EventArgs e)
        {
            SimulationParameters.StartPositionX = Parse(xPositionTextBox.Text);
            SimulationParameters.StartPositionY = Parse(yPositionTextBox.Text);
            SimulationParameters.NumberOfExploringSteps = Parse(numberOfExploringStepsTextBox.Text);
            SimulationParameters.NumberOfTestingSteps = Parse(numberOfTestingStepsTextBox.Text);
            SimulationParameters.NumberOfEpochs = Parse(numberOfEpochsTextBox.Text);
            SimulationParameters.NumberOfExpedicions = Parse(numberOfExpedicionsTextBox.Text);
            SimulationParameters.BatteryMaxCapacity = Parse(batteryMaxCapacityTextBox.Text);

            if (setHorizontalObstacleCheckBox.Checked) SimulationParameters.SetHorizontalObstacle = true;
            if (setVerticalObstacleCheckBox.Checked) SimulationParameters.SetVerticalObstacle = true;
            if (setRandomObstacleCheckBox.Checked) SimulationParameters.SetRandomObstacle = true;

            Hide();
            
            var presentationWindow = new PresentationWindow();
            presentationWindow.Show();
        }

        private void CancelButton_Click(object sender, EventArgs e) => Close();

        private static void PrepareSerialPort()
        {
            ArloParameters.SerialPort = new SerialPort("COM5", 115200, Parity.None, 8, StopBits.One)
            {

                //ArloParameters.SerialPort.PortName = SetPortName(ArloParameters.SerialPort.PortName);
                //ArloParameters.SerialPort.BaudRate = SetPortBaudRate(ArloParameters.SerialPort.BaudRate);
                //ArloParameters.SerialPort.Parity = SetPortParity(ArloParameters.SerialPort.Parity);
                //ArloParameters.SerialPort.DataBits = SetPortDataBits(ArloParameters.SerialPort.DataBits);
                //ArloParameters.SerialPort.StopBits = SetPortStopBits(ArloParameters.SerialPort.StopBits);
                //ArloParameters.SerialPort.Handshake = SetPortHandshake(ArloParameters.SerialPort.Handshake);

                //Console.Clear();

                ReadTimeout = 500,
                WriteTimeout = 500
            };

            ArloParameters.SerialPort.Open();

            var readThread = new Thread(Read);


            Continue = true;
            readThread.Start();

            var filestream = new FileStream("out.txt", FileMode.Create);
            var streamwriter = new StreamWriter(filestream) { AutoFlush = true };
            Console.SetOut(streamwriter);
            Console.SetError(streamwriter);

            Thread.Sleep(WaitTime);

            //Thread.Sleep(WaitTime);

            //Continue = false;

            //readThread.Join();
            //ArloParameters.SerialPort.Close();
        }

        public static void Read()
        {
            string[] substrings = { "rst", "dist" };
            while (Continue)
            {
                try
                {
                    var message = ArloParameters.SerialPort.ReadLine();
                    var write = true;

                    foreach (var sub in substrings)
                    {
                        if (message.Contains(sub)) write = false;
                    }

                    if (!write) continue;
                    message = message.Replace("\\r", Empty);
                    message = message.Replace(WriteSpeed.ToString(), Empty);
                    if (message.Length > 1)
                        Console.WriteLine(message);
                    
                }
                catch (TimeoutException) { }
            }
        }

        public static string SetPortName(string defaultPortName)
        {
            Console.WriteLine("Available Ports:");
            //foreach (var s in ArloParameters.SerialPort.GetPortNames())
            //{
            //    Console.WriteLine("   {0}", s);
            //}

            Console.Write("Enter COM port value (Default: {0}): ", defaultPortName);
            var portName = Console.ReadLine();

            if (portName != null && (portName == "" || !(portName.ToLower()).StartsWith("com")))
            {
                portName = defaultPortName;
            }
            return portName;
        }
        public static int SetPortBaudRate(int defaultPortBaudRate)
        {
            Console.Write("Baud Rate(default:{0}): ", defaultPortBaudRate);
            var baudRate = Console.ReadLine();

            if (baudRate == "")
            {
                baudRate = defaultPortBaudRate.ToString();
            }

            return int.Parse(baudRate);
        }
        public static Parity SetPortParity(Parity defaultPortParity)
        {
            Console.WriteLine("Available Parity options:");
            foreach (var s in Enum.GetNames(typeof(Parity)))
            {
                Console.WriteLine("   {0}", s);
            }

            Console.Write("Enter Parity value (Default: {0}):", defaultPortParity.ToString(), true);
            var parity = Console.ReadLine();

            if (parity == "")
            {
                parity = defaultPortParity.ToString();
            }

            return (Parity)Enum.Parse(typeof(Parity), parity, true);
        }
        public static int SetPortDataBits(int defaultPortDataBits)
        {
            Console.Write("Enter DataBits value (Default: {0}): ", defaultPortDataBits);
            var dataBits = Console.ReadLine();

            if (dataBits == "")
            {
                dataBits = defaultPortDataBits.ToString();
            }

            return int.Parse(dataBits.ToUpperInvariant());
        }
        public static StopBits SetPortStopBits(StopBits defaultPortStopBits)
        {
            Console.WriteLine("Available StopBits options:");
            foreach (var s in Enum.GetNames(typeof(StopBits)))
            {
                Console.WriteLine("   {0}", s);
            }

            Console.Write("Enter StopBits value (None is not supported and \n" +
                          "raises an ArgumentOutOfRangeException. \n (Default: {0}):", defaultPortStopBits.ToString());
            var stopBits = Console.ReadLine();

            if (stopBits == "")
            {
                stopBits = defaultPortStopBits.ToString();
            }

            return (StopBits)Enum.Parse(typeof(StopBits), stopBits, true);
        }
        public static Handshake SetPortHandshake(Handshake defaultPortHandshake)
        {
            Console.WriteLine("Available Handshake options:");
            foreach (var s in Enum.GetNames(typeof(Handshake)))
            {
                Console.WriteLine("   {0}", s);
            }

            Console.Write("Enter Handshake value (Default: {0}):", defaultPortHandshake.ToString());
            var handshake = Console.ReadLine();

            if (handshake == "")
            {
                handshake = defaultPortHandshake.ToString();
            }

            return (Handshake)Enum.Parse(typeof(Handshake), handshake, true);
        }
    }
}
