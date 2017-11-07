using System;
using NeuralNetwork.RobotModel;

namespace NeuralNetwork
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            //var xorTest = new XOR_Test();
            //xorTest.RunXORTest();


            var robot = new Robot(100, 2, new[] { 2, 2 }, 4);
            //robot.TestRun();
            robot.TeachRobot();

            Console.ReadKey();
        }
    }
}