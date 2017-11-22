using System.Drawing;
using System.Windows.Forms;
using NeuralNetwork.GeneralHelpers;
using NeuralNetworkPresentation.Parameters;
using static System.Int32;

namespace NeuralNetworkPresentation.Presentation
{
    public class PresentationArrays
    {
        private readonly PresentationWindow _presentationWindow;
        public readonly Label[,] ExploringArray;
        public readonly Label[,] RetreatingArray;

        public PresentationArrays(PresentationWindow presentationWindow)
        {
            ExploringArray = new Label[SimulationParameters.ArrayDefaultSize, SimulationParameters.ArrayDefaultSize];
            RetreatingArray = new Label[SimulationParameters.ArrayDefaultSize, SimulationParameters.ArrayDefaultSize];
            _presentationWindow = presentationWindow;
        }

        public void PreparePresentationArrays()
        {
            CreateExploringArea();
            CreateRetreatingArea();

            PaintExploringArray();
            PaintRetreatingArray();
        }

        private void CreateExploringArea()
        {
            var horizotal = 30;
            var vertical = 90;
            const int initialValue = 0;

            for (var j = 0; j < SimulationParameters.ArrayDefaultSize; j++)
            {
                for (var i = 0; i < SimulationParameters.ArrayDefaultSize; i++)
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
                    if ((i + 1) % SimulationParameters.ArrayDefaultSize == 0)
                    {
                        vertical = 90;
                        horizotal = horizotal + 40;
                    }
                    else
                        vertical = vertical + 40;
                    _presentationWindow.Controls.Add(ExploringArray[i, j]);
                }
            }
        }

        private void CreateRetreatingArea()
        {
            var horizotal = 450;
            var vertical = 90;
            const int initialValue = -1;

            for (var j = 0; j < SimulationParameters.ArrayDefaultSize; j++)
            {
                for (var i = 0; i < SimulationParameters.ArrayDefaultSize; i++)
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
                    if ((i + 1) % SimulationParameters.ArrayDefaultSize == 0)
                    {
                        vertical = 90;
                        horizotal = horizotal + 40;
                    }
                    else
                        vertical = vertical + 40;
                    _presentationWindow.Controls.Add(RetreatingArray[i, j]);
                }
            }
        }

        public void UpdateExploringArea()
        {
            for (var i = 0; i < SimulationParameters.ArrayDefaultSize; i++)
            {
                for (var j = 0; j < SimulationParameters.ArrayDefaultSize; j++)
                {
                    if (Parse(ExploringArray[i, j].Text) != _presentationWindow.Robot.GetFieldExploreValue(i, j))
                    {
                        ExploringArray[i, j].Text = _presentationWindow.Robot.GetFieldExploreValue(i, j).ToString();
                    }
                }
            }
        }

        public void UpdateRetreatingArea()
        {
            for (var i = 0; i < SimulationParameters.ArrayDefaultSize; i++)
            {
                for (var j = 0; j < SimulationParameters.ArrayDefaultSize; j++)
                {
                    if (Parse(RetreatingArray[i, j].Text) != _presentationWindow.Robot.GetFieldRetreatValue(i, j))
                    {
                        RetreatingArray[i, j].Text = _presentationWindow.Robot.GetFieldRetreatValue(i, j).ToString();
                    }
                }
            }
        }

        public void MarkActualPosition(MovementType movementType)
        {
            if (movementType == MovementType.Explore)
            {
                ExploringArray[_presentationWindow.Robot.GetActualPositionY(), _presentationWindow.Robot.GetActualPositionX()].BackColor = Color.Chartreuse;
                RetreatingArray[_presentationWindow.Robot.GetActualPositionY(), _presentationWindow.Robot.GetActualPositionX()].BackColor = Color.Chartreuse;
            }
            else
                RetreatingArray[_presentationWindow.Robot.GetActualPositionY(), _presentationWindow.Robot.GetActualPositionX()].BackColor = Color.Red;

        }

        public void PaintExploringArray()
        {
            for (var i = 0; i < SimulationParameters.ArrayDefaultSize; i++)
            {
                for (var j = 0; j < SimulationParameters.ArrayDefaultSize; j++)
                {
                    ExploringArray[i, j].BackColor = (Parse(ExploringArray[i, j].Text) == 0
                                                      || Parse(ExploringArray[i, j].Text) == MaxValue)
                        ? Color.Black
                        : Color.BurlyWood;
                }
            }
            MarkActualPosition(MovementType.Explore);
        }

        public void PaintRetreatingArray()
        {
            for (var i = 0; i < SimulationParameters.ArrayDefaultSize; i++)
            {
                for (var j = 0; j < SimulationParameters.ArrayDefaultSize; j++)
                {
                    RetreatingArray[i, j].BackColor =
                        Parse(RetreatingArray[i, j].Text) == -1 ? Color.Black : Color.BurlyWood;
                }
            }
            MarkActualPosition(MovementType.Explore);
        }
    }
}