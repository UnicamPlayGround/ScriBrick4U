using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Blocks.Starts
{
    public class ColliderBlock : AbstractBlock
    {
        public ColliderBlock() : base("Collider")
        {
        }

        public override string GetCode()
        {
            string code = "";
            code += "void OnCollisionEnter2D(Collision2D collision){\n";
            foreach (var children in Children)
            {
                code += children.GetCode();
            }
            code += "}\n";
            return code;
        }
    }
}
