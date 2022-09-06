using Frontend.Helpers.Builders;
using Frontend.Models.Blocks.AbstractTypeBlocks;
using Frontend.Models.QuestionItem;

namespace Frontend.Models.Blocks.ConcreteBlocks
{
    /// <summary>
    /// Classe concreta che rappresenta un blocco rotazione
    /// </summary>
    public class RotateBlock : RectangleFrontEndBlock
    {
        public override IFrontEndBlock GetInfo()
        {
            IBlockEditItem editItem = new EntryEditItem(
                "Digita di quanto vuoi muovere lo sprite: ",
                TypeValue.NUMBER,
                "I gradi devono essere un numero."
            );
            return new BlockBuilder<RotateBlock>("Rotazione", BlockType.Movimento)
                .AddLabel("ruota di TOT gradi")
                .AddQuestion(editItem)
                .AddTextDroppedFunction(() => { return "ruota di " + editItem.ToString() + " gradi"; })
                .Build();
        }
    }
}
