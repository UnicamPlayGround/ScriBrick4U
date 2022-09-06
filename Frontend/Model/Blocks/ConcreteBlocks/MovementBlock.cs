using Frontend.Builders;
using Frontend.Model.Blocks.AbstractTypeBlocks;
using Frontend.Model.QuestionItem;
using Frontend.Model.Blocks.Shapes;

namespace Frontend.Model.Blocks.ConcreteBlocks
{
    /// <summary>
    /// Classe concreta che rappresenta un blocco movimento
    /// </summary>
    public class MovementBlock : RectangleFrontEndBlock
    {
        public override IFrontEndBlock GetInfo()
        {
            IBlockEditItem editItem = new EntryEditItem(
                "Digita di quanto vuoi muovere lo sprite: ",
                TypeValue.NUMBER,
                "La quantità di passi deve essere un numero."
            );
            return new BlockBuilder<MovementBlock>("Movimento", BlockType.Movimento)
                .AddLabel("muovi di TOT passi")
                .AddQuestion(editItem)
                .AddTextDroppedFunction(() => { return "muovi di " + editItem.ToString() + " passi"; })
                .Build();
        }
    }
}
