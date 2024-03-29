﻿using Frontend.Helpers.Builders;
using Frontend.Models.Blocks.AbstractTypeBlocks;
using Frontend.Models.QuestionItem;

namespace Frontend.Models.Blocks.ConcreteBlocks
{
    /// <summary>
    /// Classe concreta che rappresenta un blocco per definire una variabile
    /// </summary>
    public class VariableDefinitionBlock : RectangleFrontEndBlock
    {
        public override IFrontEndBlock GetInfo()
        {
            List<string> variableTypes = new() { "int", "float", "char", "string", "bool" };
            List<string> scopes = new() { "local", "global"};
            List<IBlockEditItem> editItems = new()
            {
                new PickerEditItem(
                    "Seleziona scope della variabile: ",
                    TypeValue.STRING,
                    "Devi selezionare uno scope",
                    scopes),
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
                .AddTextDroppedFunction(() => $"{editItems[0].Value} {editItems[1].Value} {editItems[2].Value}")
                .Build();
        }
    }
}
