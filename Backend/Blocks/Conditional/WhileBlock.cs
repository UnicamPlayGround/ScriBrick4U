using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Blocks.Conditional
{
    public class WhileBlock : AbstractBlock
    {
        private string First { get; set; }
        private string Second { get; set; }
        private string Condition { get; set; }
        public WhileBlock(string name, string first, string condition, string second) : base(name)
        {
            First = first;
            Second = second;
            Condition = condition;
        }

        public override string GetCode()
        {
            string code = "";
            code += $"while( ( {First} ) {Condition} ( {Second} ) ){{\n";
            foreach (var child in Children)
            {
                code += child.GetCode();
            }
            code += "}\n";
            return code;
        }
    }
}
