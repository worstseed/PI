using System.IO;

namespace NeuralNetwork.NeuralNetworkModel
{
    public class Data
    {
        public double[] Values { get; set; }
        public double[] Targets { get; set; }

        public Data()
        {
            throw new InvalidDataException();
        }

        public Data(double[] values, double[] targets)
        {
            Values = values;
            Targets = targets;
        }
    }
}