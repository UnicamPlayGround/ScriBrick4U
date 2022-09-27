using Backend.Blocks;
using Backend.Blocks.Movement;
using Backend.Blocks.Starts;
using Backend.Transpilers;

namespace BackendTest.Transpilers
{
    public class TranspilerTest
    {
        [Fact]
        public void CorrectClassName()
        {
            var transpiler = new Transpiler();
            string code = transpiler.ConvertToCode("test", new List<IBlock>().AsQueryable());
            Assert.Contains("public class test", code);
        }
        [Fact]
        public void GeneratedCodeTest()
        {
            IBlock forwardBlock = new ForwardBlock("Forward_1", "3");
            IBlock startBlock = new StartBlock();
            startBlock.Children = new List<IBlock>() { forwardBlock };

            List<IBlock> blockTest = new() { startBlock };
            ITranspiler transpiler = new Transpiler();
            string code = transpiler.ConvertToCode("test", blockTest.AsQueryable());
            Assert.False(string.IsNullOrEmpty(code), "Stringa code vuota");
        }
    }
}
