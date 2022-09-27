using Frontend.Helpers.Builders;
using Frontend.Models.Blocks.AbstractTypeBlocks;

namespace Frontend.Models.Blocks.ConcreteBlocks
{
    /// <summary>
    /// Classe concreta che rappresenta un blocco per l'evento Start di Unity
    /// </summary>
    public class StartBlock : UpperFrontEndBlock
    {
        public override IFrontEndBlock GetInfo()
        {
            return new BlockBuilder<StartBlock>("Start", BlockType.Principale, BlockCategory.Principale)
                .AddLabel("Start", 18)
                .AddTextDroppedFunction(() => { return "Start"; })
                .Build();
        }
    }
}
