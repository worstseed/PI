using System;
using System.Drawing;
using System.Windows.Forms;
using NeuralNetworkPresentation.Parameters;
using static System.Int32;

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
        }

        private void startSimulationButton_Click(object sender, EventArgs e)
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

        private void cancelButton_Click(object sender, EventArgs e) => Close();
    }
}
