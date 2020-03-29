using Common;
using GeneticAlgorithm.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithm
{
    public class PMXCrossover : ICrossoverAlgorithm
    {
        public double CrossoverChance { get; }

        public PMXCrossover(double crossoverChance)
        {
            CrossoverChance = crossoverChance;
        }
        public Population RUN(Population population)
        {
            var populationSize = population.Size;
            var offspring = new List<Element>();
            foreach (var element in population.Elements)
            {
                if (Utils.Random.NextDouble() < CrossoverChance)
                {
                    int randomIndex = Utils.Random.Next(populationSize);
                    var a = element.Copy();
                    var b = population.Elements.ElementAt(randomIndex);
                    Crossover(ref a, ref b);

                    offspring.Add(a);
                    offspring.Add(b);
                }
                else
                {
                    offspring.Add(element);
                }
            }
            return new Population(offspring);
        }
        private void Crossover(ref Element a, ref Element b)
        {
            int length = a.Data.Length;
            int crossStartIndex = Utils.Random.Next(length - 1) + 1;
            int crossEndIndex = Utils.Random.Next(length - 1) + 1;

            if (crossStartIndex > crossEndIndex)
            {
                int tmp = crossStartIndex;
                crossStartIndex = crossEndIndex;
                crossEndIndex = tmp;
            }
            var copyA = a.Copy();
            var copyB = b.Copy();
            a = new Element(Crossover(copyA.Data, copyB.Data, crossStartIndex, crossEndIndex));
            b = new Element(Crossover(copyB.Data, copyA.Data, crossStartIndex, crossEndIndex));

        }
        private double[] Crossover(double[] a, double[] b, int crossStartIndex, int crossEndIndex)
        {
            int size = b.Length;
            double[] retval = new double[size];
            for (int i = 0; i < size; i++)
            {
                retval[i] = -1;
            }
            for (int i = 0; i < size; i++)
            {
                if (i >= crossStartIndex && i <= crossEndIndex)
                {
                    retval[i] = b[i];
                }
            }
            for (int i = 0; i < size; i++)
            {
                if (i < crossStartIndex || i > crossEndIndex)
                {
                    var tmp = a[i];
                    while (retval.Contains(tmp))
                    {
                        for (int j = 0; j < retval.Length; j++)
                        {
                            if (retval[j] == tmp)
                                tmp = a[j];
                        }

                    }
                    retval[i] = tmp;
                }
            }

            return retval;
        }
    }
}
