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
            var dataList = new List<Data>();
            for (var i = 0; i < 5; i++)
            {

                ExploreNumberOfSteps(5);

                Console.WriteLine("Time to go home!");

                while (!IsRobotHome())
                {
                    GetNextTeachingData(dataList);
                }
                Console.WriteLine();

                Network.Train(dataList, 50);

                dataList.Clear();
                RulingBody.ChangePositionToStart();
            }
            RulingBody.ShowRetreatingArea();
            Console.WriteLine("Now I now everything");
        }

        public void GetNextTeachingData(List<Data> dataList)
        {
            var values = new[] {(double) GetActualPositionX(), (double) GetActualPositionY()};
            RulingBody.StepBack();
            double[] targets = null;
            switch (RulingBody.RetreatDirection)
            {
                case Direction.Right:
                    targets = DirectionTranslator.Right;
                    break;
                case Direction.Left:
                    targets = DirectionTranslator.Left;
                    break;
                case Direction.Above:
                    targets = DirectionTranslator.Above;
                    break;
                case Direction.Below:
                    targets = DirectionTranslator.Below;
                    break;
            }
            if (targets == null) throw new Exception("Target is null, lol");
            
            dataList.Add(new Data(values, targets));
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
        }

        public void Train(List<Data> data, int epochsNumber)
        {
            Network.Train(data, epochsNumber);
        }

        public void Train(List<Data> data, double minimumError)
        {
            Network.Train(data, minimumError);
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
       }

        public void RetreatLeft()
        {
            RulingBody.RetreatLeft();
        }

        public void RetreatBelow()
        {
            RulingBody.RetreatBelow();
        }

        public void RetreatAbove()
        {
            RulingBody.RetreatAbove();
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
    }
}