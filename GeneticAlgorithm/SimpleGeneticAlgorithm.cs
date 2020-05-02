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
        public IFitnessFunction FitnessFunction { get; }

        private Crossover _crossover;
        private Mutation _mutation;
        private bool _stop;

        public SimpleGeneticAlgorithm(
            ISelectionAlgorithm selection,
            ICrossoverAlgorithm crossover,
            IMutationAlgorithm mutation,
            IFitnessFunction fitnessFunction)
        {
            Selection = selection;
            Crossover = crossover;
            Mutation = mutation;
            FitnessFunction = fitnessFunction;

            _crossover = new Crossover(crossover);
            _mutation = new Mutation(mutation);
        }

        public void STOP() => _stop = true;
        public void RUN(Population population)
        {
            FitnessFunction.SetFitness(population);
            var i = 0ul;
            while (!_stop)
            {
                population = Selection.RUN(population);
                population = _crossover.RUN(population);
                population = _mutation.RUN(population);
                FitnessFunction.SetFitness(population);

                IterationCompleted?.Invoke(population, ++i);
            }
        }
    }
}