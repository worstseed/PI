using static System.Int32;

namespace NeuralNetworkPresentation.Parameters
{
    public class SimulationParameters
    {
        public int StartPositionX { get; set; }
        public int StartPositionY { get; set; }
        public int NumberOfExploringSteps { get; set; }
        public int NumberOfTestingSteps { get; set; }
        public int NumberOfExpedicions { get; set; }
        public int NumberOfEpochs { get; set; }
        public int BatteryMaxCapacity { get; set; }
        public bool SetHorizontalObstacle { get; set; }
        public bool SetVerticalObstacle { get; set; }
        public bool SetRandomObstacle { get; set; }
        public const int ArrayDefaultSize = 10;
        public const double MaximumError = 0.4;

        public SimulationParameters()
        {
            StartPositionX = Parse(SetupWindow.xPositon);
            StartPositionY = Parse(SetupWindow.yPositon);

            NumberOfExploringSteps = Parse(SetupWindow.NumberOfExploringSteps);
            NumberOfTestingSteps = Parse(SetupWindow.NumberOfTestinggSteps);
            NumberOfExpedicions = Parse(SetupWindow.NumberOfExpedicions);
            NumberOfEpochs = Parse(SetupWindow.NumberOfEpochs);
            BatteryMaxCapacity = Parse(SetupWindow.BatteryMaxCapacity);

            SetHorizontalObstacle = SetupWindow.SetHorizontalObstacle;
            SetVerticalObstacle = SetupWindow.SetVerticalObstacle;
            SetRandomObstacle = SetupWindow.SetRandomObstacle;
        }

        
    }
}