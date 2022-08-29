using Backend.Blocks;
using Backend.Blocks.Movement;
using Backend.Blocks.Starts;
using Backend.Blocks.Variable;
using Backend.Transpilers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTest.Transpilers
{
    [TestClass]
    public class TranspilerTest
    {
        [TestMethod]
        public void CorrectClassName()
        {
            var transpiler = new Transpiler();
            string code = transpiler.ConvertToCode("test", new List<IBlock>().AsQueryable());
            Assert.IsTrue(code.Contains("public class test"));
        }
        [TestMethod]
        public void GeneratedCodeTest()
        {
            IBlock forwardBlock = new ForwardBlock("Forward_1", new VariableBlock("Variable_1", 3));
            IBlock startBlock = new StartBlock();
            startBlock.Children = new List<IBlock>() { forwardBlock };

            List<IBlock> blockTest = new() { startBlock};
            ITranspiler transpiler = new Transpiler();
            string code = transpiler.ConvertToCode("test", blockTest.AsQueryable());
            Assert.IsFalse(string.IsNullOrEmpty(code), "Stringa code vuota");
        }

    }
}
