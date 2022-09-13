using Frontend.Models.QuestionItem;
using Frontend.Models.Blocks.AbstractTypeBlocks;
using Frontend.Helpers.Builders;

namespace Frontend.Models.Blocks.ConcreteBlocks
{
    /// <summary>
    /// Classe concreta che rappresenta un blocco movimento in avanti
    /// </summary>
    public class MovementForwardBlock : RectangleFrontEndBlock
    {
        public override IFrontEndBlock GetInfo()
        {
            IBlockEditItem editItem = new EntryEditItem(
                "Digita di quanto vuoi muovere lo sprite in avanti: ",
                TypeValue.NUMBER,
                "La quantità di passi deve essere un numero."
            );
            return new BlockBuilder<MovementForwardBlock>("MovimentoAvanti", BlockType.Movimento, BlockCategory.Movimento)
                .AddLabel("muovi di TOT passi in avanti")
                .AddQuestion(editItem)
                .AddTextDroppedFunction(() => { return "muovi di " + editItem.ToString() + " passi in avanti"; })
                .Build();
        }
    }
}
