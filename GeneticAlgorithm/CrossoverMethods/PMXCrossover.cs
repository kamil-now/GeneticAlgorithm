using Common;
using GeneticAlgorithm.Abstractions;
using System.Linq;

namespace GeneticAlgorithm.CrossoverMethods
{
    public class PMXCrossover : ICrossoverAlgorithm
    {
        public double CrossoverChance { get; }

        public PMXCrossover(double crossoverChance)
        {
            CrossoverChance = crossoverChance;
        }
        public void Crossover(ref Element a, ref Element b, int startCrossoverIndex, int endCrossoverIndex)
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
