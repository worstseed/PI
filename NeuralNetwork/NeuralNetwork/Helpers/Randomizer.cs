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

        public static int GetRandomIndex(int length)
        {
            return Random.Next(length);
        }
    }
}