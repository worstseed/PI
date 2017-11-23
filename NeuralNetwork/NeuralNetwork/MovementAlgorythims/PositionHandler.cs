using NeuralNetwork.MovementAlgorythims.Enums;
using static System.Int32;

namespace NeuralNetwork.MovementAlgorythims
{
    public class PositionHandler
    {
        private readonly RulingBody _rulingBody;

        public PositionHandler(int actualPositionY, int actualPositionX, RulingBody rulingBody)
        {
            ActualPositionY = actualPositionY;
            ActualPositionX = actualPositionX;
            _rulingBody = rulingBody;
        }

        public int ActualPositionX { get; set; }
        public int ActualPositionY { get; set; }
        public bool IsHome { get; set; }

        public void CheckIfIsHome()
        {
            if (_rulingBody.DecisionArea.DecisionValuesArea[ActualPositionY, ActualPositionX].RetreatingValue == 0)
                IsHome = true;
        }

        public void ChangePositionToStart()
        {
            _rulingBody.ArraysHandler.Counter = 1;
            ActualPositionY = _rulingBody.DecisionArea.StartPositionX;
            ActualPositionX = _rulingBody.DecisionArea.StartPositionY;
            _rulingBody.ArraysHandler.UpdateValue(ArrayType.Exploring);
        }

        public void GetSurroundingValues(out int left, out int right, out int above, out int below, ArrayType arrayType)
        {
            left = MaxValue;
            right = MaxValue;
            above = MaxValue;
            below = MaxValue;

            if (ThereIsFieldOnTheLeft())
                left = GetValueLeft(arrayType);
            if (ThereIsFieldOnTheRight())
                right = GetValueRight(arrayType);
            if (ThereIsFieldAbove())
                above = GetValueAbove(arrayType);
            if (ThereIsFieldBelow())
                below = GetValueBelow(arrayType);
        }

        public int GetValueRight(ArrayType arrayType)
        {
            return arrayType == ArrayType.Exploring ? _rulingBody.DecisionArea.DecisionValuesArea[ActualPositionY, ActualPositionX + 1].ExploringValue
                : _rulingBody.DecisionArea.DecisionValuesArea[ActualPositionY, ActualPositionX + 1].RetreatingValue;
        }

        public int GetValueLeft(ArrayType arrayType)
        {
            return arrayType == ArrayType.Exploring ? _rulingBody.DecisionArea.DecisionValuesArea[ActualPositionY, ActualPositionX - 1].ExploringValue
                : _rulingBody.DecisionArea.DecisionValuesArea[ActualPositionY, ActualPositionX - 1].RetreatingValue;
        }

        public int GetValueBelow(ArrayType arrayType)
        {
            return arrayType == ArrayType.Exploring ? _rulingBody.DecisionArea.DecisionValuesArea[ActualPositionY + 1, ActualPositionX].ExploringValue
                : _rulingBody.DecisionArea.DecisionValuesArea[ActualPositionY + 1, ActualPositionX].RetreatingValue;
        }

        public int GetValueAbove(ArrayType arrayType)
        {
            return arrayType == ArrayType.Exploring ? _rulingBody.DecisionArea.DecisionValuesArea[ActualPositionY - 1, ActualPositionX].ExploringValue
                : _rulingBody.DecisionArea.DecisionValuesArea[ActualPositionY - 1, ActualPositionX].RetreatingValue;
        }

        public bool ThereIsFieldOnTheRight()
        {
            return ActualPositionX + 1 < _rulingBody.DecisionArea.SizeY;
        }

        public bool ThereIsFieldOnTheLeft()
        {
            return ActualPositionX - 1 >= 0;
        }

        public bool ThereIsFieldBelow()
        {
            return ActualPositionY + 1 < _rulingBody.DecisionArea.SizeX;
        }

        public bool ThereIsFieldAbove()
        {
            return ActualPositionY - 1 >= 0;
        }
    }
}