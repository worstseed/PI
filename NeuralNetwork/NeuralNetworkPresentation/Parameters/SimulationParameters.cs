namespace NeuralNetworkPresentation.Parameters
{
    public static class SimulationParameters
    {
        public static double DefaultLearnRate = 0.1;
        public static double DefaultMomentum = 0.4;
        public static int ArrayDefaultSize = 10;
        public static int DefaultStartPositionX = 0;
        public static int DefaultStartPositionY = 4;
        public static int DefaultNumberOfExploringSteps = 15;
        public static int DefaultNumberOfTestingSteps = 15;
        public static int DefaultNumberOfExpedicions = 70;
        public static int DefaultNumberOfEpochs = 20;
        public static int DefaultBatteryMaxCapacity = 300;

        public static int StartPositionX;
        public static int StartPositionY;
        public static int NumberOfExploringSteps;
        public static int NumberOfTestingSteps;
        public static int NumberOfExpedicions;
        public static int NumberOfEpochs;
        public static int BatteryMaxCapacity;
        public static bool SetHorizontalObstacle;
        public static bool SetVerticalObstacle;
        public static bool SetRandomObstacle;

        public static int TeacherLearningTreshold = 30;
        public static double MaximumError = 0.4;
    }
}