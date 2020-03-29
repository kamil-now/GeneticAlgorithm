using Common.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithm
{
    public class Population
    {
        public int Size => Elements.Count;
        public IList<Element> Elements { get; private set; }

        public Population(IList<Element> elements)
        {
            Elements = new List<Element>(elements);
        }
        public Population()
        {
            Elements = new List<Element>();
        }
        public Population Copy() => new Population(Elements.Select(x => x.Copy()).ToList());
        public void Add(Element element)
        {
            Elements.Add(element);
        }
        public void Add(IEnumerable<Element> elements)
        {
            Elements.AddRange(elements);
        }
        public void Remove(Element element)
        {
            if (Elements.Contains(element))
                Elements.Remove(element);
        }
        public void Clear()
        {
            Elements = new List<Element>();
        }
    }
}
