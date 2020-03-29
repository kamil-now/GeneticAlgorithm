using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithm
{
    public class Element
    {
        public double Value { get; set; }
        public double Fitness { get; set; }
        public double[] Data { get; }

        public Element(IEnumerable<double> data)
        {
            Data = (double[])data.ToArray().Clone();
        }

        public Element Copy() => new Element(Data) { Fitness = Fitness, Value = Value };
    }
}