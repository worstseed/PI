using System.Collections.Generic;
using System.Linq;

namespace NeuralNetwork.TopologyEvolution.Helpers
{
    public static class Remover
    {
        public static List<Genome> RemoveSameElementsInPopulation(List<Genome> population)
        {
            var whichToRemove = new List<int>();

            population.Reverse();

            for (var j = 0; j < population.Count; j++)
            {
                for (var k = j + 1; k < population.Count; k++)
                {
                    if (population[j].Equal(population[k]))
                        whichToRemove.Add(j);
                }
            }

            foreach (var indice in whichToRemove.OrderByDescending(v => v))
            {
                population.RemoveAt(indice);
            }

            population.Reverse();

            return population;
        }
    }
}
