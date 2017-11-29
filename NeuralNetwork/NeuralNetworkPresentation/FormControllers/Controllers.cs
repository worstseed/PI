using System;
using System.Drawing;
using System.Windows.Forms;
using NeuralNetwork.ProjectParameters;
using NeuralNetwork.TransportingDataHelpers;

namespace NeuralNetworkPresentation.FormControllers
{
    public class Controllers
    {
        public PresentationWindow PresentationWindow;
        public Button TeachButton;
        public Button CheckButton;
        public Button ExportButton;
        public Button ImportButton;
        public Label BatteryLevelText;
        public Label BatteryLevel;

        public Controllers(PresentationWindow presentationWindow)
        {
            PresentationWindow = presentationWindow;
        }

        public void PrepareControllers()
        {
            CreateTeachButton();
            CreateCheckButton();
            CreateImportButton();
            CreateExportButton();
            CreateBatteryLabels();
        }

        private void CreateTeachButton()
        {
            TeachButton = new Button
            {
                Size = new Size(80, 30),
                Location = new Point(350, 20),
                Text = @"Teach",
                TextAlign = ContentAlignment.MiddleCenter,
                Enabled = true
            };
            TeachButton.Click += PresentationWindow.Teacher.Teach;
            PresentationWindow.Controls.Add(TeachButton);
        }

        private void CreateCheckButton()
        {
            CheckButton = new Button
            {
                Size = new Size(80, 30),
                Location = new Point(450, 20),
                Text = @"Check",
                TextAlign = ContentAlignment.MiddleCenter,
                Enabled = true
            };
            CheckButton.Click += PresentationWindow.Checker.Check;
            PresentationWindow.Controls.Add(CheckButton);
        }

        private void CreateExportButton()
        {
            ExportButton = new Button
            {
                Size = new Size(80, 30),
                Location = new Point(450, 50),
                Text = @"Export",
                TextAlign = ContentAlignment.MiddleCenter,
                Enabled = true
            };
            ExportButton.Click += Export;
            PresentationWindow.Controls.Add(ExportButton);
        }

        private void CreateImportButton()
        {
            ImportButton = new Button
            {
                Size = new Size(80, 30),
                Location = new Point(350, 50),
                Text = @"Import",
                TextAlign = ContentAlignment.MiddleCenter,
                Enabled = true
            };
            ImportButton.Click += Import;
            PresentationWindow.Controls.Add(ImportButton);
        }

        public void Export(object sender, EventArgs e)
        {
            ExportHelper.ExportNetwork(PresentationWindow.Robot.NetworkHandler.GetNetwork());
        }

        public void Import(object sender, EventArgs e)
        {
            PresentationWindow.Robot.NetworkHandler.ImportNetwork(ImportHelper.ImportNetwork());
        }

        private void CreateBatteryLabels()
        {
            BatteryLevelText = new Label
            {
                Size = new Size(180, 30),
                Location = new Point(580, 30),
                Text = @"Battery level: ",
                Font = new Font("TimesNewRoman", 20),
                TextAlign = ContentAlignment.MiddleCenter,
                Enabled = false
            };
            PresentationWindow.Controls.Add(BatteryLevelText);
            BatteryLevel = new Label
            {
                Size = new Size(80, 30),
                Location = new Point(750, 30),
                Text = SimulationParameters.BatteryMaxCapacity.ToString(),
                Font = new Font("TimesNewRoman", 20),
                TextAlign = ContentAlignment.MiddleCenter,
                Enabled = false
            };
            PresentationWindow.Controls.Add(BatteryLevel);
        }

        public void UpdateBatteryLevelValue()
        {
            BatteryLevel.Text = PresentationWindow.Robot.BatteryHandler.BatteryLevel().ToString();
            //Console.WriteLine(@"Battery level: {0}", Robot.BatteryLevel());//
        }
    }
}