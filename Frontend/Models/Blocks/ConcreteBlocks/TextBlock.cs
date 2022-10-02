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
    public class TextBlock : RectangleFrontEndBlock
    {
        public override IFrontEndBlock GetInfo()
        {
            List<IBlockEditItem> editItems = new()
            {
                new EntryEditItem(
                    "Digita il tag dell'elemento",
                    TypeValue.STRING,
                    "Valore non valido."
                ),
                new EntryEditItem(
                    "Digita il nuovo testo:"
                ),
                new PickerEditItem(
                    "O seleziona una variabile",
                    BlockViewModel.VariableNames
                )
            };
            return new BlockBuilder<TextBlock>("Testo", BlockType.Text, BlockCategory.Testo)
                .AddLabel("Aggiorna testo")
                .AddQuestions(editItems)
                .AddTextDroppedFunction(() => $"Testo {editItems[0].Value} = {editItems[1].Value ?? editItems[2].Value}")
                .Build();
        }
    }
}
