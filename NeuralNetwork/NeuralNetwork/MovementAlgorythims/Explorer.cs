using System;
using System.Collections.Generic;
using NeuralNetwork.GeneralHelpers;
using NeuralNetwork.MovementAlgorythims.Enums;

namespace NeuralNetwork.MovementAlgorythims
{
    public class Explorer
    {
        private readonly RulingBody _rulingBody;

        public Explorer(RulingBody rulingBody)
        {
            _rulingBody = rulingBody;
        }

        public void Explore()
        {
            var directionToExplore = ChooseDirectionToExplore();
            //Console.WriteLine("Exploring direction: {0}", directionToExplore);
            _rulingBody.PositionHandler.IsHome = false;
            switch (directionToExplore)
            {
                case Direction.Right:
                    ExploreRight();
                    break;
                case Direction.Left:
                    ExploreLeft();
                    break;
                case Direction.Above:
                    ExploreAbove();
                    break;
                case Direction.Below:
                    ExploreBelow();
                    break;
                case Direction.None:
                    throw new Exception("Why am I not moving?");
            }
            _rulingBody.ArraysHandler.UpdateRetreatingAreaValue();
        }

        private Direction ChooseDirectionToExplore()
        {
            _rulingBody.PositionHandler.GetSurroundingValues(out int left, out int right, out int above, out int below, ArrayType.Exploring);
            //Console.WriteLine("left: {0}, right: {1}, above: {2}, below: {3}", left, right, above, below); //
            var min = Minimizer.FindMinimum(right, left, below, above);

            return RandomizeDirection(left, min, right, above, below);
            
        }

        private static Direction RandomizeDirection(int left, int min, int right, int above, int below)
        {
            var possibleDirections = new List<Direction>();
            if (left == min) possibleDirections.Add(Direction.Left);
            if (right == min) possibleDirections.Add(Direction.Right);
            if (above == min) possibleDirections.Add(Direction.Above);
            if (below == min) possibleDirections.Add(Direction.Below);
            var randomIndex = Randomizer.GetRandomIndex(possibleDirections.Count);
            return possibleDirections.Count != 0 ? possibleDirections[randomIndex] : Direction.None;
        }

        public void ExploreBelow()
        {
            _rulingBody.PositionHandler.ActualPositionY++;
            _rulingBody.ArraysHandler.UpdateValue(ArrayType.Exploring);
        }

        public void ExploreAbove()
        {
            _rulingBody.PositionHandler.ActualPositionY--;
            _rulingBody.ArraysHandler.UpdateValue(ArrayType.Exploring);
        }

        public void ExploreLeft()
        {
            _rulingBody.PositionHandler.ActualPositionX--;
            _rulingBody.ArraysHandler.UpdateValue(ArrayType.Exploring);
        }

        public void ExploreRight()
        {
            _rulingBody.PositionHandler.ActualPositionX++;
            _rulingBody.ArraysHandler.UpdateValue(ArrayType.Exploring);
        }

        public void ShowExploringArea()
        {
            _rulingBody.DecisionArea.ShowExploringArea();
        }
    }
}