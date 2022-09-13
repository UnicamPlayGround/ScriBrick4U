using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Blocks.Conditional
{
    public class CollisionWithBlock : AbstractBlock
    {
        public string ObjectTag { get; set; }
        public CollisionWithBlock(string name, string objectTag) : base(name)
        {
            ObjectTag = objectTag;
        }

        public override string GetCode()
        {
            string code = "";
            code += $"if (collision.collider.tag == \"{ObjectTag}\") {{\n";
            foreach (var child in Children)
            {
                code += child.GetCode();
            }
            code += "}\n";
            return code;
        }
    }
}
