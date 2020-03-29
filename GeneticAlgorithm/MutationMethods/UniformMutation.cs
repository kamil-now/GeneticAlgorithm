using Common;

namespace GeneticAlgorithm.Abstractions
{
    public class UniformMutation : Mutation
    {
        public double Max { get; }
        public double Min { get; }
        public UniformMutation(double mutationChance, double min, double max):base(mutationChance)
        {
            Max = max;
            Min = min;
        }
        protected override Element Mutate(Element element)
        {
            var data = element.Data;
            int gen = Utils.Random.Next(0, data.Length);

            data[gen] += Utils.RandomDouble(Min, Max);
            return new Element(data);
        }
    }
}
