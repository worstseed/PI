using System;
using System.Collections.Generic;

namespace NeuralNetwork.TransportingDataHelpers
{
    
        public class NetworkHelper
        {
            public double LearnRate { get; set; }
            public double Momentum { get; set; }
            public List<NeuronHelper> InputLayer { get; set; }
            public List<List<NeuronHelper>> HiddenLayers { get; set; }
            public List<NeuronHelper> OutputLayer { get; set; }
            public List<SynapseHelper> Synapses { get; set; }

            public NetworkHelper()
            {
                InputLayer = new List<NeuronHelper>();
                HiddenLayers = new List<List<NeuronHelper>>();
                OutputLayer = new List<NeuronHelper>();
                Synapses = new List<SynapseHelper>();
            }
        }

        public class NeuronHelper
        {
            public Guid Id { get; set; }
            public double Bias { get; set; }
            public double BiasDelta { get; set; }
            public double Gradient { get; set; }
            public double Value { get; set; }
        }

        public class SynapseHelper
        {
            public Guid Id { get; set; }
            public Guid OutputNeuronId { get; set; }
            public Guid InputNeuronId { get; set; }
            public double Weight { get; set; }
            public double WeightDelta { get; set; }
        }
 }