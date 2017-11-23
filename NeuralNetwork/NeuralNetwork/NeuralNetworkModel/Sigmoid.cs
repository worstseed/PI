using System;

namespace NeuralNetwork.NeuralNetworkModel
{
    public static class Sigmoid
    {
        public static double Output(double x)
        {
            return x < -45.0 ? 0.0 : x > 45.0 ? 1.0 : 1.0 / (1.0 + Math.Exp(-x)); //sigmoid
            //return x > 0.0 ? x : 0.01 * x; //leaky relu
        }

        public static double Derivative(double x)
        {
            return x * (1 - x); //sigmoid
            //return x > 0.0 ? 1 : 0.01; //leaky relu
        }
    }
}