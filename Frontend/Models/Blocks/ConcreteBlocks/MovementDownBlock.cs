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
    /// <summary>
    /// Classe concreta che rappresenta un blocco movimento in basso
    /// </summary>
    public class MovementDownBlock : RectangleFrontEndBlock
    {
        public override IFrontEndBlock GetInfo()
        {
            IBlockEditItem editItem = new EntryEditItem(
                "Digita di quanto vuoi muovere lo sprite in basso: ",
                TypeValue.NUMBER,
                "La quantità di passi deve essere un numero."
            );
            return new BlockBuilder<MovementDownBlock>("MovimentoGiu", BlockType.Movimento, BlockCategory.Movimento)
                .AddLabel("muovi di TOT passi in basso")
                .AddQuestion(editItem)
                .AddTextDroppedFunction(() => { return "muovi di " + editItem.ToString() + " passi in basso"; })
                .Build();
        }
    }
}
