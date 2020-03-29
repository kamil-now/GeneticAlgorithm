namespace GeneticAlgorithm.Abstractions
{
    public interface ICrossoverAlgorithm
    {
        double CrossoverChance { get; }
        Population RUN(Population population);
    }
}