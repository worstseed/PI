using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NeuralNetwork.Helpers;

namespace NeuralNetwork.NeuralNetworkModel
{
    public class Network
    {
        public double LearnRate { get; set; }
        public double Momentum { get; set; }

        public List<Neuron> InputLayer { get; set; }
        public List<List<Neuron>> HiddenLayers { get; set; }
        public List<Neuron> OutputLayer { get; set; }

        public Network()
        {
            throw new InvalidDataException();
        }

        public Network(int inputCount, int[] hiddenCounts, int outputCount, double? learnRate = null,
            double? momentum = null)
        {
            LearnRate = learnRate ?? 0.4;
            Momentum = momentum ?? 0.9;

            InputLayer = new List<Neuron>();
            HiddenLayers = new List<List<Neuron>>();
            OutputLayer = new List<Neuron>();

            CreateInputLayer(inputCount);
            CreateHiddenLayers(hiddenCounts);
            CreateOutputLayer(outputCount);
        }

        private void CreateInputLayer(int inputCount)
        {
            for (var i = 0; i < inputCount; i++)
            {
                InputLayer.Add(new Neuron());
            }
        }

        private void CreateHiddenLayers(int[] hiddenCounts)
        {
            var firstHiddenLayer = new List<Neuron>();
            for (var i = 0; i < hiddenCounts[0]; i++)
            {
                firstHiddenLayer.Add(new Neuron(InputLayer));
            }

            HiddenLayers.Add(firstHiddenLayer);

            for (var i = 1; i < hiddenCounts.Length; i++)
            {
                var nextHiddenLayer = new List<Neuron>();
                for (var j = 0; j < hiddenCounts[i]; j++)
                {
                    nextHiddenLayer.Add(new Neuron(HiddenLayers[i - 1]));
                }
                HiddenLayers.Add(nextHiddenLayer);
            }
        }

        private void CreateOutputLayer(int outputCount)
        {
            for (var i = 0; i < outputCount; i++)
            {
                OutputLayer.Add(new Neuron(HiddenLayers.Last()));
            }
        }

        public void Train(List<Data> data, int epochsNumber)
        {
            for (var i = 1; i < epochsNumber + 1; i++)
            {
                Console.WriteLine("Epoch number: {0}", i);
                foreach (var dataPiece in data)
                {
                    ForwardPropagate(dataPiece.Values);
                    BackwardPropagate(dataPiece.Targets);
                    ShowResult();
                }
                Console.WriteLine();
            }
        }

        private void ForwardPropagate(params double[] inputs)
        {
            var i = 0;
            InputLayer.ForEach(x => x.Value = inputs[i++]);
            HiddenLayers.ForEach(x => x.ForEach(y => y.CalculateValue()));
            OutputLayer.ForEach(x => x.CalculateValue());
        }

        private void BackwardPropagate(params double[] targets)
        {
            var i = 0;
            OutputLayer.ForEach(x => x.CalculateGradient(targets[i++]));
            HiddenLayers.Reverse();
            HiddenLayers.ForEach(x => x.ForEach(y => y.CalculateGradient()));
            HiddenLayers.ForEach(x => x.ForEach(y => y.UpdateWeights(LearnRate, Momentum)));
            HiddenLayers.Reverse();
            OutputLayer.ForEach(x => x.UpdateWeights(LearnRate, Momentum));
        }

        private void ShowResult()
        {
            const int precision = 4;
            var pSpecifier = $"F{precision}";
            InputLayer.ForEach(i => Console.Write("{0}\t", i.Value));
            OutputLayer.ForEach(i => Console.Write("{0}\t", i.Value.ToString(pSpecifier)));
            Console.WriteLine();
        }
    }
}