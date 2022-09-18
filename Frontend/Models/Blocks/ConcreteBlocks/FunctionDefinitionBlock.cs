using Frontend.Helpers.Builders;
using Frontend.Models.Blocks.AbstractTypeBlocks;
using Frontend.Models.QuestionItem;


namespace Frontend.Models.Blocks.ConcreteBlocks
{
    /// <summary>
    /// Classe concreta che rappresenta un blocco per definire una funzione
    /// </summary>
    public class FunctionDefinitionBlock : UpperFrontEndBlock
    {
        public override IFrontEndBlock GetInfo()
        {
            List<string> scopes = new() { "private", "public", "protected" };
            List<string> returnTypes = new() { "void", "int", "double", "float", "char", "string" };
            List<IBlockEditItem> editItems = new()
            {
                new PickerEditItem(
                  "Seleziona scope della funzione",
                  TypeValue.STRING,
                  "Scope della funzione obbligatorio",
                  scopes
                ),
                new PickerEditItem(
                    "Seleziona valore di ritorno",
                    TypeValue.STRING,
                    "Valore di ritorno obbligatorio",
                    returnTypes
                ),
                new EntryEditItem(
                    "Digita il nome della funzione: ",
                    TypeValue.FUNCTION_NAME,
                    "Nome funzione non valido."
                ),
            };
            return new BlockBuilder<FunctionDefinitionBlock>("Definizione Funzione", BlockType.DefinizioneFunzione, BlockCategory.Funzione)
                .AddLabel("funzione NOME")
                .AddQuestions(editItems)
                .AddTextDroppedFunction(() => { return $"{editItems[0].Value} {editItems[1].Value} {editItems[2].Value}"; })
                .Build();
        }
    }
}