namespace GeneticAlgorithm.Abstractions
{
    public interface IGeneticAlgorithm
    {
        ISelectionAlgorithm Selection { get; }
        ICrossoverAlgorithm Crossover { get; }
        IMutationAlgorithm Mutation { get; }
        void RUN(Population population);
        void STOP();
    }
}
