using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NeuralNetwork.Helpers;
using NeuralNetwork.ProjectParameters;
using Remover = NeuralNetwork.TopologyEvolution.Helpers.Remover;

namespace NeuralNetwork.TopologyEvolution
{
    public class Evolution
    {
        private int PopulationSize { get; }
        private int NumberOfBests { get; }
        public List<Genome> Bests { get; set; }
        private int NumberOfParents { get; }
        public List<Genome> Parents { get; set; }
        private int NumberOfChildren { get; }
        public List<Genome> Children { get; set; }
        
        private int CrossOverChance { get; }
        private int MutationChance { get; }
        public List<Genome> Population { get; set; }
        private double PopulationFitnessValuesSum { get; set; }

        private readonly StreamWriter _streamWriter =
            new StreamWriter(@"C:\Users\Marek\Desktop\PI\NeuralNetwork\TrainedNetworks\EvolutionTest\test.txt", true);


        public Evolution(int? populationSize = null, int? numberOfBests = null, 
                            int? numberOfParents = null, int? numberOfChildren = null,
                            int? crossOverChance = null, int? mutationChance = null)
        {
            PopulationSize = populationSize ?? EvolutionParameters.DefaultPopulationSize;
            NumberOfBests = numberOfBests ?? EvolutionParameters.DefaultNumberOfBests;
            NumberOfParents = numberOfParents ?? EvolutionParameters.DefaultNumberOfParents;
            NumberOfChildren = numberOfChildren ?? EvolutionParameters.DefaultNumberOfChildren;

            CrossOverChance = crossOverChance ?? EvolutionParameters.DefaultCrossOverChance;
            MutationChance = mutationChance ?? EvolutionParameters.DefaultMutationChance;

            SimulationParameters.StartPositionX = SimulationParameters.DefaultStartPositionX;
            SimulationParameters.StartPositionY = SimulationParameters.DefaultStartPositionY;
            SimulationParameters.NumberOfEpochs = SimulationParameters.DefaultNumberOfEpochs;
            SimulationParameters.NumberOfExpedicions = SimulationParameters.DefaultNumberOfExpedicions;
            SimulationParameters.NumberOfExploringSteps = SimulationParameters.DefaultNumberOfExploringSteps;
            SimulationParameters.NumberOfTestingSteps = SimulationParameters.DefaultNumberOfTestingSteps;
            SimulationParameters.SetHorizontalObstacle = true;
            SimulationParameters.SetVerticalObstacle = true;

            InitializePopulation();
            OrderPopulation();
        }

        private void InitializePopulation()
        {
            Population = new List<Genome>();

            for (var i = 0; i < PopulationSize; i++)
                Population.Add(new Genome());

            Bests = new List<Genome>(NumberOfBests);
            Parents = new List<Genome>(NumberOfParents);
            Children = new List<Genome>(NumberOfChildren);
        }

        public void SimulateEvolution(int numberOfGenerations)
        {
            for (var i = 0; i < numberOfGenerations; i++)
            {
                Console.WriteLine("{0} generation: ", i);
                Show();
                Console.WriteLine("Bests: ");
                foreach (var genome in Bests)
                    genome.Show();
                NextGeneration();
            }
        }

        public void NextGeneration()
        {
            CheckIfPopulationIsCreated();

            while (Population.Count > PopulationSize)
                Population.RemoveAt(Population.Count);

            CalculatePopulationFitnessValues();
            OrderPopulation();

            Bests.Clear();

            Bests = Population.Take(NumberOfBests).ToList();
            while (Parents.Count < NumberOfParents)
            {
                SpinBiasedRouletteWheel();
                Parents = Remover.RemoveSameElementsInPopulation(Parents);
            }
            while (Children.Count < NumberOfChildren)
            {
                CrossOver();
                Children = Remover.RemoveSameElementsInPopulation(Children);
            }

            var nextGeneration = Bests.ToList();
            nextGeneration.AddRange(Parents);

            //var i = nextGeneration.Count;
            //while (nextGeneration.Count < PopulationSize)
            //{
            //    nextGeneration.Add(Population[i]);
            //    i++;
            //}
            nextGeneration.AddRange(Children);

            nextGeneration = Remover.RemoveSameElementsInPopulation(nextGeneration);
            while (nextGeneration.Count < PopulationSize)
                nextGeneration.Add(new Genome());

            Population = nextGeneration;
            Repair();
            Mutate();
            OrderPopulation();
        }

        private void CheckIfPopulationIsCreated()
        {
            if (Population.Count == 0)
                throw new InvalidOperationException("Error! Population not initialized");
        }

        public void SpinBiasedRouletteWheel()
        {
            CheckIfPopulationIsCreated();
            
            foreach (var genome in Population)
            {
                // Chance that genome is picked.
                var percentage = genome.FitnessValue / PopulationFitnessValuesSum * 100;
                var randomNumber = Randomizer.GetRandomFromRange(1, 100);
                // Actual wheel: if the percentage lies within random number (1-100), return it
                if ((percentage <= 0 || randomNumber <= percentage) && Parents.Count <= NumberOfParents )
                    Parents.Add(genome);
            }
        }

        public void CalculatePopulationFitnessValues()
        {
            //Parallel.ForEach(Population, genome => genome.CalculateFitnessValue());
            foreach (var genome in Population)
            {
                genome.CalculateFitnessValue();
            }
            PopulationFitnessValuesSum = Population.Sum(x => x.FitnessValue);
        }

        public void CalculateBestsFitnessValues()
        {
            //Parallel.ForEach(Bests, genome => genome.CalculateFitnessValue());
            foreach (var genome in Bests)
            {
                genome.CalculateFitnessValue();
            }
        }

        public void CrossOver()
        {
            CheckIfPopulationIsCreated();

            decimal percentage = CrossOverChance;
            var randomNumber = Randomizer.GetRandomFromRange(1, 100);

            if (percentage <= 0 || randomNumber > percentage) return;
            for (var i = 0; i < Parents.Count - 1; i++)
            {
                if (i + 1 > Parents.Count) break;

                var firstGenome = Parents[i];
                var secondGenome = Parents[i + 1];

                var pointOfCross = GetPointOfCross(firstGenome, secondGenome);

                var copyFirstGenome = new Genome(firstGenome);
                var copySecondGenome = new Genome(secondGenome);

                copyFirstGenome.SwapWith(secondGenome, pointOfCross);
                copyFirstGenome.Length = copyFirstGenome.Genes.Count;
                copySecondGenome.SwapWith(firstGenome, pointOfCross);
                copySecondGenome.Length = copySecondGenome.Genes.Count;
                
                Children.Add(copyFirstGenome);
                Children.Add(copySecondGenome);

            }
        }

        public int GetPointOfCross(Genome firstGenome, Genome secondGenome)
        {
            var shorterGenome = firstGenome.Length > secondGenome.Length ? secondGenome : firstGenome;
            return Randomizer.GetRandomFromRange(1, shorterGenome.Length);
        }

        public void Mutate()
        {
            CheckIfPopulationIsCreated();

            decimal percentage = MutationChance;
            var randomNumber = Randomizer.GetRandomFromRange(1, 100);

            if (percentage <= 0 || randomNumber > percentage) return;

            for (var i = 0; i < Population.Count; i++)
            {
                if (i > Population.Count) break;
                Population[i].Mutate();
            }
        }

        public void Repair()
        {
            //Parallel.ForEach(Population, genome => genome.Repair());
            foreach (var genome in Population)
            {
                genome.Repair();
            }
        }

        public void Show()
        {
            foreach (var genome in Population)
            {
                foreach (var gene in genome.Genes)
                {
                    Console.Write("{0}, ", gene);
                }
                Console.Write("fitness: {0}", genome.FitnessValue);
                Console.WriteLine();
            }

            foreach (var genome in Population)
            {
                foreach (var gene in genome.Genes)
                {
                    _streamWriter.Write("{0}, ", gene);
                }
                _streamWriter.Write("fitness: {0}", genome.FitnessValue);
                _streamWriter.WriteLine();
            }
            _streamWriter.WriteLine();
        }

        public void OrderPopulation()
        {
            Population = Population.OrderByDescending(x => x.FitnessValue).ToList();
        }
    }
}