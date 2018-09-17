namespace NeuralNetwork.ProjectParameters
{
    public static class SimulationParameters
    {
        public static double DefaultLearnRate = 0.2;
        public static double DefaultMomentum = 0.9;
        public static int ArrayDefaultSize = 10;
        public static int DefaultStartPositionX = 0;
        public static int DefaultStartPositionY = 4;
        public static int DefaultNumberOfExploringSteps = 5;
        public static int DefaultNumberOfTestingSteps = 20;
        public static int DefaultNumberOfExpedicions = 1; // 50
        public static int DefaultNumberOfEpochs = 5; // 5
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

        public static int TeacherLearningTreshold = 40; // 40
        public static double MaximumError = 0.125;
    }
}