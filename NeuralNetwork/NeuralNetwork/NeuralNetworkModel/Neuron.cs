using System;
using System.Collections.Generic;
using System.Linq;
using NeuralNetwork.GeneralHelpers;

namespace NeuralNetwork.NeuralNetworkModel
{
    public class Neuron
    {
        public Guid Id { get; set; }

        public List<Synapse> InputSynapses { get; set; }
        public List<Synapse> OutputSynapses { get; set; }

        public double Bias { get; set; }
        public double BiasDelta { get; set; }
        public double Gradient { get; set; }
        public double Value { get; set; }

        public Neuron()
        {
            Id = Guid.NewGuid();
            InputSynapses = new List<Synapse>();
            OutputSynapses = new List<Synapse>();
            Bias = Randomizer.GetRandom();
        }


        public Neuron(List<Neuron> inputNeurons) : this()
        {
            foreach (var inputNeuron in inputNeurons)
            {
                var synapse = new Synapse(inputNeuron, this);
                inputNeuron.OutputSynapses.Add(synapse);
                InputSynapses.Add(synapse);
            }
        }

        public virtual double CalculateValue()
        {
            return Value = Sigmoid.Output(InputSynapses.Sum(x => x.Weight * x.InputNeuron.Value) + Bias);
        }

        public double CalculateError(double target)
        {
            return target - Value;
        }

        public double CalculateGradient(double? target = null)
        {
            if (target == null)
                return Gradient = OutputSynapses.Sum(x => x.OutputNeuron.Gradient * x.Weight) *
                                  Sigmoid.Derivative(Value);
            return Gradient = CalculateError(target.Value) * Sigmoid.Derivative(Value);
        }

        public void UpdateWeights(double learnRate, double momentum)
        {
            var previousDelta = BiasDelta;
            BiasDelta = learnRate * Gradient;
            Bias += BiasDelta + momentum * previousDelta;
            foreach (var synapse in InputSynapses)
            {
                previousDelta = synapse.WeightDelta;
                synapse.WeightDelta = learnRate * Gradient * synapse.InputNeuron.Value;
                synapse.Weight += synapse.WeightDelta + momentum * previousDelta;
            }
        }
    }
}