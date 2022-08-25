using Frontend.Builders;
using Frontend.Model.Blocks;
using Frontend.Model.QuestionItem;
using Frontend.Models.Blocks.Shapes;

namespace Frontend.Models.Blocks.ConcreteBlocks
{
    public class MovementBlock : AbstractFrontEndBlock
    {
        public MovementBlock()
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
