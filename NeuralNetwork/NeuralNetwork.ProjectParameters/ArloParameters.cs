using System.IO.Ports;

namespace NeuralNetwork.ProjectParameters
{
    public static class ArloParameters
    {
        public static bool UseArlo = false;
        public static bool Continue;
        public static SerialPort SerialPort;

        public const int WriteSpeed = 40;
        public const int WriteDistance = 80;
        public const int WriteAngle = 93;

        public const int WaitTime = 3000;
        public const int ShortWaitTime = 3000;

         
    }
}
