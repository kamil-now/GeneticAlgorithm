namespace GeneticAlgorithm.Abstractions
{
    public interface ICrossoverAlgorithm
    {
        double CrossoverChance { get; }
        void Crossover(ref Element a, ref Element b, int startCrossoverIndex, int endCrossoverIndex);
    }
}