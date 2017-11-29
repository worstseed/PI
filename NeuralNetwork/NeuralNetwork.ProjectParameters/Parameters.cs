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

        public static class EvolutionParameters
        {
            public static int DefaultPopulationSize = 10;
            public static int DefaultNumberOfBests = 3;
            public static int DefaultNumberOfParents = 4;
            public static int DefaultNumberOfChildren = 3;

            public static int DefaultNumberOfGenerations = 3;

            public static int DefaultCrossOverChance = 100;
            public static int DefaultMutationChance = 20;
        }

        public static class RobotParameters
        {
            public static int MaxBateryCapacity = 300;
        }

        public static class FormParameters
        {
            public static int LongSleepTime = 0;
            public static int ShortSleepTime = 0;
        }

        public static class SimulationParameters
        {
            public static double DefaultLearnRate = 0.2;
            public static double DefaultMomentum = 0.9;
            public static int ArrayDefaultSize = 10;
            public static int DefaultStartPositionX = 0;
            public static int DefaultStartPositionY = 4;
            public static int DefaultNumberOfExploringSteps = 15;
            public static int DefaultNumberOfTestingSteps = 15;
            public static int DefaultNumberOfExpedicions = 50; // 70
            public static int DefaultNumberOfEpochs = 20; // 5
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

        public static class GeneralParameters
        {
            public static int ArrayDefaultSize = 10;
            public static bool SetHorizontalObstacle = true;
            public static bool SetVerticalObstacle = true;
            public static bool SetRandomObstacle;
            public static int AverageOfNumber = 1;
        }
    

}