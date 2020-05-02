using System;

namespace GeneticAlgorithm.Abstractions
{
    public interface IFitnessFunction
    {
        int DataSize { get; }
        double MaxValue { get; }
        double MinValue { get; }
        void SetFitness(Population population);
    }
}
