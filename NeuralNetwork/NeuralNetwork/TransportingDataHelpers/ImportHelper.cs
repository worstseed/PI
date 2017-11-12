using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using NeuralNetwork.NeuralNetworkModel;
using Newtonsoft.Json;

namespace NeuralNetwork.TransportingDataHelpers
{
    public static class ImportHelper
    {
        public static Network ImportNetwork()
        {
            var importingData = GetHelperNetwork();
            if (importingData == null) return null;

            var network = new Network();
            var allNeurons = new List<Neuron>();

            network.LearnRate = importingData.LearnRate;
            network.Momentum = importingData.Momentum;

            foreach (var neuron in importingData.InputLayer)
            {
                var tmpNeuron = new Neuron
                {
                    Id = neuron.Id,
                    Bias = neuron.Bias,
                    BiasDelta = neuron.BiasDelta,
                    Gradient = neuron.Gradient,
                    Value = neuron.Value
                };

                network.InputLayer.Add(tmpNeuron);
                allNeurons.Add(tmpNeuron);
            }

            foreach (var layer in importingData.HiddenLayers)
            {
                var neurons = new List<Neuron>();
                foreach (var neuron in layer)
                {
                    var tmpNeuron = new Neuron
                    {
                        Id = neuron.Id,
                        Bias = neuron.Bias,
                        BiasDelta = neuron.BiasDelta,
                        Gradient = neuron.Gradient,
                        Value = neuron.Value
                    };

                    neurons.Add(tmpNeuron);
                    allNeurons.Add(tmpNeuron);
                }

                network.HiddenLayers.Add(neurons);
            }

            foreach (var neuron in importingData.OutputLayer)
            {
                var tmpNeuron = new Neuron
                {
                    Id = neuron.Id,
                    Bias = neuron.Bias,
                    BiasDelta = neuron.BiasDelta,
                    Gradient = neuron.Gradient,
                    Value = neuron.Value
                };

                network.OutputLayer.Add(tmpNeuron);
                allNeurons.Add(tmpNeuron);
            }

            foreach (var synapse in importingData.Synapses)
            {
                var tmpSynapse = new Synapse { Id = synapse.Id };
                var inputNeuron = allNeurons.First(x => x.Id == synapse.InputNeuronId);
                var outputNeuron = allNeurons.First(x => x.Id == synapse.OutputNeuronId);
                tmpSynapse.InputNeuron = inputNeuron;
                tmpSynapse.OutputNeuron = outputNeuron;
                tmpSynapse.Weight = synapse.Weight;
                tmpSynapse.WeightDelta = synapse.WeightDelta;

                inputNeuron.OutputSynapses.Add(tmpSynapse);
                outputNeuron.InputSynapses.Add(tmpSynapse);
            }

            return network;
        }

        public static List<DataSet> ImportDatasets()
        {
            try
            {
                var dialog = new OpenFileDialog
                {
                    Multiselect = false,
                    Title = "Open Dataset File",
                    Filter = "Text File|*.txt;"
                };

                using (dialog)
                {
                    if (dialog.ShowDialog() != DialogResult.OK) return null;
                    using (var file = File.OpenText(dialog.FileName))
                    {
                        return JsonConvert.DeserializeObject<List<DataSet>>(file.ReadToEnd());
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static NetworkHelper GetHelperNetwork()
        {
            try
            {
                var dialog = new OpenFileDialog
                {
                    Multiselect = false,
                    Title = "Open Network File",
                    Filter = "Text File|*.txt;"
                };

                using (dialog)
                {
                    if (dialog.ShowDialog() != DialogResult.OK) return null;

                    using (var file = File.OpenText(dialog.FileName))
                    {
                        return JsonConvert.DeserializeObject<NetworkHelper>(file.ReadToEnd());
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
