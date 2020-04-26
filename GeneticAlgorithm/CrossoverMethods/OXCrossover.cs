using GeneticAlgorithm.Abstractions;
using System.Linq;

namespace GeneticAlgorithm.CrossoverMethods
{
    public class OXCrossover : ICrossoverAlgorithm
    {
        public double CrossoverChance { get; }

        public OXCrossover(double crossoverChance)
        {
            CrossoverChance = crossoverChance;
        }
        public void Crossover(ref Element a, ref Element b, int startCrossoverIndex, int endCrossoverIndex = -1)
        {
            var copyA = a.Copy();
            var copyB = b.Copy();
            a = new Element(Crossover(copyA.Data, copyB.Data, startCrossoverIndex, endCrossoverIndex));
            b = new Element(Crossover(copyB.Data, copyA.Data, startCrossoverIndex, endCrossoverIndex));
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
                if (i > crossStartIndex && i <= crossEndIndex)
                {
                    retval[i] = b[i];
                }
            }
            for (int i = 0, j = 0; i < size; i++)
            {
                var tmp = a[i];
                if (!retval.Contains(tmp))
                {
                    retval[j] = tmp;
                    j = j + 1 > crossStartIndex  && j < crossEndIndex ? crossEndIndex + 1 : j + 1;
                }
            }

            return retval;
        }
    }
}
