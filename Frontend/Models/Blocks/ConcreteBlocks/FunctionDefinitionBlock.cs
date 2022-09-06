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
            IBlockEditItem editItem = new EntryEditItem(
                "Digita il nome della funzione: ",
                TypeValue.STRING,
                "Devi scrivere il nome della funzione."
            );
            return new BlockBuilder<FunctionDefinitionBlock>("Definizione Funzione", BlockType.DefinizioneFunzione, BlockCategory.Funzione)
                .AddLabel("funzione NOME")
                .AddQuestion(editItem)
                .AddTextDroppedFunction(() => { return "funzione " + editItem.ToString()?.ToUpper(); })
                .Build();
        }
    }
}