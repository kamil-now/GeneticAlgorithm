using GeneticAlgorithm.MutationMethods;
using Xunit;

namespace GeneticAlgorithm.UnitTests.MutationMethods
{
    public class UniformMutationTest
    {
        [Fact]
        public void Should_Perform_Mutation()
        {
            var algorithm = new UniformMutation(1, -0.1, 0.1);
            var a = new Element(new double[] { 8D, 3D, 2D, 9D, 1D, 0D, 7D, 6D, 5D, 4D });
            var index = 1;
            algorithm.Mutate(ref a, index);
            Assert.NotEqual(3D, a.Data[1]);
            Assert.InRange(a.Data[1], 2.9, 3.1);
        }
    }
}
