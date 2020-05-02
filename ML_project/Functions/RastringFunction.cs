using GeneticAlgorithm;
using GeneticAlgorithm.Abstractions;
using System;
using System.Linq;

namespace ML_project.Functions
{
    public class RastringFunction : IFitnessFunction
    {
        public int DataSize => 5;
        public double MaxValue => 5.12;
        public double MinValue => -5.12;

        public void SetFitness(Population population)
        {
            foreach (var element in population.Elements)
            {
                element.Value = 10 * element.Data.Length + element.Data.Sum(x => GetValue(x));
                element.Fitness = -1 * element.Value;
            }
        }
        private double GetValue(double x)
        {
            return Math.Pow(x, 2) - 10 * Math.Cos(2 * Math.PI * x);
        }
    }
}
