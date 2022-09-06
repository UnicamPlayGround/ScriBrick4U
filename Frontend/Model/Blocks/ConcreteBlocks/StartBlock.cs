using Frontend.Builders;
using Frontend.Model.Blocks;
using Frontend.Model.Blocks.AbstractTypeBlocks;
using Frontend.Model.Blocks.Shapes;

namespace Frontend.Model.Blocks.ConcreteBlocks
{
    /// <summary>
    /// Classe concreta che rappresenta un blocco Start
    /// </summary>
    public class StartBlock : UpperFrontEndBlock
    {
        public override IFrontEndBlock GetInfo()
        {
            return new BlockBuilder<StartBlock>("Start", BlockType.Principale)
                .AddLabel("Start", 18)
                .AddTextDroppedFunction(() => { return "Start"; })
                .Build();
        }
    }
}
