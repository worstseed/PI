using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetwork.NeuralNetworkModel.NetworkHandlers
{
    public class Trainer
    {
        private readonly Network _network;

        public Trainer(Network network)
        {
            _network = network;
        }

        public void Train(List<Data> data, int epochsNumber)
        {
            for (var i = 1; i < epochsNumber + 1; i++)
            {
                //Console.WriteLine("Epoch number: {0}", i);
                foreach (var dataPiece in data)
                {
                    ForwardPropagate(dataPiece.Values);
                    BackwardPropagate(dataPiece.Expectations);
                    //_network.ShowOutput();
                }
                //Console.WriteLine();
            }
        }

        public void Train(List<Data> data, double minimumError)
        {
            var error = 1.0;
            var epochsNumber = 0;

            while (error > minimumError && epochsNumber < int.MaxValue)
            {
                Console.WriteLine("Epoch number: {0}", epochsNumber);
                var errors = new List<double>();
                foreach (var dataPiece in data)
                {
                    ForwardPropagate(dataPiece.Values);
                    BackwardPropagate(dataPiece.Expectations);
                    errors.Add(CalculateError(dataPiece.Expectations));
                    _network.ShowOutput();
                }
                error = errors.Average();
                epochsNumber++;
            }
        }

        public double Train(List<Data> data, int epochsNumber, double maximumError)
        {
            var maxEpochsNumber = epochsNumber;
            var error = 1.0;
            for (var i = 1; i < maxEpochsNumber + 1; i++)
            {
                Console.WriteLine("Epoch number: {0}", i);
                var errors = new List<double>();
                foreach (var dataPiece in data)
                {
                    ForwardPropagate(dataPiece.Values);
                    BackwardPropagate(dataPiece.Expectations);
                    errors.Add(CalculateError(dataPiece.Expectations));
                    _network.ShowOutput();
                }
                error = errors.Average();
                //if (errors.Average() >= maximumError) maxEpochsNumber += 100;
                //if (maxEpochsNumber > 500) break;
                Console.WriteLine();
            }
            return error;
        }

        private double CalculateError(params double[] targets)
        {
            var i = 0;
            return _network.OutputLayer.Sum(a => Math.Abs(a.CalculateError(targets[i++])));
        }

        public void ForwardPropagate(params double[] inputs)
        {
            var i = 0;
            _network.InputLayer.ForEach(x => x.Value = inputs[i++]);
            _network.HiddenLayers.ForEach(x => x.ForEach(y => y.CalculateValue()));
            _network.OutputLayer.ForEach(x => x.CalculateValue());
        }

        private void BackwardPropagate(params double[] targets)
        {
            var i = 0;
            _network.OutputLayer.ForEach(x => x.CalculateGradient(targets[i++]));
            _network.HiddenLayers.Reverse();
            _network.HiddenLayers.ForEach(x => x.ForEach(y => y.CalculateGradient()));
            _network.HiddenLayers.ForEach(x => x.ForEach(y => y.UpdateWeights(_network.LearnRate, _network.Momentum)));
            _network.HiddenLayers.Reverse();
            _network.OutputLayer.ForEach(x => x.UpdateWeights(_network.LearnRate, _network.Momentum));
        }
    }
}