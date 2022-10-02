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
    public class PositionBlock : RectangleFrontEndBlock
    {
        public override IFrontEndBlock GetInfo()
        {
            List<IBlockEditItem> editItems = new()
            {
                new EntryEditItem(
                    "Digita il valore per la x:",
                    TypeValue.NUMBER,
                    "Valore non valido."
                ),
                new EntryEditItem(
                    "Digita il valore per la y:",
                    TypeValue.NUMBER,
                    "Valore non valido."
                )
            };
            return new BlockBuilder<PositionBlock>("Posizione", BlockType.Movimento, BlockCategory.Movimento)
                .AddLabel("Posizione sprite")
                .AddQuestions(editItems)
                .AddTextDroppedFunction(() => $"x = {editItems[0].Value}; y = {editItems[1].Value}")
                .Build();
        }
    }
}
