using static System.Math;

namespace NeuralNetwork.NeuralNetworkModel
{
    public static class Sigmoid
    {
        public static double Output(double x)
        {
            return x < -45.0 ? 0.0 : x > 45.0 ? 1.0 : 1.0 / (1.0 + Exp(-x)); //sigmoid
            //return x > 0.0 ? x : 0.01 * x; //leaky relu
            //return 2 / (1 + Pow(E, -(2 * x))) - 1; // tan
            //return Pow(E, Pow(-x, 2)); // gaussian
            //return (1 - Exp(-x)) / (1 + Exp(-x)); // bipolar sigmoid

        }

        public static double Derivative(double x)
        {
            return x * (1 - x); //sigmoid
            //return x > 0.0 ? 1 : 0.01; //leaky relu
            //return 1 - Pow(Tanh(x), 2); // tan
            //return -2 * x * Pow(E, Pow(-x, 2)); // gaussian
            //return 0.5 * (1 + Output(x)) * (1 - Output(x)); // bipolar sigmoid
        }
    }
}