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
    public class SpeedBlock : RectangleFrontEndBlock
    {
        public override IFrontEndBlock GetInfo()
        {
            IBlockEditItem editItem = new EntryEditItem(
                "Digita il valore per la velocità: ",
                TypeValue.NUMBER,
                "La velocità deve essere un numero."
            );
            return new BlockBuilder<SpeedBlock>("Velocita", BlockType.Movimento, BlockCategory.Movimento)
                .AddLabel("Modifica velocità")
                .AddQuestion(editItem)
                .AddTextDroppedFunction(() => "velocità = " + editItem.ToString())
                .Build();
        }
    }
}
