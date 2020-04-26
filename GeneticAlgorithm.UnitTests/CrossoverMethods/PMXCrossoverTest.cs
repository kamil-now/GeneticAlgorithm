using GeneticAlgorithm.CrossoverMethods;
using Xunit;

namespace GeneticAlgorithm.UnitTests
{
    public class PMXCrossoverTest
    {
        [Fact]
        public void Should_Perform_Crossover()
        {
            var algorithm = new PMXCrossover(1);
            var a = new Element(new double[] { 0D, 1D, 2D, 3D, 4D, 5D, 6D, 7D, 8D, 9D });
            var b = new Element(new double[] { 8D, 3D, 2D, 9D, 1D, 0D, 7D, 6D, 5D, 4D });

            algorithm.Crossover(ref a, ref b, 2, 6);

            Assert.Equal(new double[] { 5D, 4D, 2D, 9D, 1D, 0D, 7D, 6D, 8D, 3D }, a.Data);
            Assert.Equal(new double[] { 8D, 9D, 2D, 3D, 4D, 5D, 6D, 7D, 0D, 1D }, b.Data);
        }
    }
}
