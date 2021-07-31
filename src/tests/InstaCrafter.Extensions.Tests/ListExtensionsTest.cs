using System.Linq;
using Xunit;

namespace InstaCrafter.Extensions.Tests
{
    public class ListExtensionsTest
    {
        [Fact]
        public void Test_Randomise_Should_Generate_Different_Sequence()
        {
            var testList = Enumerable.Range(0, 100);

            var result = testList.Randomize();

            Assert.False(testList.SequenceEqual(result));
        }
    }
}