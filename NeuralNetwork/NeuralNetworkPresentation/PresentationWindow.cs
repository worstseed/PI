using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using NeuralNetwork.GeneralHelpers;
using NeuralNetwork.MovementAlgorythims;
using NeuralNetwork.NeuralNetworkModel;
using NeuralNetwork.RobotModel;
using NeuralNetwork.TransportingDataHelpers;
using static System.Int32;

namespace NeuralNetworkPresentation
{
    public partial class PresentationWindow : Form
    {
        #region --Simulation Params--

        private int StartPositionX { get; set; }
        private int StartPositionY { get; set; }

        private int NumberOfExploringSteps { get; set; }
        private int NumberOfTestingSteps { get; set; }
        private int NumberOfExpedicions { get; set; }
        private int NumberOfEpochs { get; set; }
        private int BatteryMaxCapacity { get; set; }

        private bool SetHorizontalObstacle { get; set; }
        private bool SetVerticalObstacle { get; set; }
        private bool SetRandomObstacle { get; set; }

        private const int ArrayDefaultSize = 10;

        private const double MaximumError = 0.3;

        #endregion

        #region --Form Params--

        private const int LongSleepTime = 0;
        private const int ShortSleepTime = 0;

        #endregion

        #region --Network Params--

        private const int InputNeuronsCount = 2;
        private readonly int[] HiddenLayers = {32, 64, 64, 64, 32};
        private const int OutputNeuronsCount = 4;

        #endregion

        private readonly Robot Robot;

        private readonly Label[,] ExploringArray;
        private readonly Label[,] RetreatingArray;

        private Button _teachButton;
        private Button _checkButton;
        private Button _exportButton;
        private Button _importButton;

        private Label _batteryLevelText;
        private Label _batteryLevel;

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

            ExploringArray = new Label[ArrayDefaultSize, ArrayDefaultSize];
            RetreatingArray = new Label[ArrayDefaultSize, ArrayDefaultSize];

            SetSimulationParameters();

            Robot = new Robot(
                BatteryMaxCapacity,
                InputNeuronsCount, HiddenLayers, OutputNeuronsCount,
                ArrayDefaultSize, ArrayDefaultSize, StartPositionY, StartPositionX);
            Robot.SetObstacles(SetHorizontalObstacle, SetVerticalObstacle, SetRandomObstacle);
        }

        private void SetSimulationParameters()
        {
            StartPositionX = Parse(SetupWindow.xPositon);
            StartPositionY = Parse(SetupWindow.yPositon);

            NumberOfExploringSteps = Parse(SetupWindow.NumberOfExploringSteps);
            NumberOfTestingSteps = Parse(SetupWindow.NumberOfTestinggSteps);
            NumberOfExpedicions = Parse(SetupWindow.NumberOfExpedicions);
            NumberOfEpochs = Parse(SetupWindow.NumberOfEpochs);
            BatteryMaxCapacity = Parse(SetupWindow.BatteryMaxCapacity);

            SetHorizontalObstacle = SetupWindow.SetHorizontalObstacle;
            SetVerticalObstacle = SetupWindow.SetVerticalObstacle;
            SetRandomObstacle = SetupWindow.SetRandomObstacle;
    }

        public sealed override string Text
        {
            get => base.Text;
            set => base.Text = value;
        }

        private void PresentationWindow_Load(object sender, EventArgs e)
        {
            CreateTeachButton();
            CreateCheckButton();
            CreateImportButton();
            CreateExportButton();

            CreateBatteryLabels();

            CreateExploringArea();
            CreateRetreatingArea();

            PaintExploringArray();
            PaintRetreatingArray();
        }

        #region --Create Controllers---

        private void CreateTeachButton()
        {
            _teachButton = new Button
            {
                Size = new Size(80, 30),
                Location = new Point(350, 20),
                Text = @"Teach",
                TextAlign = ContentAlignment.MiddleCenter,
                Enabled = true
            };
            _teachButton.Click += Teach;
            Controls.Add(_teachButton);
        }

        private void CreateCheckButton()
        {
            _checkButton = new Button
            {
                Size = new Size(80, 30),
                Location = new Point(450, 20),
                Text = @"Check",
                TextAlign = ContentAlignment.MiddleCenter,
                Enabled = true
            };
            _checkButton.Click += Check;
            Controls.Add(_checkButton);
        }

        private void CreateExportButton()
        {
            _exportButton = new Button
            {
                Size = new Size(80, 30),
                Location = new Point(450, 50),
                Text = @"Export",
                TextAlign = ContentAlignment.MiddleCenter,
                Enabled = true
            };
            _exportButton.Click += Export;
            Controls.Add(_exportButton);
        }

        private void CreateImportButton()
        {
            _importButton = new Button
            {
                Size = new Size(80, 30),
                Location = new Point(350, 50),
                Text = @"Import",
                TextAlign = ContentAlignment.MiddleCenter,
                Enabled = true
            };
            _importButton.Click += Import;
            Controls.Add(_importButton);
        }

        private void Export(object sender, EventArgs e)
        {
            ExportHelper.ExportNetwork(Robot.GetNetwork());
        }

        private void Import(object sender, EventArgs e)
        {
            Robot.ImportNetwork(ImportHelper.ImportNetwork());
        }

        private void CreateBatteryLabels()
        {
            _batteryLevelText = new Label
            {
                Size = new Size(180, 30),
                Location = new Point(580, 30),
                Text = @"Battery level: ",
                Font = new Font("TimesNewRoman", 20),
                TextAlign = ContentAlignment.MiddleCenter,
                Enabled = false
            };
            Controls.Add(_batteryLevelText);
            _batteryLevel = new Label
            {
                Size = new Size(80, 30),
                Location = new Point(750, 30),
                Text = BatteryMaxCapacity.ToString(),
                Font = new Font("TimesNewRoman", 20),
                TextAlign = ContentAlignment.MiddleCenter,
                Enabled = false
            };
            Controls.Add(_batteryLevel);
        }

        private void UpdateBatteryLevelValue()
        {
            _batteryLevel.Text = Robot.BatteryLevel().ToString();
            //Console.WriteLine(@"Battery level: {0}", Robot.BatteryLevel());//
        }

        #endregion

        private void Teach(object sender, EventArgs e)
        {
            Refresh();
            TeachRobot(NumberOfExpedicions, NumberOfExploringSteps, NumberOfEpochs);
        }

        private void TeachRobot(int numberOfExpedicions, int numberOfSteps, int numberOfEpochs)
        {
            var robotDataList = new List<Data>();
            for (var i = 0; i < numberOfExpedicions; i++)
            {
                MarkActualPosition(MovementType.Explore);
                ExploreNumberOfSteps(numberOfSteps, RobotMode.Learning);
                Refresh();
                Thread.Sleep(LongSleepTime);

                while (!Robot.IsRobotHome())
                {
                    Refresh();
                    robotDataList.Clear();
                    Robot.GetNextTeachingData(robotDataList);
                    //Robot.Train(robotDataList, numberOfEpochs);
                    Robot.Train(robotDataList, numberOfEpochs, MaximumError); //
                    MarkActualPosition(MovementType.Retreat);
                    UpdateBatteryLevelValue();
                }
                //Console.WriteLine();

                Robot.ChangePositionToStart();
                Refresh();
                Thread.Sleep(LongSleepTime);
            }
            robotDataList.Clear();
        }

        private void Check(object sender, EventArgs e)
        {
            MarkActualPosition(MovementType.Explore);

            //ExploreNumberOfSteps(NumberOfTestingSteps, RobotMode.UsingKnowledge);
            Robot.SetTestPosition();
            MarkActualPosition(MovementType.Explore);
            PaintExploringArray();
            PaintRetreatingArray();

            //Console.WriteLine(@"Time to go home!");
            Refresh();
            Thread.Sleep(LongSleepTime);

            var elapsed = 0d;

            try
            {
                while (!Robot.IsRobotHome() && elapsed < 2000)
                {
                    var timer = new Stopwatch();
                    timer.Start();

                    RetreatUsingNeuralNetwork();

                    timer.Stop();
                    elapsed += timer.Elapsed.TotalMilliseconds;
                }

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Console.WriteLine(@"Something went terribly wrong, my friend :(");
                Robot.ChangePositionToStart();
                Refresh();
                Thread.Sleep(LongSleepTime);
                return;
            }

            Robot.ChangePositionToStart();

            //Console.WriteLine(@"Time to rest, I know everything now.");

            Refresh();
            Thread.Sleep(LongSleepTime);
        }

        private void RetreatUsingNeuralNetwork()
        {
            Refresh();
            var tempDirection = Robot.GetOutputDirection(new double[]
                {Robot.GetActualPositionX(), Robot.GetActualPositionY()});
            Console.WriteLine(@"x: {0}, y: {1}, direction: {2}", Robot.GetActualPositionX(), Robot.GetActualPositionY(),
                tempDirection); //
            Robot.ShowOutput(new double[] { Robot.GetActualPositionX(), Robot.GetActualPositionY() });
            switch (tempDirection)
            {
                case Direction.Right:
                    Robot.RetreatRight();
                    break;
                case Direction.Left:
                    Robot.RetreatLeft();
                    break;
                case Direction.Above:
                    Robot.RetreatAbove();
                    break;
                case Direction.Below:
                    Robot.RetreatBelow();
                    break;
                case Direction.None:
                    throw new Exception("Why am I not moving?");
            }
            MarkActualPosition(MovementType.Retreat);
            UpdateBatteryLevelValue();
        }

        private void SimulateOneStep(RobotMode robotMode)
        {
            if (robotMode == RobotMode.Learning)
            {
                UpdateExploringArea();
                UpdateRetreatingArea();
            }

            Robot.ExploreOneStep();

            if (robotMode == RobotMode.Learning)
            {
                UpdateExploringArea();
                UpdateRetreatingArea();
            }

            PaintExploringArray();
            PaintRetreatingArray();

            UpdateBatteryLevelValue();
        }

        private void ExploreNumberOfSteps(int numberOfSteps, RobotMode robotMode)
        {
            for (var i = 1; i < numberOfSteps; i++)
            {
                Refresh();
                //Console.Write(@"{0}: ", i);
                SimulateOneStep(robotMode);
                Thread.Sleep(ShortSleepTime);
            }
        }


        #region --Arrays Handlers--

        private void CreateExploringArea()
        {
            var horizotal = 30;
            var vertical = 90;
            const int initialValue = 0;

            for (var j = 0; j < ArrayDefaultSize; j++)
            {
                for (var i = 0; i < ArrayDefaultSize; i++)
                {
                    ExploringArray[i, j] = new Label()
                    {
                        Size = new Size(40, 40),
                        Location = new Point(horizotal, vertical),
                        Text = initialValue.ToString(),
                        TextAlign = ContentAlignment.MiddleCenter,
                        BorderStyle = BorderStyle.FixedSingle,
                        Enabled = false
                    };
                    if ((i + 1) % ArrayDefaultSize == 0)
                    {
                        vertical = 90;
                        horizotal = horizotal + 40;
                    }
                    else
                        vertical = vertical + 40;
                    Controls.Add(ExploringArray[i, j]);
                }
            }
        }

        private void CreateRetreatingArea()
        {
            var horizotal = 450;
            var vertical = 90;
            const int initialValue = -1;

            for (var j = 0; j < ArrayDefaultSize; j++)
            {
                for (var i = 0; i < ArrayDefaultSize; i++)
                {
                    RetreatingArray[i, j] = new Label()
                    {
                        Size = new Size(40, 40),
                        Location = new Point(horizotal, vertical),
                        Text = initialValue.ToString(),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Enabled = false,
                        BorderStyle = BorderStyle.FixedSingle
                    };
                    if ((i + 1) % ArrayDefaultSize == 0)
                    {
                        vertical = 90;
                        horizotal = horizotal + 40;
                    }
                    else
                        vertical = vertical + 40;
                    Controls.Add(RetreatingArray[i, j]);
                }
            }
        }

        private void UpdateExploringArea()
        {
            for (var i = 0; i < ArrayDefaultSize; i++)
            {
                for (var j = 0; j < ArrayDefaultSize; j++)
                {
                    if (Parse(ExploringArray[i, j].Text) != Robot.GetFieldExploreValue(i, j))
                    {
                        ExploringArray[i, j].Text = Robot.GetFieldExploreValue(i, j).ToString();
                    }
                }
            }
        }

        private void UpdateRetreatingArea()
        {
            for (var i = 0; i < ArrayDefaultSize; i++)
            {
                for (var j = 0; j < ArrayDefaultSize; j++)
                {
                    if (Parse(RetreatingArray[i, j].Text) != Robot.GetFieldRetreatValue(i, j))
                    {
                        RetreatingArray[i, j].Text = Robot.GetFieldRetreatValue(i, j).ToString();
                    }
                }
            }
        }

        private void MarkActualPosition(MovementType movementType)
        {
            if (movementType == MovementType.Explore)
            {
                ExploringArray[Robot.GetActualPositionY(), Robot.GetActualPositionX()].BackColor = Color.Chartreuse;
                RetreatingArray[Robot.GetActualPositionY(), Robot.GetActualPositionX()].BackColor = Color.Chartreuse;
            }
            else
                RetreatingArray[Robot.GetActualPositionY(), Robot.GetActualPositionX()].BackColor = Color.Red;

        }

        private void PaintExploringArray()
        {
            for (var i = 0; i < ArrayDefaultSize; i++)
            {
                for (var j = 0; j < ArrayDefaultSize; j++)
                {
                    ExploringArray[i, j].BackColor = (Parse(ExploringArray[i, j].Text) == 0
                                                      || Parse(ExploringArray[i, j].Text) == MaxValue)
                        ? Color.Black
                        : Color.BurlyWood;
                }
            }
            MarkActualPosition(MovementType.Explore);
        }

        private void PaintRetreatingArray()
        {
            for (var i = 0; i < ArrayDefaultSize; i++)
            {
                for (var j = 0; j < ArrayDefaultSize; j++)
                {
                    RetreatingArray[i, j].BackColor =
                        Parse(RetreatingArray[i, j].Text) == -1 ? Color.Black : Color.BurlyWood;
                }
            }
            MarkActualPosition(MovementType.Explore);
        }

        #endregion

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            if (Application.MessageLoop)
                Application.Exit();
            else
                Environment.Exit(1);

        }

    }
}