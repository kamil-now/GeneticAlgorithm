using Common;
using GeneticAlgorithm.Abstractions;
using System;

namespace GeneticAlgorithm
{
    public class InversionMutation : Mutation
    {
        public InversionMutation(double mutationChance) : base(mutationChance)
        {
        }
        protected override Element Mutate(Element element)
        {
            var data = new double[element.Data.Length];
            int gen1 = Utils.Random.Next(0, data.Length);
            int gen2 = Utils.Random.Next(0, data.Length);

            if (gen1 > gen2)
            {
                int tmp = gen2;
                gen2 = gen1;
                gen1 = tmp;
            }

            var tmpArr = new double[gen2 - gen1];

            for (int p = gen1, x = 0; p < gen2; p++, x++)
            {
                tmpArr[x] = data[p];
            }

            Array.Reverse(tmpArr);

            for (int p = gen1, x = 0; p < gen2; p++, x++)
            {
                data[p] = tmpArr[x];
            }
            return new Element(data);
        }
    }
}
