using System;
using NeuralNetwork.RobotModel;
using NeuralNetwork.Tests;

namespace NeuralNetwork
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            var xorTest = new XOR_Test();
            xorTest.RunXORTest();

            //var listRemovalTest = new ListRemoval_Test();
            //listRemovalTest.RemoveElements();

            //var robot = new Robot(100, 2, new[] { 2, 2 }, 4);
            //robot.TestRun();
            //robot.TeachRobot();

            Console.ReadKey();
        }
    }
}