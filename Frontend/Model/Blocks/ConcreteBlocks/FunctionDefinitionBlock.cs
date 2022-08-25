using Frontend.Builders;
using Frontend.Model.Blocks;
using Frontend.Model.QuestionItem;
using Frontend.Models.Blocks.Shapes;

namespace Frontend.Models.Blocks.ConcreteBlocks
{
    internal class FunctionDefinitionBlock : AbstractFrontEndBlock
    {
        public FunctionDefinitionBlock()
        {
            Shape = ShapeTypeMethods.GetShape(ShapeType.UPPER);
            HorizontalOffset = 130;
            Height = 48;
        }

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
