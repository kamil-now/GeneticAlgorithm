namespace GeneticAlgorithm.Abstractions
{
    public interface IMutationAlgorithm
    {
        double MutationChance { get; }
        Element Mutate(ref Element element, int gene, int endGene);
    }
}
