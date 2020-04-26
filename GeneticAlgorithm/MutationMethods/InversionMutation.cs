using Common;
using GeneticAlgorithm.Abstractions;
using System;

namespace GeneticAlgorithm.MutationMethods
{
    public class InversionMutation : IMutationAlgorithm
    {
        public double MutationChance { get; }
        public InversionMutation(double mutationChance)
        {
            MutationChance = mutationChance;
        }
        public Element Mutate(ref Element element, int gene, int endGene)
        {
            var data = element.Data;

            if (gene > endGene)
            {
                int tmp = endGene;
                endGene = gene;
                gene = tmp;
            }

            var tmpArr = new double[endGene - gene + 1];

            for (int p = gene, x = 0; p <= endGene; p++, x++)
            {
                tmpArr[x] = data[p];
            }

            Array.Reverse(tmpArr);

            for (int p = gene, x = 0; p <= endGene; p++, x++)
            {
                data[p] = tmpArr[x];
            }
            return new Element(data);
        }
    }
}
