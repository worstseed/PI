using System;
using System.Drawing;
using System.Windows.Forms;

namespace NeuralNetworkPresentation
{
    public partial class SetupWindow : Form
    {
        public static string xPositon = "0";
        public static string yPositon = "4";
        public static string NumberOfExploringSteps = "15";
        public static string NumberOfTestinggSteps = "15";
        public static string NumberOfExpedicions = "70";
        public static string NumberOfEpochs = "20";
        public static string BatteryMaxCapacity = "300";

        public static bool SetHorizontalObstacle;
        public static bool SetVerticalObstacle;
        public static bool SetRandomObstacle;

        public bool SetupInProgress = true;

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

        public sealed override string Text
        {
            get => base.Text;
            set => base.Text = value;
        }

        private void SetupWindow_Load(object sender, EventArgs e)
        {
            xPositionTextBox.Text = xPositon;
            yPositionTextBox.Text = yPositon;
            numberOfExploringStepsTextBox.Text = NumberOfExploringSteps;
            numberOfTestingStepsTextBox.Text = NumberOfTestinggSteps;
            numberOfEpochsTextBox.Text = NumberOfEpochs;
            numberOfExpedicionsTextBox.Text = NumberOfExpedicions;
            batteryMaxCapacityTextBox.Text = BatteryMaxCapacity;
        }

        private void startSimulationButton_Click(object sender, EventArgs e)
        {
            xPositon = xPositionTextBox.Text;
            yPositon = yPositionTextBox.Text;
            NumberOfExploringSteps = numberOfExploringStepsTextBox.Text;
            NumberOfTestinggSteps = numberOfTestingStepsTextBox.Text;
            NumberOfExpedicions = numberOfEpochsTextBox.Text;
            NumberOfExpedicions = numberOfExpedicionsTextBox.Text;
            BatteryMaxCapacity = batteryMaxCapacityTextBox.Text;

            if (setHorizontalObstacleCheckBox.Checked) SetHorizontalObstacle = true;
            if (setVerticalObstacleCheckBox.Checked) SetVerticalObstacle = true;
            if (setRandomObstacleCheckBox.Checked) SetRandomObstacle = true;

            Hide();
            
            var presentationWindow = new PresentationWindow();
            presentationWindow.Show();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
