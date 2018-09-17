using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using NeuralNetwork;
using NeuralNetwork.Arlo;
using NeuralNetwork.GeneralHelpers;
using NeuralNetwork.Helpers;
using NeuralNetwork.MovementAlgorythims.Enums;
using NeuralNetwork.NeuralNetworkModel;
using NeuralNetwork.ProjectParameters;
using NeuralNetwork.RobotModel.Enums;

namespace NeuralNetworkPresentation.Algorythims
{
    public class Teacher
    {
        private readonly PresentationWindow _presentationWindow;
        public BackgroundWorker BackgroundWorker; 

        public Teacher(PresentationWindow presentationWindow)
        {
            _presentationWindow = presentationWindow;
            BackgroundWorker = new BackgroundWorker();
            BackgroundWorker.DoWork += Teach;
        }

        public void Teach(object sender, EventArgs e)
        {
            _presentationWindow.Refresh();
            _presentationWindow.Robot.PositionHandler.ChangePositionToStart();
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
                    if (_presentationWindow.Robot.NetworkHandler.Train(robotDataList, numberOfEpochs,
                            SimulationParameters.MaximumError)
                        < SimulationParameters.MaximumError || i < SimulationParameters.TeacherLearningTreshold) //
                    {
                        if (i > 90) robotDataList.Clear(); //
                        else robotDataList.Remove(robotDataList.Last()); //
                    }
                    //if (i >= SimulationParameters.TeacherLearningTreshold)//
                    //{
                    //    _presentationWindow.Robot.Network.Momentum = 0.7; //
                    //}

                        //robotDataList.Remove(robotDataList.Last()); //
                    _presentationWindow.PresentationArrays.MarkActualPosition(MovementType.Retreat);
                    _presentationWindow.Controllers.UpdateBatteryLevelValue();
                }
                //Console.WriteLine();
                if (ArloParameters.UseArlo) ArloController.TurnToStartingPosition();

                var tempx = _presentationWindow.Robot.PositionHandler.GetActualPositionX();
                var tempy = _presentationWindow.Robot.PositionHandler.GetActualPositionY();
                var error = 0d;
                var cnt = 0;

                for (var j = 0; j < 10; j++)
                {
                    for (var k = 0; k < 10; k++)
                    {
                        if (_presentationWindow.Robot.ArrayHandler.GetFieldRetreatValue(k, j) == -1) cnt++;
                        _presentationWindow.Robot.RulingBody.PositionHandler.ActualPositionX = k;
                        _presentationWindow.Robot.RulingBody.PositionHandler.ActualPositionY = j;

                        //error += _presentationWindow.Robot.ArrayHandler.GetFieldRetreatValue(k, j);
                        var dir = _presentationWindow.Robot.RulingBody.Retreater.ChooseDirectionToRetreat();
                        var targetOutput = DirectionTranslator.None;
                        switch (dir)
                        {
                            case Direction.Above:
                                targetOutput = DirectionTranslator.Above;
                                break;
                            case Direction.Below:
                                targetOutput = DirectionTranslator.Below;
                                break;
                            case Direction.Right:
                                targetOutput = DirectionTranslator.Right;
                                break;
                            case Direction.Left:
                                targetOutput = DirectionTranslator.Left;
                                break;
                        }
                        _presentationWindow.Robot.Network.Trainer.ForwardPropagate(new double[] { k, j });
                        error += _presentationWindow.Robot.Network.Trainer.CalculateError(targetOutput);
                    }
                }
                if (cnt == 32) Console.WriteLine(@"{0} {1}", i, error);
                _presentationWindow.Robot.RulingBody.PositionHandler.ActualPositionX = tempx;
                _presentationWindow.Robot.RulingBody.PositionHandler.ActualPositionY = tempy;

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