using System;
using System.Collections.Generic;
using System.Linq;
using NeuralNetwork.ProjectParameters;
using NeuralNetwork.TopologyEvolution.Helpers;

namespace NeuralNetwork.TopologyEvolution
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var evolution = new Evolution();
            evolution.SimulateEvolution(EvolutionParameters.DefaultNumberOfGenerations);

            //SimulationParameters.StartPositionX = SimulationParameters.DefaultStartPositionX;
            //SimulationParameters.StartPositionY = SimulationParameters.DefaultStartPositionY;
            //SimulationParameters.NumberOfEpochs = SimulationParameters.DefaultNumberOfEpochs;
            //SimulationParameters.NumberOfExpedicions = SimulationParameters.DefaultNumberOfExpedicions;
            //SimulationParameters.NumberOfExploringSteps = SimulationParameters.DefaultNumberOfExploringSteps;
            //SimulationParameters.NumberOfTestingSteps = SimulationParameters.DefaultNumberOfTestingSteps;
            //SimulationParameters.SetHorizontalObstacle = true;
            //SimulationParameters.SetVerticalObstacle = true;

            //var list = new List<Genome>
            //{
            //    new Genome(new List<int>() {75, 47}),
            //    new Genome(new List<int>() {5}),
            //    new Genome(new List<int>() {17, 33}),
            //    new Genome(new List<int>() {23, 9}),
            //    new Genome(new List<int>() {16, 22}),
            //    new Genome(new List<int>() {43, 21}),
            //    new Genome(new List<int>() {54, 5}),
            //    new Genome(new List<int>() {71, 77}),
            //    new Genome(new List<int>() {79, 3}),
            //    new Genome(new List<int>() {26}),
            //    new Genome(new List<int>() {49, 10}),
            //    new Genome(new List<int>() {36, 80}),
            //    new Genome(new List<int>() {67, 19}),
            //    new Genome(new List<int>() {49, 15}),
            //    new Genome(new List<int>() {80, 19}),
            //    new Genome(new List<int>() {22, 12}),
            //    new Genome(new List<int>() {52, 79}),
            //    new Genome(new List<int>() {28, 27}),
            //    new Genome(new List<int>() {69, 25}),
            //    new Genome(new List<int>() {19, 24}),
            //    new Genome(new List<int>() {74, 65}),
            //    new Genome(new List<int>() {54, 72}),
            //    new Genome(new List<int>() {48, 9}),
            //    new Genome(new List<int>() {21, 13}),
            //    new Genome(new List<int>() {24, 7}),
            //    new Genome(new List<int>() {64, 14}),
            //    new Genome(new List<int>() {44, 75}),
            //    new Genome(new List<int>() {58, 37}),
            //    new Genome(new List<int>() {27, 16}),
            //    new Genome(new List<int>() {58, 37}),
            //    new Genome(new List<int>() {24, 18}),
            //    new Genome(new List<int>() {21, 80}),
            //    new Genome(new List<int>() {30, 14}),
            //    new Genome(new List<int>() {58, 37}),
            //    new Genome(new List<int>() {14, 14}),
            //    new Genome(new List<int>() {33, 60}),
            //    new Genome(new List<int>() {15, 26}),
            //    new Genome(new List<int>() {14, 14}),
            //    new Genome(new List<int>() {65, 13}),
            //    new Genome(new List<int>() {9, 52}),
            //    new Genome(new List<int>() {44, 24}),
            //    new Genome(new List<int>() {59, 33}),
            //    new Genome(new List<int>() {60, 43}),
            //    new Genome(new List<int>() {20, 57}),
            //    new Genome(new List<int>() {18, 47}),
            //    new Genome(new List<int>() {61, 14}),
            //    new Genome(new List<int>() {56, 60}),
            //    new Genome(new List<int>() {17, 30}),
            //    new Genome(new List<int>() {56, 52}),
            //    new Genome(new List<int>() {14, 2}),
            //    new Genome(new List<int>() {47, 20}),
            //    new Genome(new List<int>() {34, 66}),
            //    new Genome(new List<int>() {51, 6}),
            //    new Genome(new List<int>() {74, 53}),
            //    new Genome(new List<int>() {34, 57})
            //};


            //list.OrderByDescending(x => x.FitnessValue);

            //foreach (var genome in list)
            //{
            //    genome.Show();
            //}

            Console.ReadKey();
        }
    }
}
