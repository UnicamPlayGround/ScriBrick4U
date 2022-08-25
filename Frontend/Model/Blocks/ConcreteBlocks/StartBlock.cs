using Frontend.Builders;
using Frontend.Model.Blocks;
using Frontend.Models.Blocks.Shapes;

namespace Frontend.Models.Blocks.ConcreteBlocks
{
    public class StartBlock : AbstractFrontEndBlock
    {
        public StartBlock()
        {
            Shape = ShapeTypeMethods.GetShape(ShapeType.UPPER);
            HorizontalOffset = 130;
            Height = 48;
        }

        public override IFrontEndBlock GetInfo()
        {
            return new BlockBuilder<StartBlock>("Start", BlockType.Principale)
                .AddLabel("Start", 18)
                .AddTextDroppedFunction(() => { return "Start"; })
                .Build();
        }
    }
}
