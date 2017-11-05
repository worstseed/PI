﻿using System;
using System.Collections.Generic;
using NeuralNetwork.MovementAlgorythims;
using NeuralNetwork.NeuralNetworkModel;

namespace NeuralNetwork.RobotModel
{
    public class Robot
    {
        private Battery Battery;
        private Network Network;
        private RulingBody RulingBody;

        private static readonly double[] RightDirectionTranslate = {1, 0, 0, 0};
        private static readonly double[] LeftDirectionTranslate = { 0, 1, 0, 0 };
        private static readonly double[] AboveDirectionTranslate = { 0, 0, 1, 0 };
        private static readonly double[] BelowDirectionTranslate = { 0, 0, 0, 1 };


        public Robot(int maxCapacity, int inputCount, int[] hiddenCounts, int outputCount, 
            int? aReaSizeX = null, int? areaSizeY = null, int? startPositionX = null, int? startPositionY = null)
        {
            Battery = new Battery(maxCapacity);
            Network = new Network(inputCount, hiddenCounts, outputCount);
            RulingBody = new RulingBody(aReaSizeX, areaSizeY, startPositionX, startPositionY);
        }

        public void TestRun()
        {
            for(var i = 0; i < 40; i++)
            {
                for (var j = 0; j < 20; j++)
                {
                    RulingBody.Explore();
                    RulingBody.ShowExploringArea();
                    Console.WriteLine();
                }
                
                RulingBody.Retreat();
                RulingBody.ChangePositionToStart();
            }
            RulingBody.ShowRetreatingArea();
        }

        public void TeachRobot()
        {
            var dataList = new List<Data>();
            for (var i = 0; i < 5; i++)
            {

                for (var j = 0; j < 5; j++)
                {
                    RulingBody.Explore();
                    RulingBody.ShowExploringArea();
                    Console.WriteLine();
                }


                while (!RulingBody.IsHome)
                {
                    var values = new[] { (double)RulingBody.ActualPositionX, (double)RulingBody.ActualPositionY };
                    RulingBody.StepBack();
                    double[] targets = null;
                    switch (RulingBody.RetreatDirection)
                    {
                        case Direction.Right:
                            targets = RightDirectionTranslate;
                            break;
                        case Direction.Left:
                            targets = LeftDirectionTranslate;
                            break;
                        case Direction.Above:
                            targets = AboveDirectionTranslate;
                            break;
                        case Direction.Below:
                            targets = BelowDirectionTranslate;
                            break;
                    }
                    if (targets == null) throw new Exception("Target is null, lol");
                    var tmp = new Data(values, targets);
                    dataList.Add(tmp);
                }

                Network.Train(dataList, 50);
                dataList.Clear();
                RulingBody.ChangePositionToStart();
            }
            RulingBody.ShowRetreatingArea();
            Console.WriteLine("Now I now everything");
        }
    }
}