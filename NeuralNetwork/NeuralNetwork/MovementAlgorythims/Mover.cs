using NeuralNetwork.MovementAlgorythims.Enums;
using NeuralNetwork.RobotModel.Enums;
using static System.Int32;

namespace NeuralNetwork.MovementAlgorythims
{
    public class Mover
    {
        private readonly RulingBody _rulingBody;

        public Mover(RulingBody rulingBody)
        {
            _rulingBody = rulingBody;
        }

        public bool MoveRight(RobotMode robotMode)
        {
            if (!_rulingBody.PositionHandler.ThereIsFieldOnTheRight()) return false;
            if (_rulingBody.PositionHandler.GetValueRight(ArrayType.Exploring) == MaxValue) return false;
            _rulingBody.Explorer.ExploreRight(robotMode);
            return true;
        }

        public bool MoveLeft(RobotMode robotMode)
        {
            if (!_rulingBody.PositionHandler.ThereIsFieldOnTheLeft()) return false;
            if (_rulingBody.PositionHandler.GetValueLeft(ArrayType.Exploring) == MaxValue) return false;
            _rulingBody.Explorer.ExploreLeft(robotMode);
            return true;
        }

        public bool MoveAbove(RobotMode robotMode)
        {
            if (!_rulingBody.PositionHandler.ThereIsFieldAbove()) return false;
            if (_rulingBody.PositionHandler.GetValueAbove(ArrayType.Exploring) == MaxValue) return false;
            _rulingBody.Explorer.ExploreAbove(robotMode);
            return true;
        }

        public bool MoveBelow(RobotMode robotMode)
        {
            if (!_rulingBody.PositionHandler.ThereIsFieldBelow()) return false;
            if (_rulingBody.PositionHandler.GetValueBelow(ArrayType.Exploring) == MaxValue) return false;
            _rulingBody.Explorer.ExploreBelow(robotMode);
            return true;
        }
    }
}