using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Blocks.Function
{

    public class FunctionDefinitionBlock : AbstractBlock
    {
        public string ReturnType { get; set; }
        public string Visibility { get; set; }
        public string FunctionName { get; set; }

        public FunctionDefinitionBlock(string name, string visibility, string returnType, string functionName) : base(name)
        {
            ReturnType = returnType;
            Visibility = visibility;
            FunctionName = functionName;
        }

        public override string GetCode()
        {
            string code = "";
            code += $"{Visibility} {ReturnType} {FunctionName} () {{\n";
            foreach () foreach (var child in Children)
                {
                    code += child.GetCode();
                }
            code += "}\n";
            return code;
        }
    }
}
