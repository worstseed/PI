using System;
using System.Diagnostics;
using System.Threading;
using NeuralNetwork.GeneralHelpers;
using NeuralNetwork.MovementAlgorythims;
using NeuralNetwork.MovementAlgorythims.Enums;
using NeuralNetworkPresentation.Parameters;

namespace NeuralNetworkPresentation.Algorythims
{
    public class Checker
    {
        private readonly PresentationWindow _presentationWindow;

        public Checker(PresentationWindow presentationWindow)
        {
            _presentationWindow = presentationWindow;
        }

        public void Check(object sender, EventArgs e)
        {
            //MarkActualPosition(MovementType.Explore);

            ////ExploreNumberOfSteps(NumberOfTestingSteps, RobotMode.UsingKnowledge);

            //Robot.SetTestPosition();
            //MarkActualPosition(MovementType.Explore);
            _presentationWindow.PresentationArrays.PaintExploringArray();
            _presentationWindow.PresentationArrays.PaintRetreatingArray();

            //Console.WriteLine(@"Time to go home!");
            _presentationWindow.Refresh();
            Thread.Sleep(FormParameters.LongSleepTime);

            if (!TryToRetreatWithNeuralNetwork()) return;

            _presentationWindow.Robot.PositionHandler.ChangePositionToStart();

            //Console.WriteLine(@"Time to rest, I know everything now.");

            _presentationWindow.Refresh();
            Thread.Sleep(FormParameters.LongSleepTime);
        }

        private bool TryToRetreatWithNeuralNetwork()
        {
            var elapsed = 0d;

            try
            {
                while (!_presentationWindow.Robot.PositionHandler.IsRobotHome() && elapsed < 2000)
                {
                    var timer = new Stopwatch();
                    timer.Start();

                    RetreatUsingNeuralNetwork();

                    timer.Stop();
                    elapsed += timer.Elapsed.TotalMilliseconds;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Console.WriteLine(@"Something went terribly wrong, my friend :(");
                _presentationWindow.Robot.PositionHandler.ChangePositionToStart();
                _presentationWindow.Refresh();
                Thread.Sleep(FormParameters.LongSleepTime);
                return false;
            }
            return true;
        }

        private void RetreatUsingNeuralNetwork()
        {
            _presentationWindow.Refresh();
            var tempDirection = _presentationWindow.Robot.GetOutputDirection(new double[]
                {_presentationWindow.Robot.PositionHandler.GetActualPositionX(), _presentationWindow.Robot.PositionHandler.GetActualPositionY()});
            Console.WriteLine(@"x: {0}, y: {1}, direction: {2}", _presentationWindow.Robot.PositionHandler.GetActualPositionX(), _presentationWindow.Robot.PositionHandler.GetActualPositionY(),
                tempDirection); //
            _presentationWindow.Robot.NetworkHandler.ShowOutput(new double[] {_presentationWindow.Robot.PositionHandler.GetActualPositionX(), _presentationWindow.Robot.PositionHandler.GetActualPositionY() });
            switch (tempDirection)
            {
                case Direction.Right:
                    _presentationWindow.Robot.Retreater.RetreatRight();
                    break;
                case Direction.Left:
                    _presentationWindow.Robot.Retreater.RetreatLeft();
                    break;
                case Direction.Above:
                    _presentationWindow.Robot.Retreater.RetreatAbove();
                    break;
                case Direction.Below:
                    _presentationWindow.Robot.Retreater.RetreatBelow();
                    break;
                case Direction.None:
                    throw new Exception("Why am I not moving?");
            }
            _presentationWindow.PresentationArrays.MarkActualPosition(MovementType.Retreat);
            _presentationWindow.Controllers.UpdateBatteryLevelValue();
        }
    }
}