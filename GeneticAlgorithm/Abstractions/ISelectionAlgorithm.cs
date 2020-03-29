namespace GeneticAlgorithm.Abstractions
{
    public interface ISelectionAlgorithm
    {
        double SelectionFactor { get; }
        int SelectionCount { get; }
        Population RUN(Population population);
    }
}
