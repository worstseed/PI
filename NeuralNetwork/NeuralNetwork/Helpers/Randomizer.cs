using System;

namespace NeuralNetwork.Helpers
{
    public class Randomizer
    {
        private static readonly Random Random = new Random();

        public static double GetRandom()
        {
            return 2 * Random.NextDouble() - 1;
        }
    }
}