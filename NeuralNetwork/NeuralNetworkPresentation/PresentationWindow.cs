using System;
using System.Drawing;
using System.Windows.Forms;
using NeuralNetwork.GeneralHelpers;
using NeuralNetwork.MovementAlgorythims.Enums;
using NeuralNetwork.RobotModel;
using NeuralNetworkPresentation.Algorythims;
using NeuralNetworkPresentation.FormControllers;
using NeuralNetworkPresentation.Parameters;
using NeuralNetworkPresentation.Presentation;

namespace NeuralNetworkPresentation
{
    public sealed partial class PresentationWindow : Form
    {
        public readonly Robot Robot;
        public Controllers Controllers { get; }
        public PresentationArrays PresentationArrays { get; }
        public Teacher Teacher { get; }
        public Checker Checker { get; }

        public PresentationWindow()
        {
            InitializeComponent();

            Size = new Size(900, 545);
            Text = @"Presentation";
            ShowIcon = true;
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MinimizeBox = false;
            MaximizeBox = false;

            Controllers = new Controllers(this);
            PresentationArrays = new PresentationArrays(this);
            Teacher = new Teacher(this);
            Checker = new Checker(this);

            Robot = new Robot(
                SimulationParameters.BatteryMaxCapacity, 
                NetworkParameters.InputNeuronsCount, 
                NetworkParameters.HiddenLayers, 
                NetworkParameters.OutputNeuronsCount, 
                SimulationParameters.ArrayDefaultSize, SimulationParameters.ArrayDefaultSize, 
                SimulationParameters.StartPositionY, SimulationParameters.StartPositionX,
                SimulationParameters.DefaultLearnRate,
                SimulationParameters.DefaultMomentum);

            Robot.ArrayHandler.SetObstacles(
                SimulationParameters.SetHorizontalObstacle,
                SimulationParameters.SetVerticalObstacle,
                SimulationParameters.SetRandomObstacle);
            
        }
        
        private void PresentationWindow_Load(object sender, EventArgs e)
        {
            Controllers.PrepareControllers();
            PresentationArrays.PreparePresentationArrays();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            if (Application.MessageLoop)
                Application.Exit();
            else
                Environment.Exit(1);

        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Down:
                    Robot.Move(Direction.Below);
                    break;
                case Keys.Up:
                    Robot.Move(Direction.Above);
                    break;
                case Keys.Right:
                    Robot.Move(Direction.Right);
                    break;
                case Keys.Left:
                    Robot.Move(Direction.Left);
                    break;
            }
            PresentationArrays.PaintExploringArray();
            PresentationArrays.PaintRetreatingArray();
            PresentationArrays.MarkActualPosition(MovementType.Explore);
            Refresh();
            return true;
        }
    }
}