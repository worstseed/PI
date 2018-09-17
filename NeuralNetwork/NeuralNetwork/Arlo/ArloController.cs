using System;
using System.Threading;
using NeuralNetwork.MovementAlgorythims.Enums;
using NeuralNetwork.ProjectParameters;

namespace NeuralNetwork.Arlo
{
    public class ArloController
    {
        public static Direction Rotated = Direction.Right;

        public static void Turn(Direction directionToMove)
        {
            var numberOfTurns = directionToMove - Rotated;
            ResetEncoders();
            if (numberOfTurns == 0) return;
            if (numberOfTurns == 3 || numberOfTurns == -1)
                TurnLeft();
            else
                TurnRight(numberOfTurns);
            Rotated = directionToMove;
            GetEncoderValues();
        }

        public static void TurnLeft()
        {
            ArloParameters.SerialPort.WriteLine(
                $"TURN {-ArloParameters.WriteAngle} {ArloParameters.WriteSpeed}");
            Thread.Sleep(ArloParameters.ShortWaitTime);
        }

        public static void TurnRight(int numberOfTurns)
        {
            ArloParameters.SerialPort.WriteLine(
                $"TURN {numberOfTurns * ArloParameters.WriteAngle} {ArloParameters.WriteSpeed}");
            Thread.Sleep(Math.Abs(numberOfTurns) * ArloParameters.ShortWaitTime);
        }

        public static void TurnToStartingPosition()
        {
            Turn(Direction.Right);
        }

        public static void MoveArlo()
        {
            ResetEncoders();
            ArloParameters.SerialPort.WriteLine(
                $"MOVE {ArloParameters.WriteDistance} {ArloParameters.WriteDistance} {ArloParameters.WriteSpeed}");
            Thread.Sleep(ArloParameters.WaitTime);
            GetEncoderValues();
        }

        public static void ResetEncoders()
        {
            ArloParameters.SerialPort.WriteLine(
                "rst");
            Thread.Sleep(ArloParameters.ShortWaitTime);
        }

        public static void GetEncoderValues()
        {
            ArloParameters.SerialPort.WriteLine(
                "dist");
            Thread.Sleep(ArloParameters.ShortWaitTime);
        }
    }
}
