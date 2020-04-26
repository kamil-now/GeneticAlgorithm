using GeneticAlgorithm.MutationMethods;
using Xunit;

namespace GeneticAlgorithm.UnitTests.MutationMethods
{
    public class InversionMutationTest
    {
        [Fact]
        public void Should_Perform_Mutation()
        {
            var algorithm = new InversionMutation(1);
            var a = new Element(new double[] { 8D, 3D, 2D, 9D, 1D, 0D, 7D, 6D, 5D, 4D });

            algorithm.Mutate(ref a, 1, 4);
            var expectedData = new double[] { 8D, 1D, 9D, 2D, 3D, 0D, 7D, 6D, 5D, 4D };
            Assert.Equal(expectedData, a.Data);
        }
    }
}
