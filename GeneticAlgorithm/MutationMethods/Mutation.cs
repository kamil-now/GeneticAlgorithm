using Common;
using GeneticAlgorithm.Abstractions;

namespace GeneticAlgorithm.MutationMethods
{
    public abstract class Mutation : IMutationAlgorithm
    {
        public double MutationChance { get; }
        protected Mutation(double mutationChance)
        {
            MutationChance = mutationChance;
        }
        protected abstract Element Mutate(Element element);
        public Population RUN(Population population)
        {
            var newPopulation = new Population();
            foreach (var element in population.Elements)
            {
                if (Utils.Random.NextDouble() < MutationChance)
                {
                    newPopulation.Add(Mutate(element));
                }
                newPopulation.Add(element);
            }
            return newPopulation;
        }

    }
}
