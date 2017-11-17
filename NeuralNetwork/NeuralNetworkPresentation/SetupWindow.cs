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

        public bool SetupInProgress = true;

        public SetupWindow()
        {
            InitializeComponent();
            Size = new Size(500, 300);
            Text = @"Setup";
            Icon = null;
            ShowIcon = false;
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
            numberOfEpochsTextBox.Text = NumberOfExpedicions;
            numberOfExpedicionsTextBox.Text = NumberOfExpedicions;
        }

        private void startSimulationButton_Click(object sender, EventArgs e)
        {
            xPositon = xPositionTextBox.Text;
            yPositon = yPositionTextBox.Text;
            NumberOfExploringSteps = numberOfExploringStepsTextBox.Text;
            NumberOfTestinggSteps = numberOfTestingStepsTextBox.Text;
            NumberOfExpedicions = numberOfEpochsTextBox.Text;
            NumberOfExpedicions = numberOfExpedicionsTextBox.Text;

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
