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
            for(var i = 0; i < 40; i++)
            {
                for (var j = 0; j < 20; j++)
                {
                    RulingBody.Explore();
                    RulingBody.DecisionArea.ShowExploringArea();
                    Console.WriteLine();
                }
                
                RulingBody.Retreat();
                RulingBody.ChangePositionToStart();
            }
            RulingBody.DecisionArea.ShowRetreatingArea();
        }
    }
}