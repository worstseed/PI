namespace NeuralNetwork.ProjectParameters
{
    public static class NetworkParameters
    {
        public static double DefaultLearnRate = 0.4;
        public static double DefaultMomentum = 0.9;
        public static int InputNeuronsCount = 2;
        public static int[] HiddenLayers = { 66, 37 };
        public static int OutputNeuronsCount = 4;
        public static int MinimumNumberOfLayers = 2;
        public static int MaximumNumberOfLayers = 3;
        public static int MinimumNumberOfNeurons = 0;
        public static int MaximumNumberOfNeurons = 100;
    }
}