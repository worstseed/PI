using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NeuralNetwork.Helpers;
using NeuralNetwork.MovementAlgorythims.Enums;
using NeuralNetwork.ProjectParameters;
using NeuralNetwork.RobotModel;
using static System.Int32;

namespace NeuralNetwork.TopologyEvolution
{
    public class Genome
    {
        public Guid Id { get; set; }
        public List<int> Genes; // Layers
        public double FitnessValue { get; set; }
        public int Length { get; set; } // Number of layers

        public Genome()
        {
            Id = Guid.NewGuid();
            Length = Randomizer.GetRandomFromRange(
                NetworkParameters.MinimumNumberOfLayers,
                NetworkParameters.MaximumNumberOfLayers);
            Genes = new List<int>(Length);
            for (var i = 0; i < Length; i++)
                Genes.Add(Randomizer.GetRandomFromRange(
                    NetworkParameters.MinimumNumberOfNeurons, 
                    NetworkParameters.MaximumNumberOfNeurons));
            CalculateFitnessValue();
        }

        public Genome(Genome orginalGenome)
        {
            Id = orginalGenome.Id;
            Length = orginalGenome.Length;
            Genes = new List<int>(Length);
            for (var i = 0; i < Length; i++)
                Genes.Add(orginalGenome.Genes[i]);
            FitnessValue = orginalGenome.FitnessValue;
        }

        public void CalculateFitnessValue()
        {
            FitnessValue = 0;
            //Parallel.For(0, GeneralParameters.AverageOfNumber,
            //    i => CalculateFitnessValueForSpecificRobot(CreateAndTeachRobot()));
            for (var i = 0; i < GeneralParameters.AverageOfNumber; i++)
            {
                var robot = CreateAndTeachRobot();
                CalculateFitnessValueForSpecificRobot(robot);
            }
            FitnessValue /= GeneralParameters.AverageOfNumber;
        }

        private void CalculateFitnessValueForSpecificRobot(Robot robot)
        {
            for (var j = 0; j < GeneralParameters.ArrayDefaultSize; j++)
            {
                for (var i = 0; i < GeneralParameters.ArrayDefaultSize; i++)
                {
                    robot.RulingBody.PositionHandler.ActualPositionX = i;
                    robot.RulingBody.PositionHandler.ActualPositionY = j;

                    if (robot.RulingBody.DecisionArea.DecisionValuesArea[i, j].ExploringValue == MaxValue
                        || robot.RulingBody.DecisionArea.DecisionValuesArea[i, j].ExploringValue == 0) continue;

                    var retreatDirection = robot.RulingBody.Retreater.ChooseDirectionToRetreat();
                    var networkDirection = robot.GetOutputDirection(new double[] {i, j});
                    var networkOutput = robot.Network.GetOutput(new double[] {i, j});

                    if (retreatDirection == networkDirection) FitnessValue += 10;
                    else
                    {
                        var targetOutput = DirectionTranslator.None;
                        switch (retreatDirection)
                        {
                            case Direction.Right:
                                targetOutput = DirectionTranslator.Right;
                                break;
                            case Direction.Left:
                                targetOutput = DirectionTranslator.Left;
                                break;
                            case Direction.Above:
                                targetOutput = DirectionTranslator.Above;
                                break;
                            case Direction.Below:
                                targetOutput = DirectionTranslator.Below;
                                break;
                            case Direction.None:
                                targetOutput = DirectionTranslator.None;
                                break;
                        }
                        for (var k = 0; k < networkOutput.Length; k++)
                        {
                            FitnessValue -= Math.Abs(targetOutput[k] - networkOutput[k]) * 10;
                        }
                    }
                }
            }
        }

        private Robot CreateAndTeachRobot()
        {
            var robot = new Robot(RobotParameters.MaxBateryCapacity,
                NetworkParameters.InputNeuronsCount, Genes.ToArray(), NetworkParameters.OutputNeuronsCount);

            robot.ArrayHandler.SetObstacles(GeneralParameters.SetHorizontalObstacle,
                GeneralParameters.SetVerticalObstacle, GeneralParameters.SetRandomObstacle);

            robot.TeachRobot();
            return robot;
        }

        public void Mutate()
        {
            Genes[Randomizer.GetRandomIndex(Genes.Count)] = Randomizer.GetRandomFromRange(
                                                                NetworkParameters.MinimumNumberOfNeurons,
                                                                NetworkParameters.MaximumNumberOfNeurons);
        }

        public void Repair()
        {
            while (Length > NetworkParameters.MaximumNumberOfLayers)
            {
                Genes.RemoveAt(Randomizer.GetRandomIndex(Genes.Count));
                Length--;
            }
        }

        public void SwapGenes(int startPosition, int endPosition)
        {
            var startPositionValue = Genes[startPosition];
            var endPositionValue = Genes[endPosition];
            Genes[startPosition] = endPositionValue;
            Genes[endPosition] = startPositionValue;
        }

        public void SwapWith(Genome genome, int position)
        {
            for (var i = 0; i < position; i++)
                Genes[i] = genome.Genes[i];
        }

        public void Show()
        {
            Console.Write("Layers: ");
            foreach (var layer in Genes)
                Console.Write("{0}, ", layer);
            Console.Write("fitness: ");
            Console.Write(FitnessValue);
            Console.WriteLine();
        }

        public bool Equal(Genome genome)
        {
            if (Length != genome.Length) return false;
            for (var i = 0; i < Length; i++)
                if (Genes[i] != genome.Genes[i]) return false;
            return true;
        }
    }
}