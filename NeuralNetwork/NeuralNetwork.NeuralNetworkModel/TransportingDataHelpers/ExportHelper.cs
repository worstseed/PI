using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using NeuralNetwork.NeuralNetworkModel;
using Newtonsoft.Json;
using Formatting = System.Xml.Formatting;

namespace NeuralNetwork.TransportingDataHelpers
{
    public static class ExportHelper
    {
        public static void ExportNetwork(Network network)
        {
            var exportingData = GetNetworkHelper(network);

            var dialog = new SaveFileDialog
            {
                Title = "Save Network File",
                Filter = "Text File|*.txt;"
            };

            using (dialog)
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;
                using (var file = File.CreateText(dialog.FileName))
                {
                    var serializer = new JsonSerializer { Formatting = (Newtonsoft.Json.Formatting) Formatting.Indented };
                    serializer.Serialize(file, exportingData);
                }
            }
        }

        private static NetworkHelper GetNetworkHelper(Network network)
        {
            var networkHelper = new NetworkHelper
            {
                LearnRate = network.LearnRate,
                Momentum = network.Momentum
            };

            foreach (var neuron in network.InputLayer)
            {
                var neuronHelper = new NeuronHelper
                {
                    Id = neuron.Id,
                    Bias = neuron.Bias,
                    BiasDelta = neuron.BiasDelta,
                    Gradient = neuron.Gradient,
                    Value = neuron.Value
                };

                networkHelper.InputLayer.Add(neuronHelper);

                foreach (var synapse in neuron.OutputSynapses)
                {
                    var synapseHelper = new SynapseHelper
                    {
                        Id = synapse.Id,
                        OutputNeuronId = synapse.OutputNeuron.Id,
                        InputNeuronId = synapse.InputNeuron.Id,
                        Weight = synapse.Weight,
                        WeightDelta = synapse.WeightDelta
                    };

                    networkHelper.Synapses.Add(synapseHelper);
                }
            }

            foreach (var hiddenLayer in network.HiddenLayers)
            {
                var layer = new List<NeuronHelper>();

                foreach (var neuron in hiddenLayer)
                {
                    var neuronHelper = new NeuronHelper
                    {
                        Id = neuron.Id,
                        Bias = neuron.Bias,
                        BiasDelta = neuron.BiasDelta,
                        Gradient = neuron.Gradient,
                        Value = neuron.Value
                    };

                    layer.Add(neuronHelper);

                    foreach (var synapse in neuron.OutputSynapses)
                    {
                        var synapseHelper = new SynapseHelper
                        {
                            Id = synapse.Id,
                            OutputNeuronId = synapse.OutputNeuron.Id,
                            InputNeuronId = synapse.InputNeuron.Id,
                            Weight = synapse.Weight,
                            WeightDelta = synapse.WeightDelta
                        };

                        networkHelper.Synapses.Add(synapseHelper);
                    }
                }

                networkHelper.HiddenLayers.Add(layer);
            }

            foreach (var neuron in network.OutputLayer)
            {
                var neuronHelper = new NeuronHelper
                {
                    Id = neuron.Id,
                    Bias = neuron.Bias,
                    BiasDelta = neuron.BiasDelta,
                    Gradient = neuron.Gradient,
                    Value = neuron.Value
                };

                networkHelper.OutputLayer.Add(neuronHelper);

                foreach (var synapse in neuron.OutputSynapses)
                {
                    var synapseHelper = new SynapseHelper
                    {
                        Id = synapse.Id,
                        OutputNeuronId = synapse.OutputNeuron.Id,
                        InputNeuronId = synapse.InputNeuron.Id,
                        Weight = synapse.Weight,
                        WeightDelta = synapse.WeightDelta
                    };

                    networkHelper.Synapses.Add(synapseHelper);
                }
            }
            return networkHelper;
        }
    }
}
