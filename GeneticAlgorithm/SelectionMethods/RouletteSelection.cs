using Common;
using GeneticAlgorithm.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithm.SelectionMethods
{
    public class RouletteSelection : ISelectionAlgorithm
    {
        public double SelectionFactor { get; }
        public int SelectionCount { get; }

        public RouletteSelection(double selectionFactor, int selectionCount)
        {
            SelectionFactor = selectionFactor;
            SelectionCount = selectionCount;
        }

        public Population RUN(Population population)
        {
            var selected = new List<Element>();
            var fitnessSum = population.Elements.Sum(x => x.Fitness);
            double[] probs = population.Elements.Select(x => x.Fitness / fitnessSum).ToArray();
            var sm = probs.Sum();
            var count = Math.Min(SelectionCount, population.Size * SelectionFactor);
            for (int i = 0; i < count; i++)
            {
                var rand = Utils.Random.NextDouble();

                double sum = 0.0;
                Element element;
                for (int k = 0; ; k++)
                {
                    sum += probs[k];
                    if (rand < sum)
                    {
                        element = population.Elements.ElementAt(k);
                        break;
                    }
                    if (k == probs.Length - 1)
                        k = 0;
                }
                selected.Add(element.Copy());
            }

            return new Population(selected);
        }
    }
}
