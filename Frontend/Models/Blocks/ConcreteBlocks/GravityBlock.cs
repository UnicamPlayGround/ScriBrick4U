using Frontend.Helpers.Builders;
using Frontend.Models.Blocks.AbstractTypeBlocks;
using Frontend.Models.QuestionItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Models.Blocks.ConcreteBlocks
{
    public class GravityBlock : RectangleFrontEndBlock
    {
        public override IFrontEndBlock GetInfo()
        {
            IBlockEditItem editItem = new EntryEditItem(
                "Digita il valore per la gravità: ",
                TypeValue.NUMBER,
                "La gravità deve essere un numero."
            );
            return new BlockBuilder<GravityBlock>("Gravita", BlockType.Movimento, BlockCategory.Movimento)
                .AddLabel("Modifica gravità")
                .AddQuestion(editItem)
                .AddTextDroppedFunction(() => "gravità = " + editItem.ToString())
                .Build();
        }
    }
}
