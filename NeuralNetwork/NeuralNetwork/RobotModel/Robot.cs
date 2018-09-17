using System;
using System.Collections.Generic;
using System.Linq;
using NeuralNetwork.Helpers;
using NeuralNetwork.MovementAlgorythims;
using NeuralNetwork.MovementAlgorythims.Enums;
using NeuralNetwork.NeuralNetworkModel;
using NeuralNetwork.ProjectParameters;
using NeuralNetwork.RobotModel.Enums;
using NeuralNetwork.RobotModel.RobotHandlers;
using Explorer = NeuralNetwork.RobotModel.RobotHandlers.Explorer;
using PositionHandler = NeuralNetwork.RobotModel.RobotHandlers.PositionHandler;
using Retreater = NeuralNetwork.RobotModel.RobotHandlers.Retreater;

namespace NeuralNetwork.RobotModel
{
    public class Robot
    {
        public Battery Battery;
        public Network Network;
        public readonly RulingBody RulingBody;

        public Explorer Explorer { get; }
        public Retreater Retreater { get; }

        public PositionHandler PositionHandler { get; }
        public NetworkHandler NetworkHandler { get; }
        public ArrayHandler ArrayHandler { get; }
        public BatteryHandler BatteryHandler { get; }

        public Robot(int maxCapacity, int inputCount, int[] hiddenCounts, int outputCount, 
            int? areaSizeX = null, int? areaSizeY = null, int? startPositionX = null, int? startPositionY = null,
            double? learnRate = null, double? momentum = null)
        {
            Battery = new Battery(maxCapacity);
            Network = new Network(inputCount, hiddenCounts, outputCount, learnRate, momentum);
            RulingBody = new RulingBody(areaSizeX, areaSizeY, startPositionX, startPositionY);
            PositionHandler = new PositionHandler(this);
            Explorer = new Explorer(this);
            Retreater = new Retreater(this);
            NetworkHandler = new NetworkHandler(this);
            ArrayHandler = new ArrayHandler(this);
            BatteryHandler = new BatteryHandler(this);
        }

        public void TestRun()
        {
            for(var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 7; j++)
                {
                    RulingBody.Explorer.Explore();
                    RulingBody.Explorer.ShowExploringArea();
                    Console.WriteLine();
                }
                
                RulingBody.Retreater.Retreat();
                RulingBody.PositionHandler.ChangePositionToStart();
            }
            RulingBody.Retreater.ShowRetreatingArea();
        }
        
        public void TeachRobot()
        {
            var robotDataList = new List<Data>();
            for (var i = 0; i < SimulationParameters.NumberOfExpedicions; i++)
            {
                Explorer.ExploreNumberOfSteps(SimulationParameters.NumberOfExploringSteps);

                //Console.WriteLine("Time to go home!");

                while (!PositionHandler.IsRobotHome())
                {
                    robotDataList.Clear();
                    NetworkHandler.GetNextTeachingData(robotDataList);
                    Network.Trainer.Train(robotDataList, SimulationParameters.NumberOfEpochs);
                }
                //Console.WriteLine();

                RulingBody.PositionHandler.ChangePositionToStart();
            }
            //RulingBody.Retreater.ShowRetreatingArea();
            //Console.WriteLine("Now I now everything");
        }

        public Direction GetOutputDirection(double[] input)
        {
            var output = Network.GetOutput(input);
            var max = output.Concat(new[] {-1d}).Max();
            for (var i = 0; i < output.Length; i++)
            {
                if (max != output[i]) continue;
                switch (i)
                {
                    case 0:
                        return Direction.Right;
                    case 1:
                        return Direction.Left;
                    case 2:
                        return Direction.Above;
                    case 3:
                        return Direction.Below;
                }
            }
            return Direction.None;
        }

        public void Move(Direction direction)
        {
            var success = false;
            switch (direction)
            {
                case Direction.Right:
                    success = RulingBody.Mover.MoveRight(RobotMode.UsingKnowledge);
                    break;
                case Direction.Left:
                    success = RulingBody.Mover.MoveLeft(RobotMode.UsingKnowledge);
                    break;
                case Direction.Above:
                    success = RulingBody.Mover.MoveAbove(RobotMode.UsingKnowledge);
                    break;
                case Direction.Below:
                    success = RulingBody.Mover.MoveBelow(RobotMode.UsingKnowledge);
                    break;
            }
            if (!success) return;
            RulingBody.PositionHandler.IsHome = false;
            Battery.DecreaseLevel();
        }
    }
}