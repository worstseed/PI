namespace NeuralNetwork.RobotModel.RobotHandlers
{
    public class Retreater
    {
        private readonly Robot _robot;

        public Retreater(Robot robot)
        {
            _robot = robot;
        }

        public void RetreatOneStep()
        {
            _robot.RulingBody.Retreater.StepBack();
        }

        public void RetreatRight()
        {
            _robot.RulingBody.Retreater.RetreatRight();
            _robot.Battery.DecreaseLevel();
        }

        public void RetreatLeft()
        {
            _robot.RulingBody.Retreater.RetreatLeft();
            _robot.Battery.DecreaseLevel();
        }

        public void RetreatBelow()
        {
            _robot.RulingBody.Retreater.RetreatBelow();
            _robot.Battery.DecreaseLevel();
        }

        public void RetreatAbove()
        {
            _robot.RulingBody.Retreater.RetreatAbove();
            _robot.Battery.DecreaseLevel();
        }
    }
}