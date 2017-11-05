namespace NeuralNetwork.Helpers
{
    public class DirectionTranslator
    {
        public static readonly double[] Right = {1, 0, 0, 0};
        public static readonly double[] Left = { 0, 1, 0, 0 };
        public static readonly double[] Above = { 0, 0, 1, 0 };
        public static readonly double[] Below = { 0, 0, 0, 1 };
    }
}