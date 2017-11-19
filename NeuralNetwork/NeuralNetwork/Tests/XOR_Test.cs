using System.Collections.Generic;
using NeuralNetwork.NeuralNetworkModel;

namespace NeuralNetwork.Tests
{
    class XOR_Test
    {

        private readonly double[,] _inputs;
        private readonly double[] _results;

        public XOR_Test()
        {
            _inputs = new double[,]
            {
                { 0, 0},
                { 0, 1},
                { 1, 0},
                { 1, 1}
            };

            _results = new double[]
            {
                0, 1, 1, 0
            };
        }

        public void RunXORTest()
        {
            const int inputCount = 2;
            var hiddenCounts = new[] { 25, 25 };
            var outputCount = 1;
            var network = new Network(inputCount, hiddenCounts, outputCount);

            var dataList = new List<Data>();

            for (var i = 0; i < 4; i++)
            {
                var values = new[] { _inputs[i, 0], _inputs[i, 1] };
                var targets = new[] { _results[i] };
                var tmp = new Data(values, targets);

                dataList.Add(tmp);
            }

            network.Train(dataList, 0.01);
        }
        

        

        

        
    }
}
