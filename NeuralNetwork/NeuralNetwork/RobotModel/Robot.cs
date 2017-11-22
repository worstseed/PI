using System;
using System.Collections.Generic;
using System.Linq;
using NeuralNetwork.GeneralHelpers;
using NeuralNetwork.MovementAlgorythims;
using NeuralNetwork.NeuralNetworkModel;

namespace NeuralNetwork.RobotModel
{
    public class Robot
    {
        private Battery Battery;
        private Network Network;
        private readonly RulingBody RulingBody;


        public Robot(int maxCapacity, int inputCount, int[] hiddenCounts, int outputCount, 
            int? areaSizeX = null, int? areaSizeY = null, int? startPositionX = null, int? startPositionY = null)
        {
            Battery = new Battery(maxCapacity);
            Network = new Network(inputCount, hiddenCounts, outputCount);
            RulingBody = new RulingBody(areaSizeX, areaSizeY, startPositionX, startPositionY);
        }

        public void TestRun()
        {
            for(var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 7; j++)
                {
                    RulingBody.Explore();
                    RulingBody.ShowExploringArea();
                    Console.WriteLine();
                }
                
                RulingBody.Retreat();
                RulingBody.ChangePositionToStart();
            }
            RulingBody.ShowRetreatingArea();
        }

        public void TeachRobot()
        {
            var robotDataList = new List<Data>();
            for (var i = 0; i < 5; i++)
            {
                ExploreNumberOfSteps(5);

                Console.WriteLine("Time to go home!");

                while (!IsRobotHome())
                {
                    GetNextTeachingData(robotDataList);
                }
                Console.WriteLine();

                Network.Train(robotDataList, 50);

                robotDataList.Clear();
                RulingBody.ChangePositionToStart();
            }
            RulingBody.ShowRetreatingArea();
            Console.WriteLine("Now I now everything");
        }

        public void GetNextTeachingData(List<Data> robotDataList)
        {
            var robotValues = new[] {(double) GetActualPositionX(), (double) GetActualPositionY()};
            RulingBody.StepBack();
            double[] robotTargets = null;
            switch (RulingBody.RetreatDirection)
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
            Battery.DecreaseLevel();
            robotDataList.Add(new Data(robotValues, robotTargets));
        }

        private void ExploreNumberOfSteps(int numberOfSteps)
        {
            for (var j = 0; j < numberOfSteps; j++)
            {
                RulingBody.Explore();
                RulingBody.ShowExploringArea();
                Console.WriteLine();
            }
        }

        public int GetActualPositionX()
        {
            return RulingBody.ActualPositionX;
        }
        public int GetActualPositionY()
        {
            return RulingBody.ActualPositionY;
        }
        public bool IsRobotHome()
        {
            return RulingBody.IsHome;
        }

        public void ExploreOneStep()
        {
            RulingBody.Explore();
            Battery.DecreaseLevel();
        }
        public void RetreatOneStep()
        {
            RulingBody.StepBack();
        }

        public int GetFieldExploreValue(int x, int y)
        {
            return RulingBody.DecisionArea.DecisionValuesArea[x, y].ExploringValue;
        }

        public int GetFieldRetreatValue(int x, int y)
        {
            return RulingBody.DecisionArea.DecisionValuesArea[x, y].RetreatingValue;
        }

        public void ChangePositionToStart()
        {
            RulingBody.ChangePositionToStart();
            Battery.BatteryLevel = Battery.MaxCapacity;
        }

        public void Train(List<Data> data, int epochsNumber)
        {
            Network.Train(data, epochsNumber);
        }

        public void Train(List<Data> data, double minimumError)
        {
            Network.Train(data, minimumError);
        }

        public double Train(List<Data> data, int epochsNumber, double maximumError)
        {
            //if (RulingBody.DecisionArea.DecisionValuesArea[GetActualPositionX(), GetActualPositionY()].ExploringValue > 2
            //    && RulingBody.DecisionArea.DecisionValuesArea[GetActualPositionX(), GetActualPositionY()].ExploringValue < 10)
            //    Network.Train(data, epochsNumber, maximumError);
            //else
            //    Network.Train(data, epochsNumber);
            return Network.Train(data, epochsNumber, maximumError);
        }

        public void ShowOutput(double[] input)
        {
            var output = Network.GetOutput(input);
            for (var i = 0; i < output.Length; i++)
                Console.WriteLine(output[i].ToString());
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

       public void RetreatRight()
       {
            RulingBody.RetreatRight();
            Battery.DecreaseLevel();
       }

        public void RetreatLeft()
        {
            RulingBody.RetreatLeft();
            Battery.DecreaseLevel();
        }

        public void RetreatBelow()
        {
            RulingBody.RetreatBelow();
            Battery.DecreaseLevel();
        }

        public void RetreatAbove()
        {
            RulingBody.RetreatAbove();
            Battery.DecreaseLevel();
        }

        public Network GetNetwork()
        {
            return Network;
        }

        public void ImportNetwork(Network network)
        {
            Network = network;
        }

        public void SetObstacles(bool horizontal, bool vertical, bool random)
        {
            RulingBody.DecisionArea.SetObstacles(horizontal, vertical, random);
        }

        public int BatteryLevel()
        {
            return Battery.BatteryLevel;
        }

        public void SetTestPosition()
        {
            RulingBody.IsHome = false;
            var random = Randomizer.GetRandom();
            if (random < 0.3)
            {
                RulingBody.ActualPositionX = 1;
                RulingBody.ActualPositionY = 1;
            }
            else if (random < 0.6)
            {
                RulingBody.ActualPositionX = 9;
                RulingBody.ActualPositionY = 8;
            }
            else
            {
                RulingBody.ActualPositionX = 7;
                RulingBody.ActualPositionY = 2;
            }
        }

        public void Move(Direction direction)
        {
            var success = false;
            switch (direction)
            {
                case Direction.Right:
                    success = RulingBody.MoveRight();
                    break;
                case Direction.Left:
                    success = RulingBody.MoveLeft();
                    break;
                case Direction.Above:
                    success = RulingBody.MoveAbove();
                    break;
                case Direction.Below:
                    success = RulingBody.MoveBelow();
                    break;
            }
            if (!success) return;
            RulingBody.IsHome = false;
            Battery.DecreaseLevel();
        }
    }
}