using GeneticAlgorithm;
using GeneticAlgorithm.Abstractions;
using System;
using System.Linq;

namespace ML_project.Functions
{
    public class SchwefelFunction : IFitnessFunction
    {
        public int DataSize => 5;
        public double MaxValue => 500;
        public double MinValue => -500;

        public void SetFitness(Population population)
        {
            foreach (var element in population.Elements)
            {

                element.Value = 418.9829 * element.Data.Length - element.Data.Sum(x => GetValue(x));
                element.Fitness = -1 * element.Value;
            }
        }
        private double GetValue(double x)
        {
            return x * Math.Sin(Math.Sqrt(Math.Abs(x)));
        }
    }
}
