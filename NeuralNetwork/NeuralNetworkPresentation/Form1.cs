using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using NeuralNetwork.Helpers;
using NeuralNetwork.NeuralNetworkModel;
using NeuralNetwork.RobotModel;

namespace NeuralNetworkPresentation
{
    public partial class Form1 : Form
    {
        private readonly Robot Robot;
        private const int ArrayDefaultSize = 10;

        public Label[,] ExploringArray;
        public Label[,] RetreatingArray;

        private Button _startButton;

        public Form1()
        {
            InitializeComponent();
            Size = new Size(900, 545);
            Text = @"Presentation";
            Icon = null;
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MinimizeBox = false;
            MaximizeBox = false;

            ExploringArray = new Label[ArrayDefaultSize,ArrayDefaultSize];
            RetreatingArray = new Label[ArrayDefaultSize,ArrayDefaultSize];

            Robot = new Robot(100, 2, new[] { 2, 2 }, 4);
        }

        public sealed override string Text
        {
            get => base.Text;
            set => base.Text = value;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateStartButton();
            CreateExploringArea();
            CreateRetreatingArea();

            PaintExploringArray();
            PaintRetreatingArray();
        }
        private void CreateStartButton()
        {
            _startButton = new Button
            {
                Size = new Size(80, 30),
                Location = new Point(400, 20),
                Text = @"Start",
                TextAlign = ContentAlignment.MiddleCenter,
                Enabled = true
            };
            _startButton.Click += StartSimulation;
            Controls.Add(_startButton);
        }

        private void StartSimulation(object sender, EventArgs e)
        {
            Refresh();

            var dataList = new List<Data>();
            for (var i = 0; i < 15; i++)
            {

                MarkActualPosition(MovementType.Explore);

                MakeNumberOfSteps(12);
                    
                Console.WriteLine("Time to ho home!");

                Refresh();

                Thread.Sleep(500);

                while (!Robot.IsRobotHome())
                {
                    Refresh();
                    Robot.GetNextTeachingData(dataList);
                    MarkActualPosition(MovementType.Retreat);
                }
                Console.WriteLine();

                dataList = Remover.RemoveSameElementsInDataList(dataList);

                Robot.Train(dataList, 500);

                //dataList.Clear();
                Robot.ChangePositionToStart();
                Refresh();
                Thread.Sleep(500);
            }
            //Console.WriteLine("Now I now everything");

            dataList.Clear();

            MarkActualPosition(MovementType.Explore);

            MakeNumberOfSteps(18);

            Console.WriteLine("Time to ho home!");

            Refresh();

            Thread.Sleep(500);

            while (!Robot.IsRobotHome())
            {
                Refresh();
                Robot.GetNextTeachingData(dataList);
                MarkActualPosition(MovementType.Retreat);
            }
            Console.WriteLine();

            Robot.Train(dataList, 100);

            //dataList.Clear();
            Robot.ChangePositionToStart();
            Refresh();
            Thread.Sleep(500);
        }

        private void MakeNumberOfSteps(int numberOfSteps)
        {
            for (var i = 1; i < numberOfSteps; i++)
            {
                Refresh();
                Console.Write(@"{0}: ", i);
                SimulateOneStep();
                Thread.Sleep(5);//
            }
        }

        private void SimulateOneStep()
        {
            UpdateExploringArea();
            UpdateRetreatingArea();

            Robot.ExploreOneStep();

            UpdateExploringArea();
            UpdateRetreatingArea();

            PaintExploringArray();
            PaintRetreatingArray();
        }

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
                    if (int.Parse(ExploringArray[i, j].Text) != Robot.GetFieldExploreValue(i, j))
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
                    if (int.Parse(RetreatingArray[i, j].Text) != Robot.GetFieldRetreatValue(i, j))
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
                    ExploringArray[i, j].BackColor = int.Parse(ExploringArray[i, j].Text) == 0 ? Color.Black : Color.BurlyWood;
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
                    RetreatingArray[i, j].BackColor = int.Parse(RetreatingArray[i, j].Text) == -1 ? Color.Black : Color.BurlyWood;
                }
            }
            MarkActualPosition(MovementType.Explore);
        }


        
    }
}