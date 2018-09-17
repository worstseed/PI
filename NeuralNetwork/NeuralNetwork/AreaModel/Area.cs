using System;
using NeuralNetwork.MovementAlgorythims.Enums;
using NeuralNetwork.ProjectParameters;
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
        }

        public void SetObstacles(bool horizontal, bool vertical, bool random)
        {
            if (horizontal)
            {
                //DecisionValuesArea[2, 2].ExploringValue = MaxValue;
                //DecisionValuesArea[2, 3].ExploringValue = MaxValue;
                //DecisionValuesArea[2, 4].ExploringValue = MaxValue;
                //DecisionValuesArea[2, 5].ExploringValue = MaxValue;

                //DecisionValuesArea[3, 2].ExploringValue = MaxValue;
                //DecisionValuesArea[3, 3].ExploringValue = MaxValue;
                //DecisionValuesArea[2, 3].ExploringValue = MaxValue;
                //DecisionValuesArea[4, 3].ExploringValue = MaxValue;
                //DecisionValuesArea[5, 3].ExploringValue = MaxValue;
                //DecisionValuesArea[6, 3].ExploringValue = MaxValue;
                //DecisionValuesArea[7, 3].ExploringValue = MaxValue;
                //DecisionValuesArea[5, 4].ExploringValue = MaxValue;
                //DecisionValuesArea[5, 5].ExploringValue = MaxValue;
                //DecisionValuesArea[6, 5].ExploringValue = MaxValue;
                //DecisionValuesArea[7, 5].ExploringValue = MaxValue;
                //DecisionValuesArea[7, 8].ExploringValue = MaxValue;
                //DecisionValuesArea[6, 8].ExploringValue = MaxValue;
                //DecisionValuesArea[5, 8].ExploringValue = MaxValue;
                //DecisionValuesArea[4, 8].ExploringValue = MaxValue;
                //DecisionValuesArea[3, 8].ExploringValue = MaxValue;
                //DecisionValuesArea[2, 8].ExploringValue = MaxValue;
                //DecisionValuesArea[1, 8].ExploringValue = MaxValue;
                //DecisionValuesArea[2, 9].ExploringValue = MaxValue;
                //DecisionValuesArea[2, 7].ExploringValue = MaxValue;
                //DecisionValuesArea[3, 6].ExploringValue = MaxValue;
                //DecisionValuesArea[3, 7].ExploringValue = MaxValue;
                //DecisionValuesArea[3, 9].ExploringValue = MaxValue;
                //DecisionValuesArea[2, 9].ExploringValue = MaxValue;

                DecisionValuesArea[0, 5].ExploringValue = MaxValue;
                DecisionValuesArea[1, 2].ExploringValue = MaxValue;
                DecisionValuesArea[3, 6].ExploringValue = MaxValue;
                DecisionValuesArea[8, 8].ExploringValue = MaxValue;
                DecisionValuesArea[4, 5].ExploringValue = MaxValue;
                DecisionValuesArea[7, 2].ExploringValue = MaxValue;
                DecisionValuesArea[3, 8].ExploringValue = MaxValue;
                DecisionValuesArea[2, 4].ExploringValue = MaxValue;
                DecisionValuesArea[3, 0].ExploringValue = MaxValue;
                DecisionValuesArea[5, 0].ExploringValue = MaxValue;
                DecisionValuesArea[7, 2].ExploringValue = MaxValue;
                DecisionValuesArea[5, 6].ExploringValue = MaxValue;
                DecisionValuesArea[8, 5].ExploringValue = MaxValue;
                DecisionValuesArea[9, 8].ExploringValue = MaxValue;
                DecisionValuesArea[6, 4].ExploringValue = MaxValue;

            }

            if (vertical)
            {
                DecisionValuesArea[8, 5].ExploringValue = MaxValue;
                DecisionValuesArea[7, 5].ExploringValue = MaxValue;
                DecisionValuesArea[6, 5].ExploringValue = MaxValue;
                DecisionValuesArea[5, 5].ExploringValue = MaxValue;
            }
            if (random)
            {
                DecisionValuesArea[0, 0].ExploringValue = MaxValue;
                DecisionValuesArea[1, 0].ExploringValue = MaxValue;

                DecisionValuesArea[0, 9].ExploringValue = MaxValue;

                DecisionValuesArea[2, 3].ExploringValue = MaxValue;
                DecisionValuesArea[2, 4].ExploringValue = MaxValue;
                DecisionValuesArea[3, 3].ExploringValue = MaxValue;
                DecisionValuesArea[3, 4].ExploringValue = MaxValue;

                DecisionValuesArea[7, 2].ExploringValue = MaxValue;

                DecisionValuesArea[4, 6].ExploringValue = MaxValue;
                DecisionValuesArea[5, 6].ExploringValue = MaxValue;
                DecisionValuesArea[6, 6].ExploringValue = MaxValue;
                DecisionValuesArea[7, 6].ExploringValue = MaxValue;

                for (var i = 0; i < GeneralParameters.ArrayDefaultSize; i++)
                    for (var j = GeneralParameters.ArrayDefaultSize - 2; j < GeneralParameters.ArrayDefaultSize; j++)
                        DecisionValuesArea[j, i].ExploringValue = MaxValue;
            }
        }
    }
}