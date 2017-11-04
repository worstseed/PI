using System;
using System.IO;
using NeuralNetwork.Helpers;

namespace NeuralNetwork.NeuralNetworkModel
{
    public class Synapse
    {
        public Neuron InputNeuron { get; set; }
        public Neuron OutputNeuron { get; set; }
        public double Weight { get; set; }
        public double WeightDelta { get; set; }

        public Synapse()
        {
            throw new InvalidDataException();
        }

        public Synapse(Neuron inputNeuron, Neuron outputNeuron)
        {
            InputNeuron = inputNeuron;
            OutputNeuron = outputNeuron;
            Weight = Randomizer.GetRandom();
        }

    }
}