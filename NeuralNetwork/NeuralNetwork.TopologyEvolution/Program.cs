using System;
using NeuralNetwork.ProjectParameters;

namespace NeuralNetwork.TopologyEvolution
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var evolution = new Evolution();
            evolution.SimulateEvolution(EvolutionParameters.DefaultNumberOfGenerations);

            Console.ReadKey();
        }
    }
}
