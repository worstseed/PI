namespace NeuralNetwork.RobotModel
{
    public class Battery
    {
        private int MaxCapacity { get; set; }
        private int BatteryLevel { get; set; }

        // Each step costs robot 10 points from its capacity

        public Battery(int maxCapacity)
        {
            MaxCapacity = maxCapacity;
            BatteryLevel = maxCapacity;
        }

    }
}