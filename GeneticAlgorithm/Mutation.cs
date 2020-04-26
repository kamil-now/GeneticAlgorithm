using Common;
using GeneticAlgorithm.Abstractions;

namespace GeneticAlgorithm
{
    public class Mutation
    {
        public IMutationAlgorithm Algorithm { get; }

        public Mutation(IMutationAlgorithm mutationAlgorithm)
        {
            Algorithm = mutationAlgorithm;
        }
        public Population RUN(Population population)
        {
            var newPopulation = new Population();
            for (int i = 0; i < population.Elements.Count; i++)
            {
                var element = population.Elements[i];
                if (Utils.Random.NextDouble() >= 1 - Algorithm.MutationChance)
                {
                    var length = element.Data.Length;
                    var startIndex = Utils.Random.Next(0, length);
                    var endIndex = Utils.Random.Next(0, length);
                    Algorithm.Mutate(ref element, startIndex, endIndex);
                }
                newPopulation.Add(element);
            }
            return newPopulation;
        }
    }
}
