using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using NeuralNetwork.Arlo;
using NeuralNetwork.Cherry;
using NeuralNetwork.Helpers;
using NeuralNetwork.MovementAlgorythims.Enums;
using NeuralNetwork.ProjectParameters;
using NeuralNetwork.RobotModel.Enums;

namespace NeuralNetwork.MovementAlgorythims
{
    public class Explorer
    {
        private readonly RulingBody _rulingBody;

        public Explorer(RulingBody rulingBody)
        {
            _rulingBody = rulingBody;
        }

        public void Explore()
        {
            var directionToExplore = ChooseDirectionToExplore();
            SendDirectionToCherry(directionToExplore);
            //Console.WriteLine("Exploring direction: {0}", directionToExplore);
            _rulingBody.PositionHandler.IsHome = false;
            switch (directionToExplore)
            {
                case Direction.Right:
                    ExploreRight(RobotMode.Learning);
                    break;
                case Direction.Left:
                    ExploreLeft(RobotMode.Learning);
                    break;
                case Direction.Above:
                    ExploreAbove(RobotMode.Learning);
                    break;
                case Direction.Below:
                    ExploreBelow(RobotMode.Learning);
                    break;
                case Direction.None:
                    throw new Exception("Why am I not moving?");
            }
            _rulingBody.ArraysHandler.UpdateRetreatingAreaValue();
        }

        private Direction ChooseDirectionToExplore()
        {
            _rulingBody.PositionHandler.GetSurroundingValues(out int left, out int right, out int above, out int below, ArrayType.Exploring);
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

        public void ExploreBelow(RobotMode robotMode)
        {
            _rulingBody.PositionHandler.ActualPositionY++;
            if (robotMode == RobotMode.Learning)
                _rulingBody.ArraysHandler.UpdateValue(ArrayType.Exploring);

            if (CherryParameters.UseCherry)
            {
                CherryController.Turn(Direction.Below);
                CherryController.MoveCherry();
            }

            if (!ArloParameters.UseArlo) return;
            ArloController.Turn(Direction.Below);
            ArloController.MoveArlo();
        }

        public void ExploreAbove(RobotMode robotMode)
        {
            _rulingBody.PositionHandler.ActualPositionY--;
            if (robotMode == RobotMode.Learning)
                _rulingBody.ArraysHandler.UpdateValue(ArrayType.Exploring);

            if (CherryParameters.UseCherry)
            {
                CherryController.Turn(Direction.Above);
                CherryController.MoveCherry();
            }

            if (!ArloParameters.UseArlo) return;
            ArloController.Turn(Direction.Above);
            ArloController.MoveArlo();
        }

        public void ExploreLeft(RobotMode robotMode)
        {
            _rulingBody.PositionHandler.ActualPositionX--;
            if (robotMode == RobotMode.Learning)
                _rulingBody.ArraysHandler.UpdateValue(ArrayType.Exploring);

            if (CherryParameters.UseCherry)
            {
                CherryController.Turn(Direction.Left);
                CherryController.MoveCherry();
            }

            if (!ArloParameters.UseArlo) return;
            ArloController.Turn(Direction.Left);
            ArloController.MoveArlo();
        }

        public void ExploreRight(RobotMode robotMode)
        {
            _rulingBody.PositionHandler.ActualPositionX++;
            if (robotMode == RobotMode.Learning)
                _rulingBody.ArraysHandler.UpdateValue(ArrayType.Exploring);

            if (CherryParameters.UseCherry)
            {
                CherryController.Turn(Direction.Right);
                CherryController.MoveCherry();
            }

            if (!ArloParameters.UseArlo) return;
            ArloController.Turn(Direction.Right);
            ArloController.MoveArlo();
        }

        public void ShowExploringArea()
        {
            _rulingBody.DecisionArea.ShowExploringArea();
        }

        private static void SendDirectionToCherry(Direction directionToExplore)
        {
            if (!CherryParameters.UseCherry) return;
            using (var client = new WebClient())
            {
                var values = new NameValueCollection {["port"] = "Exploring direction: " + directionToExplore};

                var ret = client.UploadValues("http://localhost:8181/", "POST", values);
                Console.WriteLine(Encoding.ASCII.GetString(ret));
            }
        }
    }
}