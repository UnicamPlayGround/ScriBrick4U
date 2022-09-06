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
    public class VariableDefinitionBlock : RectangleFrontEndBlock
    {
        public override IFrontEndBlock GetInfo()
        {
            List<string> variableTypes = new() { "int", "float", "char", "string", "bool" };
            List<IBlockEditItem> editItems = new()
            {
                new PickerEditItem(
                    "Seleziona il tipo della variabile: ",
                    TypeValue.STRING,
                    "Devi selezionare un tipo",
                    variableTypes),
                new EntryEditItem(
                    "Digita il nome della variabile:",
                    TypeValue.VARIABLE,
                    "Nome variabile non valido."
                )
            };
            return new BlockBuilder<VariableDefinitionBlock>("DefinizioneVariabile", BlockType.DefinizioneVariabile, BlockCategory.Variabile)
                .AddLabel("Definisci variabile")
                .AddQuestions(editItems)
                .AddTextDroppedFunction(() => $"{editItems[0].Value} {editItems[1].Value}")
                .Build();
        }
    }
}
