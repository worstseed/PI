using System;
using System.Collections.Generic;
using NeuralNetwork.AreaModel;
using NeuralNetwork.MovementAlgorythims;
using NeuralNetwork.NeuralNetworkModel;
using NeuralNetwork.RobotModel;

namespace NeuralNetwork
{
    internal class XOR_Test
    {
        private static void Main(string[] args)
        {
            //double[,] inputs =
            //{
            //    { 0, 0},
            //    { 0, 1},
            //    { 1, 0},
            //    { 1, 1}
            //};

            //double[] results =
            //{
            //    0, 1, 1, 0
            //};

            //var inputCount = 2;
            //var hiddenCounts = new[] {25, 25};
            //var outputCount = 1;
            //var network = new Network(inputCount, hiddenCounts, outputCount);

            //var dataList = new List<Data>();

            //for (var i = 0; i < 4; i++)
            //{
            //    var values = new[] {inputs[i, 0], inputs[i, 1]};
            //    var targets = new[] {results[i]};
            //    var tmp = new Data(values, targets);

            //    dataList.Add(tmp);
            //}

            //network.Train(dataList, 500);


            Robot _robot = new Robot(100, 2, new []{2, 2}, 4);

            _robot.TestRun();

            Console.ReadKey();
        }
    }
}
