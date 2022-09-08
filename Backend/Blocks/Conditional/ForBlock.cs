using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Blocks.Conditional
{
    public class ForBlock : AbstractBlock
    {
        private string Repetitions { get; set; }
        public ForBlock(string name, string repetitions) : base(name)
        {
            Repetitions = repetitions;
        }

        public override string GetCode()
        {
            string code = "";
            code += $"for( int i=0; i<{Repetitions}; i++){{\n";
            foreach (var child in Children)
            {
                code += child.GetCode();
            }
            code += "}\n";
            return code;
        }
    }
}
