namespace GeneticAlgorithm.Abstractions
{
    public interface IMutationAlgorithm
    {
        double MutationChance { get; }
        Population RUN(Population population);
    }
}
