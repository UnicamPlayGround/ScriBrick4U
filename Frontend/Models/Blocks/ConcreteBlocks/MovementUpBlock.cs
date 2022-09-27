using Frontend.Helpers.Builders;
using Frontend.Models.Blocks.AbstractTypeBlocks;
using Frontend.Models.QuestionItem;

namespace Frontend.Models.Blocks.ConcreteBlocks
{
    /// <summary>
    /// Classe concreta che rappresenta un blocco movimento in alto
    /// </summary>
    public class MovementUpBlock : RectangleFrontEndBlock
    {
        public override IFrontEndBlock GetInfo()
        {
            IBlockEditItem editItem = new EntryEditItem(
                "Digita di quanto vuoi muovere lo sprite in alto: ",
                TypeValue.NUMBER,
                "La quantità di passi deve essere un numero."
            );
            return new BlockBuilder<MovementUpBlock>("MovimentoSu", BlockType.Movimento, BlockCategory.Movimento)
                .AddLabel("muovi di TOT passi in alto")
                .AddQuestion(editItem)
                .AddTextDroppedFunction(() => { return "muovi di " + editItem.ToString() + " passi in alto"; })
                .Build();
        }
    }
}
