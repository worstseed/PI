using System.Linq;

namespace NeuralNetwork.Helpers
{
    public class Minimizer
    {
        public static int FindMinimum(int first, int second, int third, int fourth)
        {
            var tmp = new[] { first, second, third, fourth };
            return tmp.Min();
        }
    }
}