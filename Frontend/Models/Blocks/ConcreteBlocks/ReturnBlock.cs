using Frontend.Helpers.Builders;
using Frontend.Models.Blocks.AbstractTypeBlocks;
using Frontend.Models.QuestionItem;
using Frontend.ViewModels;

namespace Frontend.Models.Blocks.ConcreteBlocks
{
    /// <summary>
    /// Classe concreta che rappresenta un blocco per il valore di ritorno di una funzione
    /// </summary>
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
