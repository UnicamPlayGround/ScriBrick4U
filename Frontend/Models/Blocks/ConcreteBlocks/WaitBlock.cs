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
    public class WaitBlock : RectangleFrontEndBlock
    {
        public override IFrontEndBlock GetInfo()
        {
            IBlockEditItem editItem = new EntryEditItem(
                "Digita di quanto vuoi attendere: ",
                TypeValue.NUMBER,
                "La quantità di passi deve essere un numero."
            );
            return new BlockBuilder<WaitBlock>("Wait", BlockType.Condizionale, BlockCategory.Controllo)
                .AddLabel("Aspetta n secondi")
                .AddQuestion(editItem)
                .AddTextDroppedFunction(() => $"Aspetta {editItem} secondi")
                .Build();
        }
    }
}
