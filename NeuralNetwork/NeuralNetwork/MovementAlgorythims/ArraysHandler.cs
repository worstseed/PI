using NeuralNetwork.GeneralHelpers;
using NeuralNetwork.MovementAlgorythims.Enums;
using static System.Int32;

namespace NeuralNetwork.MovementAlgorythims
{
    public class ArraysHandler
    {
        private readonly RulingBody _rulingBody;

        public ArraysHandler(int counter, RulingBody rulingBody)
        {
            Counter = counter;
            _rulingBody = rulingBody;
        }

        public int Counter { get; set; }

        public void UpdateRetreatingAreaValue()
        {
            if (_rulingBody.DecisionArea.DecisionValuesArea[_rulingBody.PositionHandler.ActualPositionY, _rulingBody.PositionHandler.ActualPositionX].RetreatingValue >= Counter
                || _rulingBody.DecisionArea.DecisionValuesArea[_rulingBody.PositionHandler.ActualPositionY, _rulingBody.PositionHandler.ActualPositionX].RetreatingValue == -1)
                _rulingBody.DecisionArea.DecisionValuesArea[_rulingBody.PositionHandler.ActualPositionY, _rulingBody.PositionHandler.ActualPositionX].RetreatingValue = Counter;
            Counter++;
        }

        public void UpdateValue(ArrayType arrayType)
        {
            _rulingBody.DecisionArea.UpdateValue(arrayType, _rulingBody.PositionHandler.ActualPositionY, _rulingBody.PositionHandler.ActualPositionX);
        }

        public int GetLengthOfFastesWayHome()
        {
            _rulingBody.PositionHandler.GetSurroundingValues(out int left, out int right, out int above, out int below, ArrayType.Retreating);

            if (right == -1) right = MaxValue;
            if (left == -1) left = MaxValue;
            if (above == -1) above = MaxValue;
            if (below == -1) below = MaxValue;

            var min = Minimizer.FindMinimum(right, left, below, above);
            if (min == -1) return MaxValue;

            if (left == min) return _rulingBody.PositionHandler.GetValueLeft(ArrayType.Retreating);
            if (right == min) return _rulingBody.PositionHandler.GetValueRight(ArrayType.Retreating);
            if (above == min) return _rulingBody.PositionHandler.GetValueAbove(ArrayType.Retreating);
            if (below == min) return _rulingBody.PositionHandler.GetValueBelow(ArrayType.Retreating);

            return MaxValue;
        }
    }
}