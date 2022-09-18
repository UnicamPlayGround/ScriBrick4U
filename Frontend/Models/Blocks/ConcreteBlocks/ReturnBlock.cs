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
    public class ReturnBlock : RectangleFrontEndBlock
    {
        public override IFrontEndBlock GetInfo()
        {
            List<IBlockEditItem> editItems = new()
            {
                new PickerEditItem(
                  "Seleziona una variabile",
                  BlockViewModel.VariableNames
                ),
                new EntryEditItem(
                    "O digita il valore: "
                ),
            };
            return new BlockBuilder<ReturnBlock>("Return", BlockType.RitornaValore, BlockCategory.Funzione)
                .AddLabel("ritorna VALORE")
                .AddQuestions(editItems)
                .AddTextDroppedFunction(() => { return $"return {editItems[0].Value ?? editItems[1].Value}"; })
                .Build();
        }
    }
}
