using System;
using NeuralNetwork.AreaModel;
using NeuralNetwork.Helpers;
using static System.Int32;

namespace NeuralNetwork.MovementAlgorythims
{
    public class RulingBody
    {
        public Area DecisionArea;
        private int ActualPositionX { get; set; }
        private int ActualPositionY { get; set; }
        private int Counter { get; set; }
        private bool IsHome { get; set; }

        public RulingBody(int? areaSizeX = null, int? areaSizeY = null, int? startPositionX = null, int? startPositionY = null)
        {
            DecisionArea = new Area(areaSizeX, areaSizeY, startPositionX, startPositionY);
            ActualPositionX = DecisionArea.StartPositionX;
            ActualPositionY = DecisionArea.StartPositionY;
            Counter = 1;
            DecisionArea.DecisionValuesArea[ActualPositionX, ActualPositionY].RetreatingValue = 0;
            UpdateValue(ArrayType.Exploring);
        }

        public void Explore()
        {
            var directionToExplore = ChooseDirectionToExplore();

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
            Console.WriteLine(directionToExplore);
            UpdateRetreatingAreaValue();
        }

        private Direction ChooseDirectionToExplore()
        {
            GetSurroundingValues(out int left, out int right, out int above, out int below, ArrayType.Exploring);
            Console.WriteLine("left: {0}, right: {1}, above: {2}, below: {3}", left, right, above, below); //
            var min = Minimizer.FindMinimum(right, left, below, above);

            if (left == min) return Direction.Left;
            if (right == min) return Direction.Right;
            if (above == min) return Direction.Above;
            if (below == min) return Direction.Below;

            return Direction.None;
        }

        private void ExploreRight()
        {
            ActualPositionX++;
            UpdateValue(ArrayType.Exploring);
        }
        private void ExploreLeft()
        {
            ActualPositionX--;
            UpdateValue(ArrayType.Exploring);
        }
        private void ExploreAbove()
        {
            ActualPositionY--;
            UpdateValue(ArrayType.Exploring);
        }
        private void ExploreBelow()
        {
            ActualPositionY++;
            UpdateValue(ArrayType.Exploring);
        }


        private void UpdateRetreatingAreaValue()
        {
            if (DecisionArea.DecisionValuesArea[ActualPositionX, ActualPositionY].RetreatingValue >= Counter
                || DecisionArea.DecisionValuesArea[ActualPositionX, ActualPositionY].RetreatingValue == -1)
                DecisionArea.DecisionValuesArea[ActualPositionX, ActualPositionY].RetreatingValue = Counter;
            Counter++;
        }


        public void Retreat()
        {
            while(!IsHome)
                StepBack();
        }

        private void StepBack()
        {
            var directionToRetreat = ChooseDirectionToRetreat();

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

        private Direction ChooseDirectionToRetreat()
        {
            GetSurroundingValues(out int left, out int right, out int above, out int below, ArrayType.Retreating);

            if (right == -1) right = MaxValue;
            if (left == -1) left = MaxValue;
            if (above == -1) above = MaxValue;
            if (below == -1) below = MaxValue;

            var min = Minimizer.FindMinimum(right, left, below, above);

            if (min == 0) IsHome = true;

            if (left == min) return Direction.Left;
            if (right == min) return Direction.Right;
            if (above == min) return Direction.Above;
            if (below == min) return Direction.Below;

            return Direction.None;
        }


        private void RetreatRight()
        {
            ActualPositionX++;
            UpdateValue(ArrayType.Retreating);
        }
        private void RetreatLeft()
        {
            ActualPositionX--;
            UpdateValue(ArrayType.Retreating);
        }
        private void RetreatAbove()
        {
            ActualPositionY--;
            UpdateValue(ArrayType.Retreating);
        }
        private void RetreatBelow()
        {
            ActualPositionY++;
            UpdateValue(ArrayType.Retreating);
        }



        public void ChangePositionToStart()
        {
            Counter = 1;
            ActualPositionX = DecisionArea.StartPositionX;
            ActualPositionY = DecisionArea.StartPositionY;
            UpdateValue(ArrayType.Exploring);
        }

        private void UpdateValue(ArrayType arrayType)
        {
            DecisionArea.UpdateValue(arrayType, ActualPositionX, ActualPositionY);
        }

        private void GetSurroundingValues(out int left, out int right, out int above, out int below, ArrayType arrayType)
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


        private int GetValueBelow(ArrayType arrayType)
        {
            return arrayType == ArrayType.Exploring ? DecisionArea.DecisionValuesArea[ActualPositionX, ActualPositionY + 1].ExploringValue
                : DecisionArea.DecisionValuesArea[ActualPositionX, ActualPositionY + 1].RetreatingValue;
        }
        private int GetValueAbove(ArrayType arrayType)
        {
            return arrayType == ArrayType.Exploring ? DecisionArea.DecisionValuesArea[ActualPositionX, ActualPositionY - 1].ExploringValue
                : DecisionArea.DecisionValuesArea[ActualPositionX, ActualPositionY - 1].RetreatingValue;
        }
        private int GetValueRight(ArrayType arrayType)
        {
            return arrayType == ArrayType.Exploring ? DecisionArea.DecisionValuesArea[ActualPositionX + 1, ActualPositionY].ExploringValue
                : DecisionArea.DecisionValuesArea[ActualPositionX + 1, ActualPositionY].RetreatingValue;
        }
        private int GetValueLeft(ArrayType arrayType)
        {
            return arrayType == ArrayType.Exploring ? DecisionArea.DecisionValuesArea[ActualPositionX - 1, ActualPositionY].ExploringValue
                : DecisionArea.DecisionValuesArea[ActualPositionX - 1, ActualPositionY].RetreatingValue;
        }



        private bool ThereIsFieldBelow()
        {
            return ActualPositionY + 1 <= DecisionArea.SizeY;
        }
        private bool ThereIsFieldAbove()
        {
            return ActualPositionY - 1 >= 0;
        }
        private bool ThereIsFieldOnTheRight()
        {
            return ActualPositionX + 1 <= DecisionArea.SizeX;
        }
        private bool ThereIsFieldOnTheLeft()
        {
            return ActualPositionX - 1 >= 0;
        }
    }
}