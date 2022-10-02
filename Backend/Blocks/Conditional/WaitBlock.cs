using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Blocks.Conditional
{
    /// <summary>
    /// Definisce il blocco per l'attesa di n secondi
    /// </summary>
    public class WaitBlock : AbstractBlock
    {
        public string Value { get; }
        public WaitBlock(string name, string value) : base(name)
        {
            Value = value;
        }

        public override string GetCode()
        {
            var code = $"float {Name} = 0;\n";
            code += $"while({Name} < {Value}){{\n";
            code += $"{Name} += Time.deltaTime;\n";
            code += "}\n";
            return code;
        }
    }
}
