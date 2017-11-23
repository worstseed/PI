namespace NeuralNetwork.RobotModel
{
    public class Battery
    {
        public int MaxCapacity { get; set; }
        public int BatteryLevel { get; set; }
        //public Network Network;

        // Each step costs robot 10 points from its capacity

        public Battery(int maxCapacity)
        {
            MaxCapacity = maxCapacity;
            BatteryLevel = maxCapacity;
            //Network = new Network(3, new[]{25, 25}, 2);
        }

        public void DecreaseLevel()
        {
            BatteryLevel -= 10;
        }
    }
}