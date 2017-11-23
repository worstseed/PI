namespace NeuralNetwork.RobotModel.RobotHandlers
{
    public class PositionHandler
    {
        private readonly Robot _robot;

        public PositionHandler(Robot robot)
        {
            _robot = robot;
        }

        public int GetActualPositionX()
        {
            return _robot.RulingBody.PositionHandler.ActualPositionX;
        }

        public int GetActualPositionY()
        {
            return _robot.RulingBody.PositionHandler.ActualPositionY;
        }

        public bool IsRobotHome()
        {
            return _robot.RulingBody.PositionHandler.IsHome;
        }

        public void ChangePositionToStart()
        {
            _robot.RulingBody.PositionHandler.ChangePositionToStart();
            _robot.Battery.BatteryLevel = _robot.Battery.MaxCapacity;
        }
    }
}