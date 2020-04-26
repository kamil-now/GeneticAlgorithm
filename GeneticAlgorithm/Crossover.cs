using Common;
using GeneticAlgorithm.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithm
{
    public class Crossover
    {
        public ICrossoverAlgorithm Algorithm { get; }

        public Crossover(ICrossoverAlgorithm crossoverAlgorithm)
        {
            Algorithm = crossoverAlgorithm;
        }
        public Population RUN(Population population)
        {
            var populationSize = population.Size;
            var offspring = new List<Element>();
            foreach (var element in population.Elements)
            {
                if (Utils.Random.NextDouble() >= 1 - Algorithm.CrossoverChance)
                {
                    var randomIndex = Utils.Random.Next(populationSize);
                    var a = element.Copy();
                    var b = population.Elements.ElementAt(randomIndex);
                    if (a.Data == b.Data)
                    {
                        var i = randomIndex;
                        do
                        {
                            i = i == populationSize ? 0 : i + 1;

                            b = population.Elements.ElementAt(i);
                        } while (a.Data == b.Data && i != randomIndex);

                    };
                    var length = a.Data.Length;
                    var crossStartIndex = Utils.Random.Next(length - 1) + 1;
                    var crossEndIndex = Utils.Random.Next(length - 1) + 1;
                    if (crossStartIndex > crossEndIndex)
                    {
                        var tmp = crossStartIndex;
                        crossStartIndex = crossEndIndex;
                        crossEndIndex = tmp;
                    }
                    Algorithm.Crossover(ref a, ref b, crossStartIndex, crossEndIndex);

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
    }
}
