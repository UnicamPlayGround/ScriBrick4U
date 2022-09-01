using Frontend.Builders;
using Frontend.Models.Blocks.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Model.Blocks.ConcreteBlocks
{
    public class UpdateBlock : AbstractFrontEndBlock
    {
        public UpdateBlock()
        {
            Shape = ShapeTypeMethods.GetShape(ShapeType.UPPER);
            Width = 130;
            Height = 48;
        }

        public override IFrontEndBlock GetInfo()
        {
            return new BlockBuilder<UpdateBlock>("Update", BlockType.Principale)
                .AddLabel("Update", 18)
                .AddTextDroppedFunction(() => { return "Update"; })
                .Build();
        }
    }
}
