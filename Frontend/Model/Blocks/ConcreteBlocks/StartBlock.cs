using Frontend.Builders;
using Frontend.Model.Blocks;
using Frontend.Models.Blocks.Shapes;

namespace Frontend.Models.Blocks.ConcreteBlocks
{
    /// <summary>
    /// Classe concreta che rappresenta un blocco start
    /// </summary>
    public class StartBlock : AbstractFrontEndBlock
    {
        /// <summary>
        /// Costruttore di default che imposta la forma, l'<see cref="IFrontEndBlock.Width"/> e l'altezza del blocco
        /// </summary>
        public StartBlock()
        {
            Shape = ShapeTypeMethods.GetShape(ShapeType.UPPER);
            Width = 130;
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
