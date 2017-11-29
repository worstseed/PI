using System;
using System.Collections.Generic;
using System.Linq;
using NeuralNetwork.NeuralNetworkModel.NetworkHandlers;
using NeuralNetwork.ProjectParameters;

namespace NeuralNetwork.NeuralNetworkModel
{
    public class Network
    {
        public double LearnRate { get; set; }
        public double Momentum { get; set; }

        public List<Neuron> InputLayer { get; set; }
        public List<List<Neuron>> HiddenLayers { get; set; }
        public List<Neuron> OutputLayer { get; set; }

        public Trainer Trainer { get; }

        public Network()
        {
            Trainer = new Trainer(this);
            LearnRate = 0;
            Momentum = 0;
            InputLayer = new List<Neuron>();
            HiddenLayers = new List<List<Neuron>>();
            OutputLayer = new List<Neuron>();
        }

        public Network(int inputCount, int[] hiddenCounts, int outputCount, double? learnRate = null,
            double? momentum = null)
        {
            Trainer = new Trainer(this);
            LearnRate = learnRate ?? NetworkParameters.DefaultLearnRate;
            Momentum = momentum ?? NetworkParameters.DefaultMomentum;

            InputLayer = new List<Neuron>();
            HiddenLayers = new List<List<Neuron>>();
            OutputLayer = new List<Neuron>();

            var layerCreator = new LayerCreator(this);
            layerCreator.CreateInputLayer(inputCount);
            layerCreator.CreateHiddenLayers(hiddenCounts);
            layerCreator.CreateOutputLayer(outputCount);
        }

        public double[] GetOutput(double[] input)
        {
            Trainer.ForwardPropagate(input);
            var temp = new double[OutputLayer.Count];
            for (var i = 0; i < OutputLayer.Count; i++)
                temp[i] = OutputLayer[i].Value;
            return temp;
        }

        public void ShowOutput()
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