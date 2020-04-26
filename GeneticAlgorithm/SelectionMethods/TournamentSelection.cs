using Common;
using GeneticAlgorithm.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithm.SelectionMethods
{
    public class TournamentSelection : ITournamentSelectionAlgorithm
    {
        public double SelectionFactor { get; }
        public int SelectionCount { get; }
        public int TournamentSize { get; }

        public TournamentSelection(double selectionFactor, int selectionCount, int tournamentSize)
        {
            SelectionFactor = selectionFactor;
            SelectionCount = selectionCount;
            TournamentSize = tournamentSize;
        }

        public Population RUN(Population population)
        {
            if (TournamentSize <= 0)
                throw new ArgumentException("Tournament size is incorrect");

            var selected = new List<Element>();
            var count = Math.Min(SelectionCount, population.Size * SelectionFactor);
            for (int i = 0; i < count; i++)
            {
                var best = RunTournament(population);
                selected.Add(best.Copy());
            }

            return new Population(selected);
        }

        private Element RunTournament(Population population)
        {
            int size = population.Size;
            double bestFitness = double.NegativeInfinity;
            Element best = null;
            for (int j = 0; j < TournamentSize; j++)
            {
                var randomIndex = Utils.Random.Next(size);
                var randomElement = population.Elements.ElementAt(randomIndex);
                if (bestFitness < randomElement.Fitness)
                {
                    bestFitness = randomElement.Fitness;
                    best = randomElement;

                }
            }
            return best;
        }
    }
}
