using Frontend.Models.Blocks;

namespace FrontendTest.Models
{
    public class ConcreteBlockTest
    {
        [Fact]
        public void ShouldInstantiateCorrectType()
        {
            foreach (var block in AbstractFrontEndBlock.GetEnumerableOfType())
            {
                Type correctType = block.GetType();
                Assert.IsType(correctType, block.GetInfo());
            }
        }
    }
}
