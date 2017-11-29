using System;
using System.Collections.Generic;
using NeuralNetwork.Helpers;
using NeuralNetwork.MovementAlgorythims.Enums;
using NeuralNetwork.NeuralNetworkModel;

namespace NeuralNetwork.RobotModel.RobotHandlers
{
    public class NetworkHandler
    {
        private readonly Robot _robot;

        public NetworkHandler(Robot robot)
        {
            _robot = robot;
        }

        public void GetNextTeachingData(List<Data> robotDataList)
        {
            var robotValues = new[] { (double) _robot.PositionHandler.GetActualPositionX(), (double) _robot.PositionHandler.GetActualPositionY() };
            _robot.RulingBody.Retreater.StepBack();
            double[] robotTargets = null;
            switch (_robot.RulingBody.Retreater.RetreatDirection)
            {
                case Direction.Right:
                    robotTargets = DirectionTranslator.Right;
                    break;
                case Direction.Left:
                    robotTargets = DirectionTranslator.Left;
                    break;
                case Direction.Above:
                    robotTargets = DirectionTranslator.Above;
                    break;
                case Direction.Below:
                    robotTargets = DirectionTranslator.Below;
                    break;
            }
            if (robotTargets == null) throw new Exception("Target is null, lol");
            _robot.Battery.DecreaseLevel();
            robotDataList.Add(new Data(robotValues, robotTargets));
        }

        public void Train(List<Data> data, int epochsNumber)
        {
            _robot.Network.Trainer.Train(data, epochsNumber);
        }

        public void Train(List<Data> data, double minimumError)
        {
            _robot.Network.Trainer.Train(data, minimumError);
        }

        public double Train(List<Data> data, int epochsNumber, double maximumError)
        {
            //if (RulingBody.DecisionArea.DecisionValuesArea[GetActualPositionX(), GetActualPositionY()].ExploringValue > 2
            //    && RulingBody.DecisionArea.DecisionValuesArea[GetActualPositionX(), GetActualPositionY()].ExploringValue < 10)
            //    Network.Train(data, epochsNumber, maximumError);
            //else
            //    Network.Train(data, epochsNumber);
            return _robot.Network.Trainer.Train(data, epochsNumber, maximumError);
        }

        public void ShowOutput(double[] input)
        {
            var output = _robot.Network.GetOutput(input);
            for (var i = 0; i < output.Length; i++)
                Console.WriteLine(output[i].ToString());
        }

        public Network GetNetwork()
        {
            return _robot.Network;
        }

        public void ImportNetwork(Network network)
        {
            _robot.Network = network;
        }
    }
}