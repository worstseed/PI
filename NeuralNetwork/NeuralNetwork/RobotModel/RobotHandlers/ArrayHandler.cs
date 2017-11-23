namespace NeuralNetwork.RobotModel.RobotHandlers
{
    public class ArrayHandler
    {
        private readonly Robot _robot;

        public ArrayHandler(Robot robot)
        {
            _robot = robot;
        }

        public int GetFieldExploreValue(int x, int y)
        {
            return _robot.RulingBody.DecisionArea.DecisionValuesArea[x, y].ExploringValue;
        }

        public int GetFieldRetreatValue(int x, int y)
        {
            return _robot.RulingBody.DecisionArea.DecisionValuesArea[x, y].RetreatingValue;
        }

        public void SetObstacles(bool horizontal, bool vertical, bool random)
        {
            _robot.RulingBody.DecisionArea.SetObstacles(horizontal, vertical, random);
        }
    }
}