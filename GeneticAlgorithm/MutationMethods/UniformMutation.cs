using Common;
using GeneticAlgorithm.Abstractions;

namespace GeneticAlgorithm.MutationMethods
{
    public class UniformMutation : IMutationAlgorithm
    {
        public double Max { get; }
        public double Min { get; }
        public double MutationChance { get; }
        public UniformMutation(double mutationChance, double min, double max)
        {
            MutationChance = mutationChance;
            Max = max;
            Min = min;
        }
        public Element Mutate(ref Element element, int gene, int endGene = -1)
        {
            var data = element.Data;
            double value;
            do
            {
                value = Utils.RandomDouble(Min, Max);
            } while (value == 0);

            data[gene] += value;
            return new Element(data);
        }
    }
}
