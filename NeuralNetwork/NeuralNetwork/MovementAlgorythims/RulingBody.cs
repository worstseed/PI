using NeuralNetwork.AreaModel;
using NeuralNetwork.MovementAlgorythims.Enums;

namespace NeuralNetwork.MovementAlgorythims
{
    public class RulingBody
    {
        public Area DecisionArea;

        public Explorer Explorer { get; }
        public Retreater Retreater { get; }
        public Mover Mover { get; }

        public PositionHandler PositionHandler { get; }
        public ArraysHandler ArraysHandler { get; }

        public RulingBody(int? areaSizeX = null, int? areaSizeY = null, int? startPositionX = null, int? startPositionY = null)
        {
            DecisionArea = new Area(areaSizeX, areaSizeY, startPositionX, startPositionY);
            var actualPositionY = DecisionArea.StartPositionX;
            var actualPositionX = DecisionArea.StartPositionY;

            PositionHandler = new PositionHandler(actualPositionY, actualPositionX, this);
            ArraysHandler = new ArraysHandler(1, this);

            DecisionArea.DecisionValuesArea[PositionHandler.ActualPositionY, PositionHandler.ActualPositionX].RetreatingValue = 0;
            ArraysHandler.UpdateValue(ArrayType.Exploring);

            Explorer = new Explorer(this);
            Retreater = new Retreater(this);
            Mover = new Mover(this);
        }
    }
}