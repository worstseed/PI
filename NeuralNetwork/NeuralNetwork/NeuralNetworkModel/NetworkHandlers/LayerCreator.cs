using System.Collections.Generic;
using System.Linq;

namespace NeuralNetwork.NeuralNetworkModel.NetworkHandlers
{
    public class LayerCreator
    {
        private readonly Network _network;

        public LayerCreator(Network network)
        {
            _network = network;
        }

        public void CreateInputLayer(int inputCount)
        {
            for (var i = 0; i < inputCount; i++)
            {
                _network.InputLayer.Add(new Neuron());
            }
        }

        public void CreateHiddenLayers(int[] hiddenCounts)
        {
            var firstHiddenLayer = new List<Neuron>();
            for (var i = 0; i < hiddenCounts[0]; i++)
            {
                firstHiddenLayer.Add(new Neuron(_network.InputLayer));
            }

            _network.HiddenLayers.Add(firstHiddenLayer);

            for (var i = 1; i < hiddenCounts.Length; i++)
            {
                var nextHiddenLayer = new List<Neuron>();
                for (var j = 0; j < hiddenCounts[i]; j++)
                {
                    nextHiddenLayer.Add(new Neuron(_network.HiddenLayers[i - 1]));
                }
                _network.HiddenLayers.Add(nextHiddenLayer);
            }
        }

        public void CreateOutputLayer(int outputCount)
        {
            for (var i = 0; i < outputCount; i++)
            {
                _network.OutputLayer.Add(new Neuron(_network.HiddenLayers.Last()));
            }
        }
    }
}