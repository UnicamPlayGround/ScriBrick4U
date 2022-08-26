using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Blocks.Variable
{
    public class VariableBlock : AbstractBlock
    {
        public int Value { set; get; }
        public VariableBlock(string name, int value) : base("Variable", name)
        {
            Value = value;
        }

        public override string GetCode()
        {
            return $"{Value}";
        }

        public override Dictionary<string, string> GetVariables()
        {
            throw new NotImplementedException();
        }
    }
}
