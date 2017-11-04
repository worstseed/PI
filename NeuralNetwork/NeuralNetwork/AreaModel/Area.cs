using System;
using NeuralNetwork.MovementAlgorythims;

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
            SizeY = sizeY ?? 20;
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
    }
}