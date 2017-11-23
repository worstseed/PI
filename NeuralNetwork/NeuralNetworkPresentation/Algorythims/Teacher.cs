using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NeuralNetwork.GeneralHelpers;
using NeuralNetwork.NeuralNetworkModel;
using NeuralNetwork.RobotModel.Enums;
using NeuralNetworkPresentation.Parameters;

namespace NeuralNetworkPresentation.Algorythims
{
    public class Teacher
    {
        private readonly PresentationWindow _presentationWindow;

        public Teacher(PresentationWindow presentationWindow)
        {
            _presentationWindow = presentationWindow;
        }

        public void Teach(object sender, EventArgs e)
        {
            _presentationWindow.Refresh();
            TeachRobot(SimulationParameters.NumberOfExpedicions,
                        SimulationParameters.NumberOfExploringSteps,
                        SimulationParameters.NumberOfEpochs);
        }

        private void TeachRobot(int numberOfExpedicions, int numberOfSteps, int numberOfEpochs)
        {
            var robotDataList = new List<Data>();

            for (var i = 0; i < numberOfExpedicions; i++)
            {
                _presentationWindow.PresentationArrays.MarkActualPosition(MovementType.Explore);
                ExploreNumberOfSteps(numberOfSteps, RobotMode.Learning);
                _presentationWindow.Refresh();
                Thread.Sleep(FormParameters.LongSleepTime);

                while (!_presentationWindow.Robot.PositionHandler.IsRobotHome())
                {
                    _presentationWindow.Refresh();
                    //robotDataList.Clear();
                    Remover.RemoveSameElementsInDataList(robotDataList);
                    _presentationWindow.Robot.NetworkHandler.GetNextTeachingData(robotDataList);
                    //Robot.Train(robotDataList, numberOfEpochs);
                    if (_presentationWindow.Robot.NetworkHandler.Train(robotDataList, numberOfEpochs, SimulationParameters.MaximumError)
                        < SimulationParameters.MaximumError || i < SimulationParameters.TeacherLearningTreshold) //
                        robotDataList.Remove(robotDataList.Last()); //
                    _presentationWindow.PresentationArrays.MarkActualPosition(MovementType.Retreat);
                    _presentationWindow.Controllers.UpdateBatteryLevelValue();
                }
                //Console.WriteLine();

                _presentationWindow.Robot.PositionHandler.ChangePositionToStart();
                _presentationWindow.Refresh();
                Thread.Sleep(FormParameters.LongSleepTime);
            }
            robotDataList.Clear();
        }

        private void SimulateOneStep(RobotMode robotMode)
        {
            if (robotMode == RobotMode.Learning)
            {
                _presentationWindow.PresentationArrays.UpdateExploringArea();
                _presentationWindow.PresentationArrays.UpdateRetreatingArea();
            }

            _presentationWindow.Robot.Explorer.ExploreOneStep();

            if (robotMode == RobotMode.Learning)
            {
                _presentationWindow.PresentationArrays.UpdateExploringArea();
                _presentationWindow.PresentationArrays.UpdateRetreatingArea();
            }

            _presentationWindow.PresentationArrays.PaintExploringArray();
            _presentationWindow.PresentationArrays.PaintRetreatingArray();

            _presentationWindow.Controllers.UpdateBatteryLevelValue();
        }

        private void ExploreNumberOfSteps(int numberOfSteps, RobotMode robotMode)
        {
            for (var i = 1; i < numberOfSteps; i++)
            {
                _presentationWindow.Refresh();
                //Console.Write(@"{0}: ", i);
                SimulateOneStep(robotMode);
                Thread.Sleep(FormParameters.ShortSleepTime);
            }
        }
    }
}