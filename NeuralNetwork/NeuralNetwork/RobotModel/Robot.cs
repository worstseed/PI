using System;
using NeuralNetwork.MovementAlgorythims;
using NeuralNetwork.NeuralNetworkModel;

namespace NeuralNetwork.RobotModel
{
    public class Robot
    {
        private Battery Battery;
        private Network Network;
        private RulingBody RulingBody;

        public Robot(int maxCapacity, int inputCount, int[] hiddenCounts, int outputCount, 
            int? aReaSizeX = null, int? areaSizeY = null, int? startPositionX = null, int? startPositionY = null)
        {
            Battery = new Battery(maxCapacity);
            Network = new Network(inputCount, hiddenCounts, outputCount);
            RulingBody = new RulingBody(aReaSizeX, areaSizeY, startPositionX, startPositionY);
        }

        public void TestRun()
        {
            RulingBody.Explore();
            RulingBody.DecisionArea.ShowExploringArea();
            Console.WriteLine();
            RulingBody.Explore();
            RulingBody.DecisionArea.ShowExploringArea();
            Console.WriteLine();
            RulingBody.Explore();
            RulingBody.DecisionArea.ShowExploringArea();
            Console.WriteLine();
            RulingBody.Retreat();

        }
    }
}