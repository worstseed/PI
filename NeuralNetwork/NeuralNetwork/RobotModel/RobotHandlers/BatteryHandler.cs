namespace NeuralNetwork.RobotModel.RobotHandlers
{
    public class BatteryHandler
    {
        private readonly Robot _robot;

        public BatteryHandler(Robot robot)
        {
            _robot = robot;
        }

        public int BatteryLevel()
        {
            return _robot.Battery.BatteryLevel;
        }
    }
}