using System;
using NeuralNetwork.MovementAlgorythims;
using static System.Int32;

namespace NeuralNetwork.AreaModel
{
    public class Area
    {
        public Field[,] DecisionValuesArea;
        public int SizeX { get; set; }
        public int SizeY { get; set; }
        public int StartPositionX { get; set; }
        public int StartPositionY { get; set; }

        public Area(int? sizeX = null, int? sizeY = null, int? startPositionX = null, int? startPositionY = null)
        {
            SizeX = sizeX ?? 10;
            SizeY = sizeY ?? 10;
            DecisionValuesArea = new Field[SizeY, SizeX];
            
            for (var i = 0; i < SizeY; i++)
            {
                for (var j = 0; j < SizeX; j++)
                {
                    DecisionValuesArea[i, j] = new Field();
                }
            }

            StartPositionX = startPositionX ?? 0;
            StartPositionY = startPositionY ?? 0;
        }

        public void ShowExploringArea()
        {
            for (var i = 0; i < SizeY; i++)
            {
                for (var j = 0; j < SizeX; j++)
                {
                    DecisionValuesArea[i, j].ShowExploringFieldValue();
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void ShowRetreatingArea()
        {
            for (var i = 0; i < SizeY; i++)
            {
                for (var j = 0; j < SizeX; j++)
                {
                    DecisionValuesArea[i, j].ShowRetreatingFieldValue();
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void UpdateValue(ArrayType arrayType, int actualPositionX, int actualPositionY)
        {
            if (arrayType == ArrayType.Exploring)
                DecisionValuesArea[actualPositionX, actualPositionY].ExploringValue++;
            //else throw new Exception("Not yet implemented!");
        }

        public void SetObstacles(bool horizontal, bool vertical, bool random)
        {
            if (horizontal)
            {
                DecisionValuesArea[2, 2].ExploringValue = MaxValue;
                DecisionValuesArea[2, 3].ExploringValue = MaxValue;
                DecisionValuesArea[2, 4].ExploringValue = MaxValue;
                DecisionValuesArea[2, 5].ExploringValue = MaxValue;
            }

            if (vertical)
            {
                DecisionValuesArea[8, 8].ExploringValue = MaxValue;
                DecisionValuesArea[7, 8].ExploringValue = MaxValue;
                DecisionValuesArea[6, 8].ExploringValue = MaxValue;
                DecisionValuesArea[5, 8].ExploringValue = MaxValue;
            }
            
        }
    }
}