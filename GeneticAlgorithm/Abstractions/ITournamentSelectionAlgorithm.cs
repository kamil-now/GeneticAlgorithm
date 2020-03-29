namespace GeneticAlgorithm.Abstractions
{
    public interface ITournamentSelectionAlgorithm : ISelectionAlgorithm
    {
        int TournamentSize { get; }
    }
}
