using System;
using NeuralNetwork.Helpers;
using NeuralNetwork.MovementAlgorythims.Enums;
using static System.Int32;

namespace NeuralNetwork.MovementAlgorythims
{
    public class Retreater
    {
        private readonly RulingBody _rulingBody;

        public Retreater(RulingBody rulingBody)
        {
            _rulingBody = rulingBody;
        }

        public Direction RetreatDirection { get; set; }

        public void Retreat()
        {
            while(!_rulingBody.PositionHandler.IsHome)
                StepBack();
        }

        public void StepBack()
        {
            var directionToRetreat = ChooseDirectionToRetreat();
            //Console.WriteLine(directionToRetreat);//
            RetreatDirection = directionToRetreat;
            switch (directionToRetreat)
            {
                case Direction.Right:
                    RetreatRight();
                    break;
                case Direction.Left:
                    RetreatLeft(); 
                    break;
                case Direction.Above:
                    RetreatAbove(); 
                    break;
                case Direction.Below:
                    RetreatBelow(); 
                    break;
                case Direction.None:
                    throw new Exception("Why am I not moving?");
            }
           
        }

        public Direction ChooseDirectionToRetreat()
        {
            _rulingBody.PositionHandler.GetSurroundingValues(out int left, out int right, out int above, out int below, ArrayType.Retreating);

            if (right == -1) right = MaxValue;
            if (left == -1) left = MaxValue;
            if (above == -1) above = MaxValue;
            if (below == -1) below = MaxValue;

            var min = Minimizer.FindMinimum(right, left, below, above);

            if (left == min) return Direction.Left;
            if (right == min) return Direction.Right;
            if (above == min) return Direction.Above;
            if (below == min) return Direction.Below;

            return Direction.None;
        }

        public void RetreatBelow()
        {
            if (_rulingBody.PositionHandler.ActualPositionY >= _rulingBody.DecisionArea.SizeY - 1) throw new Exception("Out of map, moron (below)");
            _rulingBody.PositionHandler.ActualPositionY++;
            _rulingBody.ArraysHandler.UpdateValue(ArrayType.Retreating);
            _rulingBody.PositionHandler.CheckIfIsHome();
        }

        public void RetreatAbove()
        {
            if (_rulingBody.PositionHandler.ActualPositionY <= 0) throw new Exception("Out of map, moron (above)");
            _rulingBody.PositionHandler.ActualPositionY--;
            _rulingBody.ArraysHandler.UpdateValue(ArrayType.Retreating);
            _rulingBody.PositionHandler.CheckIfIsHome();
        }

        public void RetreatLeft()
        {
            if (_rulingBody.PositionHandler.ActualPositionX <= 0) throw new Exception("Out of map, moron (left)");
            _rulingBody.PositionHandler.ActualPositionX--;
            _rulingBody.ArraysHandler.UpdateValue(ArrayType.Retreating);
            _rulingBody.PositionHandler.CheckIfIsHome();
        }

        public void RetreatRight()
        {
            if (_rulingBody.PositionHandler.ActualPositionX >= _rulingBody.DecisionArea.SizeX - 1) throw new Exception("Out of map, moron (right)");
            _rulingBody.PositionHandler.ActualPositionX++;
            _rulingBody.ArraysHandler.UpdateValue(ArrayType.Retreating);
            _rulingBody.PositionHandler.CheckIfIsHome();
        }

        public void ShowRetreatingArea()
        {
            _rulingBody.DecisionArea.ShowRetreatingArea();
        }
    }
}