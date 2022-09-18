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
        public string Scope { get; set; }
        public VariableBlock(string name, string scope, string type, string nameVariable) : base(name)
        {
            Type = type;
            NameVariable = nameVariable;
            Scope = scope;
        }

        public override string GetCode()
        {
            if(Scope == "local")
            {
                return $"{Type} {NameVariable};\n";
            }
            return ""; 
        }

        public override Dictionary<string, string> GetVariables()
        {
            if(Scope == "global")
            {
                return new()
                {
                    { NameVariable, Type }
                };
            }
            return new();
        }

    }
}
