using System;
using System.Collections.Generic;
using NeuralNetwork.AreaModel;
using NeuralNetwork.GeneralHelpers;
using static System.Int32;

namespace NeuralNetwork.MovementAlgorythims
{
    public class RulingBody
    {
        public Area DecisionArea;
        public int ActualPositionX { get; set; }
        public int ActualPositionY { get; set; }
        private int Counter { get; set; }
        public bool IsHome { get; set; }
        public Direction RetreatDirection { get; set; }

        public RulingBody(int? areaSizeX = null, int? areaSizeY = null, int? startPositionX = null, int? startPositionY = null)
        {
            DecisionArea = new Area(areaSizeX, areaSizeY, startPositionX, startPositionY);
            ActualPositionY = DecisionArea.StartPositionX;
            ActualPositionX = DecisionArea.StartPositionY;
            Counter = 1;
            DecisionArea.DecisionValuesArea[ActualPositionY, ActualPositionX].RetreatingValue = 0;
            UpdateValue(ArrayType.Exploring);
        }

        public void Explore()
        {
            var directionToExplore = ChooseDirectionToExplore();
            //Console.WriteLine("Exploring direction: {0}", directionToExplore);
            IsHome = false;
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
            UpdateRetreatingAreaValue();
        }

        private Direction ChooseDirectionToExplore()
        {
            GetSurroundingValues(out int left, out int right, out int above, out int below, ArrayType.Exploring);
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

        private void ExploreBelow()
        {
            ActualPositionY++;
            UpdateValue(ArrayType.Exploring);
        }
        private void ExploreAbove()
        {
            ActualPositionY--;
            UpdateValue(ArrayType.Exploring);
        }
        private void ExploreLeft()
        {
            ActualPositionX--;
            UpdateValue(ArrayType.Exploring);
        }
        private void ExploreRight()
        {
            ActualPositionX++;
            UpdateValue(ArrayType.Exploring);
        }


        private void UpdateRetreatingAreaValue()
        {
            if (DecisionArea.DecisionValuesArea[ActualPositionY, ActualPositionX].RetreatingValue >= Counter
                || DecisionArea.DecisionValuesArea[ActualPositionY, ActualPositionX].RetreatingValue == -1)
                DecisionArea.DecisionValuesArea[ActualPositionY, ActualPositionX].RetreatingValue = Counter;
            Counter++;
        }


        public void Retreat()
        {
            while(!IsHome)
                StepBack();
        }

        public void StepBack()
        {
            var directionToRetreat = ChooseDirectionToRetreat();
            Console.WriteLine(directionToRetreat);//
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

        private Direction ChooseDirectionToRetreat()
        {
            GetSurroundingValues(out int left, out int right, out int above, out int below, ArrayType.Retreating);

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
            if (ActualPositionY >= DecisionArea.SizeY - 1) throw new Exception("Out of map, moron (below)");
            ActualPositionY++;
            UpdateValue(ArrayType.Retreating);
            CheckIfIsHome();
        }
        public void RetreatAbove()
        {
            if (ActualPositionY <= 0) throw new Exception("Out of map, moron (above)");
            ActualPositionY--;
            UpdateValue(ArrayType.Retreating);
            CheckIfIsHome();
        }
        public void RetreatLeft()
        {
            if (ActualPositionX <= 0) throw new Exception("Out of map, moron (left)");
            ActualPositionX--;
            UpdateValue(ArrayType.Retreating);
            CheckIfIsHome();
        }
        public void RetreatRight()
        {
            if (ActualPositionX >= DecisionArea.SizeX - 1) throw new Exception("Out of map, moron (right)");
            ActualPositionX++;
            UpdateValue(ArrayType.Retreating);
            CheckIfIsHome();
        }

        private void CheckIfIsHome()
        {
            if (DecisionArea.DecisionValuesArea[ActualPositionY, ActualPositionX].RetreatingValue == 0)
                IsHome = true;
        }

        public void ChangePositionToStart()
        {
            Counter = 1;
            ActualPositionY = DecisionArea.StartPositionX;
            ActualPositionX = DecisionArea.StartPositionY;
            UpdateValue(ArrayType.Exploring);
        }

        private void UpdateValue(ArrayType arrayType)
        {
            DecisionArea.UpdateValue(arrayType, ActualPositionY, ActualPositionX);
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


        private int GetValueRight(ArrayType arrayType)
        {
            return arrayType == ArrayType.Exploring ? DecisionArea.DecisionValuesArea[ActualPositionY, ActualPositionX + 1].ExploringValue
                : DecisionArea.DecisionValuesArea[ActualPositionY, ActualPositionX + 1].RetreatingValue;
        }
        private int GetValueLeft(ArrayType arrayType)
        {
            return arrayType == ArrayType.Exploring ? DecisionArea.DecisionValuesArea[ActualPositionY, ActualPositionX - 1].ExploringValue
                : DecisionArea.DecisionValuesArea[ActualPositionY, ActualPositionX - 1].RetreatingValue;
        }
        private int GetValueBelow(ArrayType arrayType)
        {
            return arrayType == ArrayType.Exploring ? DecisionArea.DecisionValuesArea[ActualPositionY + 1, ActualPositionX].ExploringValue
                : DecisionArea.DecisionValuesArea[ActualPositionY + 1, ActualPositionX].RetreatingValue;
        }
        private int GetValueAbove(ArrayType arrayType)
        {
            return arrayType == ArrayType.Exploring ? DecisionArea.DecisionValuesArea[ActualPositionY - 1, ActualPositionX].ExploringValue
                : DecisionArea.DecisionValuesArea[ActualPositionY - 1, ActualPositionX].RetreatingValue;
        }



        private bool ThereIsFieldOnTheRight()
        {
            return ActualPositionX + 1 < DecisionArea.SizeY;
        }
        private bool ThereIsFieldOnTheLeft()
        {
            return ActualPositionX - 1 >= 0;
        }
        private bool ThereIsFieldBelow()
        {
            return ActualPositionY + 1 < DecisionArea.SizeX;
        }
        private bool ThereIsFieldAbove()
        {
            return ActualPositionY - 1 >= 0;
        }
        public void ShowExploringArea()
        {
            DecisionArea.ShowExploringArea();
        }
        public void ShowRetreatingArea()
        {
            DecisionArea.ShowRetreatingArea();
        }
        
    }
}