using System;

namespace NeuralNetwork.RobotModel.RobotHandlers
{
    public class Explorer
    {
        private readonly Robot _robot;

        public Explorer(Robot robot)
        {
            _robot = robot;
        }

        public void ExploreNumberOfSteps(int numberOfSteps)
        {
            for (var j = 0; j < numberOfSteps; j++)
            {
                _robot.RulingBody.Explorer.Explore();
                _robot.RulingBody.Explorer.ShowExploringArea();
                Console.WriteLine();
            }
        }

        public void ExploreOneStep()
        {
            _robot.RulingBody.Explorer.Explore();
            _robot.Battery.DecreaseLevel();
        }
    }
}