using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Blocks.Function
{
    public class ReturnBlock : AbstractBlock
    {
        string ReturnValue { get; set; } = "";
        public ReturnBlock(string name, string returnValue) : base(name)
        {
            ReturnValue = returnValue;
        }

        public override string GetCode()
        {
            return $"return {ReturnValue};\n";
        }
    }
}
