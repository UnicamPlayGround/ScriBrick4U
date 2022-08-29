using Frontend.Builders;
using Frontend.Model.Blocks;
using Frontend.Model.QuestionItem;
using Frontend.Models.Blocks.Shapes;

namespace Frontend.Models.Blocks.ConcreteBlocks
{
    /// <summary>
    /// Classe concreta che rappresenta un blocco per chiamare una funzione
    /// </summary>
    public class FunctionCallBlock : AbstractFrontEndBlock
    {
        /// <summary>
        /// Costruttore di default che imposta la forma, l'<see cref="IFrontEndBlock.HorizontalOffset"/> e l'altezza del blocco
        /// </summary>
        public FunctionCallBlock()
        {
            Shape = ShapeTypeMethods.GetShape(ShapeType.RECTANGLE);
            HorizontalOffset = 130;
            Height = 48;
        }
        
        public override IFrontEndBlock GetInfo()
        {
            IBlockEditItem editItem = new EntryEditItem(
                "Scrivi il nome della funzione da richiamare: ",
                TypeValue.STRING,
                "Devi scrivere il nome della funzione da richiamare."
            );

            return new BlockBuilder<FunctionCallBlock>("Chiama Funzione", BlockType.ChiamaFunzione)
                .AddLabel("chiama funzione NOMEFUNZIONE")
                .AddQuestion(editItem)
                .AddTextDroppedFunction(() => { return "chiama funzione " + editItem.ToString().ToUpper(); })
                .Build();
        }
    }
}
