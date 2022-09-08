using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Blocks.Starts
{
    public class UpdateBlock : AbstractBlock
    {
        public UpdateBlock() : base("Update")
        {
        }

        public override string GetCode()
        {
            string code = "";
            code += "private void Update(){\n";
            foreach(var children in Children)
            {
                code += children.GetCode();
            }
            code += "}\n";
            return code;
        }
    }
}
