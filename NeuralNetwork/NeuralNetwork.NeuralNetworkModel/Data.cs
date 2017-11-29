using System.IO;

namespace NeuralNetwork.NeuralNetworkModel
{
    public class Data
    {
        public double[] Values { get; set; }
        public double[] Expectations { get; set; }

        public Data()
        {
            throw new InvalidDataException("Empty data");
        }

        public Data(double[] values, double[] expectations)
        {
            Values = values;
            Expectations = expectations;
        }
    }
}