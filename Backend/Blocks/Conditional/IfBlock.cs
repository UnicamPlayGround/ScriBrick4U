using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Blocks.Conditional
{
    public class IfBlock : AbstractBlock
    {
        public IBlock First { get; set; } = null!;
        public IBlock Second { get; set; } = null!;
        public string Condition { get; set; }
        public IfBlock(string name, IBlock first, string condition, IBlock second) : base("Conditional", name)
        {
            First = first;
            Second = second;
            Condition = condition;
        }

        public override string GetCode()
        {
            string code = "";
            code += $"if( ({First.GetCode()}) {Condition} ({Second.GetCode()} ){{\n";
            foreach(var child in Children)
            {
                code += child.GetCode();
            }
            code += "}\n";
            return code;
        }

        public override Dictionary<string, string> GetVariables()
        {
            return new();
        }
    }
}
