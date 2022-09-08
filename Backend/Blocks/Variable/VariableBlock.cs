using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Blocks.Variable
{
    public class VariableBlock : AbstractBlock
    {
        public string NameVariable { get; set; }
        public string Type { get; set; }
        public VariableBlock(string name, string type, string nameVariable) : base(name)
        {
            Type = type;
            NameVariable = nameVariable;
        }

        public override string GetCode()
        {
            return $"{Type} {NameVariable};\n";
        }
    }
}
