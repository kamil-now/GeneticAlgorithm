using GeneticAlgorithm;
using GeneticAlgorithm.Abstractions;
using System;

namespace ML_project.Functions
{
    public class SinCosFunction : IFitnessFunction
    {
        public int DataSize => 2;
        public double MaxValue => 0.1;
        public double MinValue => -0.1;
        public void SetFitness(Population population)
        {
            foreach (var element in population.Elements)
            {
                element.Value = GetValue(element);
                element.Fitness = -1 * element.Value;
            }
        }
        private double GetValue(Element element)
        {
            var x = element.Data[0];
            var y = element.Data[1];

            return Math.Sin(x) * Math.Cos(y);
        }
    }
}
