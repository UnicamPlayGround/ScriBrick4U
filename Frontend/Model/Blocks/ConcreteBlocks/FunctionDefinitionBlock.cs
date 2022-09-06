using Frontend.Builders;
using Frontend.Model.Blocks;
using Frontend.Model.Blocks.AbstractTypeBlocks;
using Frontend.Model.Blocks.Shapes;
using Frontend.Model.QuestionItem;
using Microsoft.Maui.Storage;

namespace Frontend.Model.Blocks.ConcreteBlocks
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
            return new BlockBuilder<FunctionDefinitionBlock>("Definizione Funzione", BlockType.DefinizioneFunzione)
                .AddLabel("funzione NOME")
                .AddQuestion(editItem)
                .AddTextDroppedFunction(() => { return "funzione " + editItem.ToString().ToUpper(); })
                .Build();
        }
    }
}