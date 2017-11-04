namespace NeuralNetwork.RobotModel
{
    public class Battery
    {
        private int MaxCapacity { get; set; }
        private int BatteryLevel { get; set; }

        public Battery(int maxCapacity)
        {
            MaxCapacity = maxCapacity;
            BatteryLevel = maxCapacity;
        }
    }
}