using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Blocks.Variable
{
    public class SetVariableBlock : AbstractBlock
    {
        public string Value { get; set; }
        public string NameVariable { get; set; }

        public string Operation { get; set; }
        public SetVariableBlock(string name, string nameVariable, string operation, string value) : base(name)
        {
            Value = value;
            NameVariable = nameVariable;
            Operation = operation;
        }

        public override string GetCode()
        {
            return $"{NameVariable} {Operation} {Value};\n";
        }
    }
}
