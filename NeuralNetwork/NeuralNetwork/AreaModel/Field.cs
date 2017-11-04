using System;

namespace NeuralNetwork.AreaModel
{
    public class Field
    {
        public int ExploringValue { get; set; }
        public int RetreatingValue { get; set; }

        public Field()
        {
            ExploringValue = 0;
            RetreatingValue = -1;
        }

        public void ShowExploringFieldValue()
        {
            Console.Write("{0:D} ", ExploringValue);
        }

        public void ShowRetreatingFieldValue()
        {
            Console.Write("{0:D} ", RetreatingValue);
        }
    }
}