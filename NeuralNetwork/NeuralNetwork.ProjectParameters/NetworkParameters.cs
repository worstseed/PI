namespace NeuralNetwork.ProjectParameters
{
    public static class NetworkParameters
    {
        public static double DefaultLearnRate = 0.2;
        public static double DefaultMomentum = 0.9;
        public static int InputNeuronsCount = 2;
        public static int[] HiddenLayers = {66, 37}; // 66, 37 ; 27, 16; 71, 57
        public static int OutputNeuronsCount = 4;
        public static int MinimumNumberOfLayers = 1;
        public static int MaximumNumberOfLayers = 2;
        public static int MinimumNumberOfNeurons = 0;
        public static int MaximumNumberOfNeurons = 80;
    }
}