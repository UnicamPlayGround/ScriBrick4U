using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Blocks.Function
{
    public class FunctionCallBlock : AbstractBlock
    {
        public string FunctionName { get; set; }
        public FunctionCallBlock(string name, string functionName) : base(name)
        {
            FunctionName = functionName;
        }

        public override string GetCode()
        {
            return $"{FunctionName}();\n";
        }
    }
}
