using Frontend.Builders;
using Frontend.Model.Blocks;
using Frontend.Model.QuestionItem;
using Frontend.Models.Blocks.Shapes;

namespace Frontend.Models.Blocks.ConcreteBlocks
{
    /// <summary>
    /// Classe concreta che rappresenta un blocco rotazione
    /// </summary>
    public class RotateBlock : AbstractFrontEndBlock
    {
        /// <summary>
        /// Costruttore di default che imposta la forma, l'<see cref="IFrontEndBlock.HorizontalOffset"/> e l'altezza del blocco
        /// </summary>
        public RotateBlock()
        {
            Shape = ShapeTypeMethods.GetShape(ShapeType.RECTANGLE);
            HorizontalOffset = 130;
            Height = 48;
        }

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
