using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using NeuralNetwork.MovementAlgorythims.Enums;
using NeuralNetwork.ProjectParameters;

namespace NeuralNetwork.Cherry
{
    class CherryController
    {
        public static Direction Rotated = Direction.Right;

        public static void Turn(Direction directionToMove)
        {
            var numberOfTurns = directionToMove - Rotated;
            if (numberOfTurns == 0) return;
            if (numberOfTurns == 3 || numberOfTurns == -1)
                TurnLeft();
            else
                TurnRight(numberOfTurns);
            Rotated = directionToMove;
        }

        public static void TurnLeft()
        {
            using (var client = new WebClient())
            {
                var values = new NameValueCollection {["port"] = "(0, 1, 0)"};

                var ret = client.UploadValues("http://localhost:8181/", "POST", values);
                Console.WriteLine(Encoding.ASCII.GetString(ret));
            }
        }

        public static void TurnRight(int numberOfTurns)
        {
            if (numberOfTurns == -2) numberOfTurns = 2;
            if (numberOfTurns == -3) numberOfTurns = 2;

            var values = new NameValueCollection();
            using (var client = new WebClient())
            {
                values["port"] = $"({numberOfTurns}, 0, 0)";

                var ret = client.UploadValues("http://localhost:8181/", "POST", values);
                Console.WriteLine(Encoding.ASCII.GetString(ret));
            }
        }

        public static void MoveCherry()
        {
            if (!CherryParameters.UseCherry) return;
            using (var client = new WebClient())
            {
                var values = new NameValueCollection {["port"] = "(0, 0, 1)"};

                var ret = client.UploadValues("http://localhost:8181/", "POST", values);
                Console.WriteLine(Encoding.ASCII.GetString(ret));
            }
        }

        public static void CherryIsHome()
        {
            if (!CherryParameters.UseCherry) return;
            using (var client = new WebClient())
            {
                var values = new NameValueCollection { ["port"] = "Home" };

                var ret = client.UploadValues("http://localhost:8181/", "POST", values);
                Console.WriteLine(Encoding.ASCII.GetString(ret));
            }
        }
    }
}
