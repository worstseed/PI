﻿using System;
using System.Collections.Generic;
using System.Linq;

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
            LearnRate = 0;
            Momentum = 0;
            InputLayer = new List<Neuron>();
            HiddenLayers = new List<List<Neuron>>();
            OutputLayer = new List<Neuron>();
        }

        public Network(int inputCount, int[] hiddenCounts, int outputCount, double? learnRate = null,
            double? momentum = null)
        {
            LearnRate = learnRate ?? 0.2;
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
                    BackwardPropagate(dataPiece.Expectations);
                    ShowResult();
                }
                Console.WriteLine();
            }
        }

        public double[] GetOutput(double[] input)
        {
            ForwardPropagate(input);
            var temp = new double[OutputLayer.Count];
            for (var i = 0; i < OutputLayer.Count; i++)
                temp[i] = OutputLayer[i].Value;
            return temp;
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
                    ShowResult();
                }
                error = errors.Average();
                epochsNumber++;
            }
        }

        public void Train(List<Data> data, int epochsNumber, double maximumError)
        {
            var maxEpochsNumber = epochsNumber;
            for (var i = 1; i < maxEpochsNumber + 1; i++)
            {
                Console.WriteLine("Epoch number: {0}", i);
                var errors = new List<double>();
                foreach (var dataPiece in data)
                {
                    ForwardPropagate(dataPiece.Values);
                    BackwardPropagate(dataPiece.Expectations);
                    errors.Add(CalculateError(dataPiece.Expectations));
                    ShowResult();
                }
                if (errors.Average() >= maximumError) maxEpochsNumber += 10;
                if (maxEpochsNumber > 500) break;
                Console.WriteLine();
            }
        }

        private double CalculateError(params double[] targets)
        {
            var i = 0;
            return OutputLayer.Sum(a => Math.Abs(a.CalculateError(targets[i++])));
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
            //Console.WriteLine("x:  y:    Right:         Left:           Above:          Below:    ");
            InputLayer.ForEach(i => Console.Write("{0, 4}", i.Value));
            Console.Write("\t\t");
            OutputLayer.ForEach(i => Console.Write("{0, 8}\t", i.Value.ToString(pSpecifier)));
            Console.Write("\t\t");
            var max = OutputLayer.Select(t => t.Value).Concat(new double[] {0}).Max();
            for (var i = 1; i < OutputLayer.Count + 1; i++)
            {
                if (max != OutputLayer[i - 1].Value) continue;
                switch (i)
                {
                    case 1:
                        Console.Write("Right");
                        break;
                    case 2:
                        Console.Write("Left");
                        break;
                    case 3:
                        Console.Write("Above");
                        break;
                    case 4:
                        Console.Write("Below");
                        break;
                }
            }
            Console.WriteLine();
        }
    }
}