using Frontend.Helpers.Builders;
using Frontend.Models.Blocks.AbstractTypeBlocks;
using Frontend.Models.QuestionItem;
using Frontend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Models.Blocks.ConcreteBlocks
{
    public class CollisionWithBlock : WithChildrenFrontEndBlock
    {
        public override IFrontEndBlock GetInfo()
        {
            IBlockEditItem editItems = new EntryEditItem("Digita il nome dell'oggetto con cui hai colliso: ", TypeValue.STRING, "Valore non valido");

            return new BlockBuilder<CollisionWithBlock>("CollisionWith", BlockType.Condizionale, BlockCategory.Controllo)
                .AddLabel("HAI COLLISO CON ")
                .AddQuestion(editItems)
                .AddTextDroppedFunction(() => { return $"Hai colliso con {editItems.Value}"; })
                .Build();

        }
    }
}
