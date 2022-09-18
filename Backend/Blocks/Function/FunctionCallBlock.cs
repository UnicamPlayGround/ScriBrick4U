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
        public string ReturnVariable { get; set; }
        public FunctionCallBlock(string name, string functionName, string returnVariable) : base(name)
        {
            FunctionName = functionName;
            ReturnVariable = returnVariable;
        }

        public override string GetCode()
        {
            string code = "";
            if (!string.IsNullOrEmpty(ReturnVariable))
            {
                code += $"{ReturnVariable} = ";
            }
            code += $"{FunctionName}();\n";
            return code;
        }
    }
}
