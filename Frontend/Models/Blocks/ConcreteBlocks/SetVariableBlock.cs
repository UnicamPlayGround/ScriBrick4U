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
    public class SetVariableBlock : RectangleFrontEndBlock
    {
        public override IFrontEndBlock GetInfo()
        {
            List<string> operators = new() { "=", "+=", "-=", "*=", "/=" };
            List<IBlockEditItem> editItems = new()
            {
                new PickerEditItem(
                    "Seleziona la variabile: ",
                    TypeValue.STRING,
                    "Devi selezionare una variabile",
                    BlockViewModel.VariableNames),
                new PickerEditItem(
                    "Seleziona l'operazione",
                    TypeValue.STRING,
                    "Devi selezionare un operatore",
                    operators
                ),
                new EntryEditItem(
                    "Digita il valore:",
                    TypeValue.STRING,
                    "Valore non valido."
                )
            };
            return new BlockBuilder<SetVariableBlock>("ModificaVariabile", BlockType.ModificaVariabile, BlockCategory.Variabile)
                .AddLabel("Modifica variabile")
                .AddQuestions(editItems)
                .AddTextDroppedFunction(() => $"{editItems[0].Value} {editItems[1].Value} {editItems[2].Value}")
                .Build();
        }
    }
}
