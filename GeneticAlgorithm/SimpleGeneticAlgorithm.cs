using GeneticAlgorithm.Abstractions;
using System;

namespace GeneticAlgorithm
{
    public class SimpleGeneticAlgorithm : IGeneticAlgorithm
    {
        public event Action<Population, ulong> IterationCompleted;
        public ISelectionAlgorithm Selection { get; }
        public ICrossoverAlgorithm Crossover { get; }
        public IMutationAlgorithm Mutation { get; }
        public Action<Population> FitnessFunction { get; }

        private Crossover _crossover;
        private bool _stop;

        public SimpleGeneticAlgorithm(
            ISelectionAlgorithm selection,
            ICrossoverAlgorithm crossover,
            IMutationAlgorithm mutation,
            Action<Population> fitnessFunction)
        {
            Selection = selection;
            Crossover = crossover;
            Mutation = mutation;
            FitnessFunction = fitnessFunction;
            _crossover = new Crossover(crossover);
        }

        public void STOP() => _stop = true;
        public void RUN(Population population)
        {
            FitnessFunction(population);
            var i = 0ul;
            while (!_stop)
            {
                population = Selection.RUN(population);
                population = _crossover.RUN(population);
                population = Mutation.RUN(population);
                FitnessFunction(population);

                IterationCompleted?.Invoke(population, ++i);
            }
        }
    }
}